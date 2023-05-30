import React, {useState} from 'react';
import s from './RegistrationForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser, signUpUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";

function RegistrationForm(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            registrationSurname: '',
            registrationName: '',
            registrationPatronymic: '',
            registrationPassword: '',
            registrationRetryPassword: '',
        },
        mode: "onBlur"
    });

    const onSubmit = (payload) => {
        if(!isLoading) {
            setIsLoading(true);
            if (payload.registrationPassword !== payload.registrationRetryPassword) {
                alert('Вы указали разные пароли!');
                return;
            }

            delete payload.registrationRetryPassword;
            //payload.registrationPassword = md5(payload.registrationPassword);

            const data = {
                userId: props.userId,
                surname: payload.registrationSurname,
                name: payload.registrationName,
                patronymic: payload.registrationPatronymic,
                password: payload.registrationPassword
            }
            console.log(data);
            dispatch(signUpUser(data)).then(() => {
                setIsLoading(false);
                navigate("/login");
            });
        }
    }

    return (
        <div className={s.createRoomForm}>
            <p className={s.header}>Название сервиса</p>
            <div>
                <p className={s.authorization}>Регистрация</p>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <Input register={register}
                           registerName='registrationSurname'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Фамилия"
                           require={true}
                           type="text"/>
                    <Input register={register}
                           registerName='registrationName'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Имя"
                           require={true}
                           type="text"/>
                    <Input register={register}
                           registerName='registrationPatronymic'
                           options={{}
                               // {
                               //     required: true
                               // }
                           }
                           errors={errors}
                           title="Отчество"
                           // require={true}
                           type="text"/>
                    <Input register={register}
                           registerName='registrationPassword'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Пароль"
                           require={true}
                           type="password"/>
                    <Input register={register}
                           registerName='registrationRetryPassword'
                           options={
                               {
                                   required: true
                               }
                           }
                           errors={errors}
                           title="Повторите пароль"
                           require={true}
                           type="password"/>

                    <Button type="submit">Войти</Button>
                </Form>
            </div>
        </div>
    )
}

export default RegistrationForm;
