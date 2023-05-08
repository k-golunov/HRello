import React, {useEffect, useState} from 'react';
import s from './Task.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {createUser, signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import Select, { StylesConfig } from "react-select";
import Dropdown from "../Dropdown/Dropdown";
import {getTask} from "../../store/slices/taskSlice";
import {useProfile} from "../../hooks/use-profile";
import Breadcrumbs from "../Breadcrumbs/Breadcrumbs";
import Status from "../Status/Status";
import {useTask} from "../../hooks/use-task";
import Tag from "../Tag/Tag";

function Task(props) {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getTask(props.taskID));
        debugger
    }, []);
    const task = useTask();
    const navigate = useNavigate();
    console.log(task)

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
            <Breadcrumbs breadcrumbs={[{id: 1, title: "Все задачи", src: "/tasks/all"}]}/>
            <div className={s.taskInformation}>
                <div className={s.taskInformationLeftContainer}>
                    <h1>{task.name}</h1>
                    <div>
                        <h4 className={s.taskInformationTitle}>Информация</h4>
                        <div className={s.taskInformationTable}>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>ФИО сотрудника</p>
                                <p>ФИО сотрудника</p>
                            </div>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>Блок</p>
                                <p>{block[task.block]}</p>
                            </div>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>Категория</p>
                                <p>{category[task.category]}</p>
                            </div>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>Планируемый результат</p>
                                <p>{task.waitResult}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div className={s.taskInformationRightContainer}>
                    <Status type={task.taskStatus}/>
                    <Tag>{task.quarter+" квартал"}</Tag>
                    <Tag>{task.year}</Tag>
                    <div>
                        <p className={s.taskInformationPlannedWeightTitle}>Планируемый вес:</p>
                        <p className={s.taskInformationPlannedWeight}>{task.plannedWeight+"%"}</p>
                    </div>
                    <Button onClick={()=>navigate("./edit")}>Редактировать</Button>
                </div>
            </div>

        </div>
    )
}

export default Task;
