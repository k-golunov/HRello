import React, {useState} from 'react';
import s from './InvitationForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {createUser, signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link} from "react-router-dom";
import Select, { StylesConfig } from "react-select";
import Dropdown from "../Dropdown/Dropdown";

function InvitationForm(props) {
    const dispatch = useDispatch();
    const departmentOptions = [
        { value: 1, label: "Управление персоналом" },
        { value: 2, label: "Компенсации и льготы" },
        { value: 3, label: "Обучение" },
        { value: 4, label: "HR интегратор и производства" },
    ];

    const roleOptions = [
        { value: "employee", label: "Сотрудник" },
        { value: "director", label: "Руководитель" },
        { value: "maindirector", label: "Главный руководитель" },
    ];
    const [departmentSelected, setDepartmentSelected] = useState(departmentOptions[0]);
    const [roleSelected, setRoleSelected] = useState(roleOptions[0]);

    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            invitationEmail: '',
        },
        mode: "onBlur"
    });

    const onSubmit = (payload) => {
        const data = {
            email: payload.invitationEmail,
            departamentId: departmentSelected.value,
            role: roleSelected.value
        }
        console.log(data);
        dispatch(createUser(data));
    }





    return (
        <div className={s.invitationForm}>
            <div>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <div className={s.inputs} >
                        <Input register={register}
                               registerName='invitationEmail'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Почта"
                               require={true}
                               type="text"
                        />
                        <Dropdown title="Отдел" minWidth="394px" options={departmentOptions} onChange={e => setDepartmentSelected(e)}/>
                        <Dropdown title="Роль"  minWidth="394px" options={roleOptions} onChange={e => setRoleSelected(e)}/>
                    </div>


                    <div className={s.button}>
                        <Button type="submit">Отправить приглашение </Button>
                    </div>

                </Form>
            </div>
        </div>
    )
}

export default InvitationForm;
