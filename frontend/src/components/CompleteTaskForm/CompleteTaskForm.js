import React, {useEffect, useState} from 'react';
import s from './CompleteTaskForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser, signUpUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import PageTitle from "../PageTitle/PageTitle";
import Dropdown from "../Dropdown/Dropdown";
import {createTask} from "../../store/slices/tasksSlice";
import Breadcrumbs from "../Breadcrumbs/Breadcrumbs";
import {useTask} from "../../hooks/use-task";
import {completeTask, getTask, getTaskHistory, sendToReviewTask, updateTask} from "../../store/slices/taskSlice";
import {useAuth} from "../../hooks/use-auth";

function CompleteTaskForm(props) {
    const dispatch = useDispatch();
    const user = useAuth();
    const navigate = useNavigate();
    const {register, handleSubmit, reset, formState: {errors}} = useForm({
        defaultValues: {
            completeTaskPercent: '',
            completeTaskComment: '',
        },
        mode: "onBlur"
    });


    const blocks = [
        {value: 'Selection', label: 'Подбор'},
        {value: 'Adaptation', label: 'Адаптация'},
        {value: 'StaffDevelopment', label: 'Развитие персонала'},
        {value: 'HRSupport', label: 'HR-сопровождение'},
        {value: 'CorporateCulture', label: 'Корпоративная культура'},
        {value: 'PersonnelAccountingAndSalary', label: 'Кадровый учет и з/п'},
        {value: 'HRBrandExternal', label: 'HR-бренд внешний'},
        {value: 'InternalWork', label: 'Внутренняя работа'},
        {value: 'Estimation', label: 'Оценка'},
    ]

    const categories = [
        {value: 'Planned', label: 'Запланированная'},
        {value: 'NotPlanned', label: 'Незапланированная'},
    ]

    const quarters = [
        {value: 1, label: '1 квартал'},
        {value: 2, label: '2 квартал'},
        {value: 3, label: '3 квартал'},
        {value: 4, label: '4 квартал'}
    ]

    const onSubmit = (payload) => {
        // if (payload.registrationPassword !== payload.registrationRetryPassword) {
        //     alert('Вы указали разные пароли!');
        //     return;
        // }
        //
        // delete payload.registrationRetryPassword;
        // payload.registrationPassword = md5(payload.registrationPassword);
        //
        const data = {
            taskId: props.taskID,
            result: payload.completeTaskPercent,
            comment: payload.completeTaskComment,
            changeByUserId: user.id
        }
        console.log(data);
        //console.log("SELECTED QUARTER", selectedQuarter);

        dispatch(completeTask(data)).then(response => {
            if(!response.error)
            {
                toast.success('Задача успешно завершена!', {
                    position: "bottom-right",
                    autoClose: 3000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                    theme: "light",
                });
                dispatch(getTask(props.taskID))
                dispatch(getTaskHistory(props.taskID))
                navigate("/task/"+props.taskID)
            }
        });
    }

    return (
        <div className={s.endingTaskForm}>
            {/*<PageTitle title="Результаты работы"/>*/}
            <div>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <div className={s.formLeftContainer}>
                        <Input register={register}
                               registerName='completeTaskComment'
                               errors={errors}
                               title="Комментарий"
                               options={
                                   {
                                       required: true
                                   }
                               }
                               require={true}
                               type="text"
                               rows={2}
                               as="textarea"
                               description="Какое-то напоминание для руководителя/главного руководителя"
                        />

                        <div className={s.buttons}>
                            <Button type="submit">Отправить</Button>
                        </div>
                    </div>

                    <div className={s.formRightContainer}>
                        <Input register={register}
                               registerName='completeTaskPercent'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Процент выполнения"
                               require={true}
                               type="text"
                               description="Укажите, на сколько процентов, по Вашему мнению, сотрудник справился с задачей"
                        />
                    </div>
                </Form>
            </div>
        </div>
    )
}

export default CompleteTaskForm;
