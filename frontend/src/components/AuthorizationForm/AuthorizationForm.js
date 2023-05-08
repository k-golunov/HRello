import React from 'react';
import s from './AuthorizationForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link} from "react-router-dom";

function AuthorizationForm(props) {
    const dispatch = useDispatch();
    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            authorizationEmail: '',
            authorizationPassword: '',
        },
        mode: "onBlur"
    });

    const onSubmit = (payload) => {
        payload.authorizationPassword = md5(payload.authorizationPassword);
        const data = {
            email: payload.authorizationEmail,
            password: payload.authorizationPassword,
            rememberMe: true
        }
        console.log(data);
        dispatch(signInUser(data));
    }

    return (
        <div className={s.authorizationForm}>
            <p className={s.header}>Название сервиса</p>
            <div>
                <p className={s.authorization}>Авторизация</p>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <Input register={register}
                           registerName='authorizationEmail'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Почта"
                           require={true}
                           type="text"/>
                    <div className={s.password}>
                        <Input register={register}
                               registerName='authorizationPassword'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Пароль"
                               require={true}
                               type="password"/>
                        <div className={s.forget}><Link to="/restore">Забыли пароль?</Link></div>
                    </div>

                    <Button type="submit">Войти</Button>
                </Form>
            </div>
        </div>
    )
}

export default AuthorizationForm;
