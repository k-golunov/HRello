import React, {useEffect, useState} from 'react';
import s from './AddDepartmentForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import {changeTaskStatus, getTask, getTaskHistory, removeTask} from "../../store/slices/taskSlice";
import {createBlock, getBlocks} from "../../store/slices/blocksSlice";
import {createDepartment, getDepartments} from "../../store/slices/departmentsSlice";
import {getUsers} from "../../store/slices/usersSlice";
import {useUsers} from "../../hooks/use-users";
import FilterDropdown from "../FilterDropdown/FilterDropdown";
import Dropdown from "../Dropdown/Dropdown";

function AddDepartmentForm(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);

    const users = useUsers();

    let employeeFilter = []

    if(!users.isLoading)
        employeeFilter = users.users.filter(user => user.emailConfirmed).map(user =>{
            return { value: user.id, label: user.surname + " " + user.name + " " + user.patronymic}
        })

    const [selectedEmployee, setSelectedEmployee] = useState(null);

    useEffect(() => {
        dispatch(getUsers());
    }, []);


    const onSubmit = (payload) => {
        if(!isLoading)
        {
            setIsLoading(true);
            dispatch(createDepartment({
                name: payload.addDepartmentName,
                bossId: selectedEmployee.value
            })).then(response=>{
                dispatch(getDepartments())
                setSelectedEmployee(null)
                props.reset()
                props.setActive(false)
            })
        }
    }

    return (
        <div className={s.authorizationForm}>
            <div>
                <p className={s.authorization}>Создание отдела</p>
                <Form className={s.form} onSubmit={props.handleSubmit(onSubmit)}>
                    <Input register={props.register}
                           registerName='addDepartmentName'
                           errors={props.errors}
                           title="Название"
                           options={
                               {
                                   required: {
                                       value: true,
                                       message: "Поле обязательно для ввода"
                                   },
                               }
                           }
                           //require={true}
                           type="text"
                           //description="Расскажите о возникших сложностях"
                    />
                    <Dropdown title="Руководитель" options={employeeFilter} minWidth="353px"
                              onChange={setSelectedEmployee}/>
                    <div className={s.buttons}>
                        <Button type="submit">Создать</Button>
                        <Button isSecond click={()=>{
                            props.setActive(false)
                            props.reset()
                        }}>Отменить</Button>
                    </div>

                </Form>
            </div>
        </div>
    )
}

export default AddDepartmentForm;
