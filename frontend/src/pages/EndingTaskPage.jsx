import React, {useEffect, useState} from 'react';
import { useDispatch } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import {useNavigate, useParams} from "react-router-dom";
import Breadcrumbs from "../components/Breadcrumbs/Breadcrumbs";
import {useTask} from "../hooks/use-task";
import {getTask} from "../store/slices/taskSlice";
import EndingTaskForm from "../components/EndingTaskForm/EndingTaskForm";
import s from "../components/Task/Task.module.css";
import Task from "../components/Task/Task";

const EndingTaskPage = (props) => {
    const { taskId } = useParams();
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getTask(taskId))
    }, []);

    const task = useTask();
    const navigate = useNavigate();

    const block = {
        'Selection': 'Подбор',
        'Adaptation': 'Адаптация',
        'StaffDevelopment': 'Развитие персонала',
        'HRSupport': 'HR-сопровождение',
        'CorporateCulture': 'Корпоративная культура',
        'PersonnelAccountingAndSalary': 'Кадровый учет и з/п',
        'HRBrandExternal': 'HR-бренд внешний',
        'InternalWork': 'Внутренняя работа',
        'Estimation': 'Оценка'
    }

    const category = {
        'Planned': 'Запланированная',
        'NotPlanned': 'Незапланированная'
    }

    return (
        <div className={s.task}>
            <Breadcrumbs breadcrumbs={[{id: 1, title: "Мои задачи", src: "/tasks/my"}, {id: 2, title: "Просмотр задачи", src: "/task/"+task.id}]}/>
            <Task taskID={taskId} action="Завершение задачи"/>
            <EndingTaskForm/>
        </div>
    );
};

export default EndingTaskPage;
