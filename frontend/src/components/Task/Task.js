import React, {useEffect} from 'react';
import s from './Task.module.css';
import Button from "../Button/Button";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {getTask, getTaskUser} from "../../store/slices/taskSlice";
import Status from "../Status/Status";
import {useTask} from "../../hooks/use-task";
import Tag from "../Tag/Tag";
import {useAuth} from "../../hooks/use-auth";
import Loading from "../Loading/Loading";

function Task(props) {
    const dispatch = useDispatch();
    const task = useTask();
    const user = useAuth();

    useEffect(() => {
        dispatch(getTask(props.taskID));
    }, []);
    useEffect(() => {
        dispatch(getTaskUser(task.userID));
    }, [task]);

    const navigate = useNavigate();
    console.log(task)

    const category = {
        'Planned': 'Запланированная',
        'NotPlanned': 'Незапланированная'
    }

    if(task.isLoading)
        return <Loading/>

    return (
        <div className={s.task}>
            <div className={s.taskInformation}>
                <div className={s.taskInformationLeftContainer}>
                    <h1>{task.name}</h1>
                    {
                        props.action ? <p className={s.taskAction}>{props.action}</p> : <></>
                    }
                    <div>
                        <h4 className={s.taskInformationTitle}>Информация</h4>
                        <div className={s.taskInformationTable}>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>ФИО сотрудника</p>
                                <p>{task.userName}</p>
                            </div>
                            <div className={s.taskInformationTableRow}>
                                <p className={s.taskInformationTableTitle}>Блок</p>
                                <p>{task.block}</p>
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
                    <Tag>{task.quarter + " квартал"}</Tag>
                    <Tag>{task.year}</Tag>
                    {
                        task.plannedWeight !== -1 ?
                            <div>
                                <p className={s.taskInformationPlannedWeightTitle}>Планируемый вес:</p>
                                <p className={s.taskInformationPlannedWeight}>{task.plannedWeight + "%"}</p>
                            </div> : <></>
                    }

                    {/*{*/}
                    {/*    task.taskStatus === "OnChecking" &&*/}
                    {/*    (user.id === task.userID ||*/}
                    {/*        ((user.role === "boss" || user.role === "mainboss")*/}
                    {/*            && user.departmentID === task.departmentID)*/}
                    {/*    ) ?*/}
                    {/*        <Button onClick={() => navigate("./edit")}>Редактировать</Button> : <></>*/}
                    {/*}*/}

                    {
                        task.taskStatus === "OnChecking" &&
                        (user.id === task.userID) ?
                            <Button onClick={() => navigate("./edit")}>Редактировать</Button> : <></>
                    }

                </div>
            </div>

        </div>
    )
}

export default Task;
