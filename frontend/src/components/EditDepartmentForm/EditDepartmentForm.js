import React, {useEffect, useState} from 'react';
import s from './EditDepartmentForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import {changeTaskStatus, getTask, getTaskHistory} from "../../store/slices/taskSlice";
import {createBlock, editBlock, getBlocks} from "../../store/slices/blocksSlice";
import Dropdown from "../Dropdown/Dropdown";
import {getUsers} from "../../store/slices/usersSlice";
import {useUsers} from "../../hooks/use-users";
import {useDepartments} from "../../hooks/use-departments";
import {getDepartments} from "../../store/slices/departmentsSlice";

function EditDepartmentForm(props) {
    console.log(props)
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);


    const onSubmit = (payload) => {
        console.log("EDIT", payload, props.selectedEmployee)

        if(!isLoading)
        {

            if(!props.selectedEmployee)
            {
                toast.error('Выберите руководителя!', {
                    position: "bottom-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                });
                return;
            }
            setIsLoading(true);


            // dispatch(editDepartment({
            //     id: props.block.id,
            //     value: payload.editBlockName
            // })).then(response=>{
            //     dispatch(getBlocks())
            //     props.setActive(false)
            //     setIsLoading(false)
            // })
        }
    }

    return (
        <div className={s.authorizationForm}>
            <div>
                <p className={s.authorization}>Редактирование отдела</p>
                <Form className={s.form} onSubmit={props.handleSubmit(onSubmit)}>
                    <Input register={props.register}
                           registerName='editDepartmentName'
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

                    <Dropdown title="Руководитель" options={props.employeeFilter} minWidth="353px"
                              onChange={props.setSelectedEmployee}
                              value={props.selectedEmployee??null}
                              isDefault
                    />
                    <div className={s.buttons}>
                        <Button type="submit">Сохранить</Button>
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

export default EditDepartmentForm;
