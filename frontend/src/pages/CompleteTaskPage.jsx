import React, {useEffect, useState} from 'react';
import { useDispatch } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import {useNavigate, useParams} from "react-router-dom";
import Breadcrumbs from "../components/Breadcrumbs/Breadcrumbs";
import {useTask} from "../hooks/use-task";
import {getTask, getTaskHistory, getTaskUser} from "../store/slices/taskSlice";
import EndingTaskForm from "../components/EndingTaskForm/EndingTaskForm";
import s from "../components/Task/Task.module.css";
import Task from "../components/Task/Task";
import TaskInformation from "../components/Task/TaskInformation";
import History from "../components/History/History";
import CompleteTaskForm from "../components/CompleteTaskForm/CompleteTaskForm";
import {useUsers} from "../hooks/use-users";
import Loading from "../components/Loading/Loading";
import {getUsers} from "../store/slices/usersSlice";

const CompleteTaskPage = (props) => {
    const { taskId } = useParams();
    const dispatch = useDispatch();
    const task = useTask();
    const users = useUsers();
    const navigate = useNavigate();

    useEffect(() => {
        dispatch(getTask(taskId));
        dispatch(getUsers());
        dispatch(getTaskHistory(taskId));
    }, []);
    useEffect(() => {
        dispatch(getTaskUser(task.userID));
    }, [task]);


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

    if(users.isLoading)
        return <Loading/>

    if(!task.isLoading && task.taskStatus !== "CompletionCheck")
        navigate("/task/"+task.id)


    return (
        <div className={s.task}>
            <Breadcrumbs breadcrumbs={[{id: 1, title: "Мои задачи", src: "/tasks/my"}, {id: 2, title: "Просмотр задачи", src: "/task/"+task.id}]}/>
            {/*<Task taskID={taskId} action="Завершение задачи"/>*/}
            {/*<EditResultTaskForm/>*/}

            <TaskInformation task={task}
                             action="complete"
            />
            <CompleteTaskForm taskID={taskId}/>

            <History history={task.history} users={users}/>
        </div>
    );
};

export default CompleteTaskPage;
