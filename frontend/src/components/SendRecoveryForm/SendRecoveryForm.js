import React, {useState} from 'react';
import s from './SendRecoveryForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {recoveryUserPassword, signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";

function SendRecoveryForm(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            recoveryEmail: ''
        },
        mode: "onBlur"
    });

    const onSubmit = (payload) => {
        if(!isLoading)
        {
            setIsLoading(true);
            //payload.authorizationPassword = md5(payload.authorizationPassword);
            const data = {
                email: payload.recoveryEmail
            }
            dispatch(recoveryUserPassword(data)).then(response=>{
                setIsLoading(false);
                if(!response.error)
                {
                    toast.success('Письмо с ссылкой для восстановления отправлено на почту!', {
                        position: "bottom-right",
                        autoClose: 3000,
                        hideProgressBar: false,
                        closeOnClick: true,
                        pauseOnHover: true,
                        draggable: true,
                        progress: undefined,
                        theme: "light",
                    });
                    navigate("/login");
                }
            });
        }
    }

    return (
        <div className={s.authorizationForm}>
            <p className={s.header}>Название сервиса</p>
            <div>
                <p className={s.authorization}>Восстановление пароля</p>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <Input register={register}
                           registerName='recoveryEmail'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Почта"
                           require={true}
                           type="text"/>

                    <Button type="submit">Восстановить пароль</Button>
                </Form>
            </div>
        </div>
    )
}

export default SendRecoveryForm;
