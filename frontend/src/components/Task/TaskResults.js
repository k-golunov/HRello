import React, {useEffect, useState} from 'react';
import s from './Task.module.css';
import Button from "../Button/Button";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {changeTaskStatus, getTask, getTaskHistory, getTaskUser} from "../../store/slices/taskSlice";
import Status from "../Status/Status";
import {useTask} from "../../hooks/use-task";
import Tag from "../Tag/Tag";
import {useAuth} from "../../hooks/use-auth";
import Loading from "../Loading/Loading";
import History from "../History/History";
import {ModalWindow} from "../ModalWindow/ModalWindow";
import OnReworkForm from "../OnReworkForm/OnReworkForm";
import {useForm} from "react-hook-form";
import {toast} from "react-toastify";

function TaskResults(props) {
    const dispatch = useDispatch();
    //const task = useTask();
    const user = useAuth();

    const navigate = useNavigate();

    const category = {
        'Planned': 'Запланированная',
        'NotPlanned': 'Незапланированная'
    }

    const isBossAndDepartment = user.role !== "employee" && user.departmentID === props.task.departmentID;
    return (
        <div className={s.taskInformation}>
            <div className={s.taskInformationLeftContainer}>
                <div>
                    {
                        props.task.userResult ?
                            <h4 className={s.taskResultTitle}>Результаты работы</h4> :<></>
                    }

                    <div className={s.taskInformationTable}>
                        {
                            props.task.userResult ?
                                <>
                                    <div className={s.taskInformationTableRow}>
                                        <p className={s.taskInformationTableTitle}>Какой результат достигнут</p>
                                        <p>{props.task.userResult.result}</p>
                                    </div>
                                    <div className={s.taskInformationTableRow}>
                                        <p className={s.taskInformationTableTitle}>Комментарий сотрудника</p>
                                        <p>{props.task.userResult.description}</p>
                                    </div>
                                </> : <></>

                        }

                        {
                            props.task.bossResult ?
                                <>
                                    <div className={s.taskInformationTableRow}>
                                        <p className={s.taskInformationTableTitle}>Комментарий руководителя</p>
                                        <p>{props.task.bossResult.comment}</p>
                                    </div>
                                </> : <></>
                        }

                        {
                            props.action === "watching" &&
                            props.task.taskStatus === "CompletionCheck" &&
                            user.id !== props.task.userID &&
                            isBossAndDepartment ?
                                <div className={s.buttons}>
                                    <Button onClick={() => {
                                        // dispatch(changeTaskStatus({
                                        //     id: props.task.id,
                                        //     nextStatus: "InWork"
                                        // })).then(()=>{
                                        //     dispatch(getTask(props.task.id))
                                        //     dispatch(getTaskHistory(props.task.id));
                                        // })
                                        navigate("./complete")
                                    }}>Одобрить</Button>
                                    <Button isSecond onClick={() => props.setCancelCompletionModalActive(true)}>Отклонить завершение</Button>
                                    {/*<ResultCell isSecond onClick={() => props.setOnReworkModalActive(true)}>На доработку</ResultCell>*/}
                                </div> : <></>
                        }


                    </div>
                </div>
            </div>
            <div className={s.taskInformationRightContainer}>
                {
                    props.task.userResult &&
                    props.task.userResult.factWeight !== -1 ?
                        <div>
                            <p className={s.taskInformationPlannedWeightTitle}>Фактический вес:</p>
                            <p className={s.taskInformationPlannedWeight}>{props.task.userResult.factWeight + "%"}</p>
                        </div> : <></>
                }

                {
                    props.task.userResult &&
                    props.task.userResult.factWeight !== -1 ?
                        <div>
                            <p className={s.taskInformationPlannedWeightTitle}>Процент выполнения<br/>оценка сотрудника:</p>
                            <p className={s.taskResultPlannedWeight}>{props.task.userResult.factResult + "%"}</p>
                        </div> : <></>
                }

                {
                    props.task.bossResult &&
                    props.task.bossResult.result !== -1 ?
                        <div>
                            <p className={s.taskInformationPlannedWeightTitle}>Процент выполнения<br/>оценка рукводителя:</p>
                            <p className={s.taskResultPlannedWeight}>{props.task.bossResult.result + "%"}</p>
                        </div> : <></>
                }


                {/*{*/}
                {/*    props.action === "watching" &&*/}
                {/*    (props.task.taskStatus === "CompletionCheck") &&*/}
                {/*    user.id === props.task.userID ?*/}
                {/*        <ResultCell onClick={() => navigate("./edit")}>Редактировать</ResultCell> : <></>*/}
                {/*}*/}


            </div>
        </div>
    )
}

export default TaskResults;
