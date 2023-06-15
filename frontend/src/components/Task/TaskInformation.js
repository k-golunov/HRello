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

function TaskInformation(props) {
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
        <div>
            <div className={s.taskInformationContainer}>
                <div className={s.taskInformationTopContainer}>


                    <div className={s.taskInformationTopContainerTags}>
                        <Status type={props.task.taskStatus}/>
                        <Tag>{props.task.quarter + " квартал"}</Tag>
                        <Tag>{props.task.year}</Tag>
                    </div>
                    <h1>{props.task.name}</h1>
                </div>




                <div className={s.taskInformation}>
                    <div className={s.taskInformationLeftContainer}>
                        {/*<h1>{props.task.name}*/}
                        {/*{*/}
                        {/*    props.task.taskStatus === "OnChecking" && isBossAndDepartment ?*/}
                        {/*        <p className={s.taskAction}>{"Новая задача"}</p> : <></>*/}
                        {/*}*/}

                        {/*{*/}
                        {/*    props.task.taskStatus === "AwaitingCancellation" && isBossAndDepartment ?*/}
                        {/*        <p className={s.taskAction}>{"Ожидающая отмены задача"}</p> : <></>*/}
                        {/*}*/}

                        {/*{*/}
                        {/*    props.task.taskStatus === "CompletionCheck" && isBossAndDepartment ?*/}
                        {/*        <p className={s.taskAction}>{"Ожидающая завершения задача"}</p> : <></>*/}
                        {/*}*/}

                        {/*{*/}
                        {/*    props.task.taskStatus === "Canceled" ?*/}
                        {/*        <p className={s.taskCanceled}>{"ЗАДАЧА ОТМЕНЕНА"}</p> : <></>*/}
                        {/*}*/}
                        {/*</h1>*/}

                        <div>
                            <h4 className={s.taskInformationTitle}>Информация</h4>
                            <div className={s.taskInformationTable}>
                                <div className={s.taskInformationTableRow}>
                                    <p className={s.taskInformationTableTitle}>ФИО сотрудника</p>
                                    <p>{props.task.userName}</p>
                                </div>
                                <div className={s.taskInformationTableRow}>
                                    <p className={s.taskInformationTableTitle}>Блок</p>
                                    <p>{props.task.block}</p>
                                </div>
                                <div className={s.taskInformationTableRow}>
                                    <p className={s.taskInformationTableTitle}>Категория</p>
                                    <p>{category[props.task.category]}</p>
                                </div>
                                <div className={s.taskInformationTableRow}>
                                    <p className={s.taskInformationTableTitle}>Планируемый результат</p>
                                    <p>{props.task.waitResult}</p>
                                </div>



                                {
                                    props.action === "watching" &&
                                    props.task.taskStatus === "OnChecking" &&
                                    user.id !== props.task.userID &&
                                    isBossAndDepartment ?
                                        <div className={s.buttons}>
                                            <Button onClick={() => {
                                                dispatch(changeTaskStatus({
                                                    id: props.task.id,
                                                    nextStatus: "InWork",
                                                    changeByUserId: user.id
                                                })).then(response=>{
                                                    dispatch(getTask(props.task.id))
                                                    dispatch(getTaskHistory(props.task.id));
                                                    if(!response.error)
                                                        toast.success('Задача успешно отправлена в работу!', {
                                                            position: "bottom-right",
                                                            autoClose: 3000,
                                                            hideProgressBar: false,
                                                            closeOnClick: true,
                                                            pauseOnHover: true,
                                                            draggable: true,
                                                            progress: undefined,
                                                            theme: "light",
                                                        });
                                                })
                                                //navigate("./edit")
                                            }}>Одобрить</Button>
                                            <Button isSecond onClick={() => props.setOnReworkModalActive(true)}>На доработку</Button>
                                        </div> : <></>
                                }



                                {
                                    props.action === "watching" &&
                                    props.task.taskStatus === "AwaitingCancellation" &&
                                    user.id !== props.task.userID &&
                                    isBossAndDepartment ?
                                        <div className={s.buttons}>
                                            <Button onClick={() => {
                                                dispatch(changeTaskStatus({
                                                    id: props.task.id,
                                                    nextStatus: "Canceled",
                                                    changeByUserId: user.id
                                                })).then(response=>{
                                                    dispatch(getTask(props.task.id))
                                                    dispatch(getTaskHistory(props.task.id));
                                                    if(!response.error)
                                                        toast.success('Задача успешно отменена!', {
                                                            position: "bottom-right",
                                                            autoClose: 3000,
                                                            hideProgressBar: false,
                                                            closeOnClick: true,
                                                            pauseOnHover: true,
                                                            draggable: true,
                                                            progress: undefined,
                                                            theme: "light",
                                                        });
                                                })
                                                //navigate("./edit")
                                            }}>Одобрить</Button>
                                            <Button isSecond onClick={() => props.setCancelCancellationModalActive(true)}>Отклонить отмену</Button>
                                        </div> : <></>
                                }



                                {
                                    props.action === "watching" &&
                                    props.task.taskStatus === "InWork" &&
                                    user.id === props.task.userID ?
                                        <div className={s.buttons}>
                                            <Button onClick={   () => {navigate("./ending")}   }>Завершить</Button>
                                            {/*<ResultCell isSecond onClick={() => setOnReworkModalActive(true)}>На доработку</ResultCell>*/}
                                        </div> : <></>
                                }











                            </div>
                        </div>
                    </div>
                    <div className={s.taskInformationRightContainer}>

                        {
                            props.task.plannedWeight !== -1 ?
                                <div>
                                    <p className={s.taskInformationPlannedWeightTitle}>Планируемый вес:</p>
                                    <p className={s.taskInformationPlannedWeight}>{props.task.plannedWeight + "%"}</p>
                                </div> : <></>
                        }




                        {
                            props.action === "watching" &&
                            (props.task.taskStatus === "OnChecking" || props.task.taskStatus === "OnRework") &&
                            user.id === props.task.userID ?
                                <Button onClick={() => navigate("./edit")}>Редактировать</Button> : <></>
                        }


                        {
                            props.action === "watching" &&
                            (props.task.taskStatus === "InWork" &&
                                user.id === props.task.userID)
                            ||
                            ((props.task.taskStatus === "OnChecking" ||
                                    props.task.taskStatus === "InWork") &&
                                user.id !== props.task.userID &&
                                isBossAndDepartment)?
                                <Button onClick={() => props.setCancellationModalActive(true)} isSecond>Отменить</Button> : <></>
                            // <ResultCell onClick={() => navigate("./edit")} isSecond>Отменить</ResultCell> : <></>
                        }




                    </div>
                </div>
            </div>


        </div>
    )
}

export default TaskInformation;
