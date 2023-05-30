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
import TaskInformation from "./TaskInformation";
import ToCancellationForm from "../ToCancellationForm/ToCancellationForm";
import CancelCompletionForm from "../CancelCompletionForm/CancelCompletionForm";
import TaskResults from "./TaskResults";

function Task(props) {
    const dispatch = useDispatch();
    const task = useTask();
    const user = useAuth();

    const [onReworkModalActive, setOnReworkModalActive] = useState(false);
    const [cancellationModalActive, setCancellationModalActive] = useState(false);
    const [cancelCompletionModalActive, setCancelCompletionModalActive] = useState(false);

    const {register: registerOnRework, reset: resetOnRework, handleSubmit: handleSubmitOnRework, formState: {errors: errorsOnRework}} = useForm({
        defaultValues: {
            onReworkComment: ''
        },
        mode: "onBlur"
    });

    const {register: registerToCancellation, reset: resetToCancellation, handleSubmit: handleSubmitToCancellation, formState: {errors: errorsToCancellation}} = useForm({
        defaultValues: {
            toCancellationComment: ''
        },
        mode: "onBlur"
    });

    const {register: registerCancelCompletion, reset: resetCancelCompletion, handleSubmit: handleSubmitCancelCompletion, formState: {errors: errorsCancelCompletion}} = useForm({
        defaultValues: {
            cancelCompletionComment: ''
        },
        mode: "onBlur"
    });

    useEffect(() => {
        dispatch(getTask(props.taskID));
        dispatch(getTaskHistory(props.taskID));
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

    const isBossAndDepartment = user.role !== "employee" && user.departmentID === task.departmentID;
    return (
        <div className={s.task}>
            <TaskInformation task={task}
                             action="watching"
                             setOnReworkModalActive={setOnReworkModalActive}
                             setCancellationModalActive={setCancellationModalActive}
                             setCancelCompletionModalActive={setCancelCompletionModalActive}
            />

            <TaskResults task={task}
                         action="watching"
                         setCancelCompletionModalActive={setCancelCompletionModalActive}
            />






            {/*<div className={s.taskInformation}>*/}
            {/*    <div className={s.taskInformationLeftContainer}>*/}
            {/*        <h1>{task.name}*/}
            {/*            {*/}
            {/*                props.action ? <p className={s.taskAction}>{props.action}</p> : <></>*/}
            {/*            }*/}
            {/*            {*/}
            {/*                task.taskStatus === "OnChecking" && isBossAndDepartment ? <p className={s.taskAction}>{"Новая задача"}</p> : <></>*/}
            {/*            }*/}
            {/*        </h1>*/}

            {/*        <div>*/}
            {/*            <h4 className={s.taskInformationTitle}>Информация</h4>*/}
            {/*            <div className={s.taskInformationTable}>*/}
            {/*                <div className={s.taskInformationTableRow}>*/}
            {/*                    <p className={s.taskInformationTableTitle}>ФИО сотрудника</p>*/}
            {/*                    <p>{task.userName}</p>*/}
            {/*                </div>*/}
            {/*                <div className={s.taskInformationTableRow}>*/}
            {/*                    <p className={s.taskInformationTableTitle}>Блок</p>*/}
            {/*                    <p>{task.block}</p>*/}
            {/*                </div>*/}
            {/*                <div className={s.taskInformationTableRow}>*/}
            {/*                    <p className={s.taskInformationTableTitle}>Категория</p>*/}
            {/*                    <p>{category[task.category]}</p>*/}
            {/*                </div>*/}
            {/*                <div className={s.taskInformationTableRow}>*/}
            {/*                    <p className={s.taskInformationTableTitle}>Планируемый результат</p>*/}
            {/*                    <p>{task.waitResult}</p>*/}
            {/*                </div>*/}

            {/*                {*/}
            {/*                    task.taskStatus === "OnChecking" &&*/}
            {/*                    user.id !== task.userID &&*/}
            {/*                    isBossAndDepartment ?*/}
            {/*                        <div className={s.buttons}>*/}
            {/*                            <Button onClick={() => {*/}
            {/*                                dispatch(changeTaskStatus({*/}
            {/*                                    id: task.id,*/}
            {/*                                    nextStatus: "InWork"*/}
            {/*                                })).then(()=>{*/}
            {/*                                    dispatch(getTask(task.id))*/}
            {/*                                    dispatch(getTaskHistory(task.id));*/}
            {/*                                })*/}
            {/*                                //navigate("./edit")*/}
            {/*                            }}>Одобрить</Button>*/}
            {/*                            <Button isSecond onClick={() => setOnReworkModalActive(true)}>На доработку</Button>*/}
            {/*                        </div> : <></>*/}
            {/*                }*/}


            {/*                {*/}
            {/*                    task.taskStatus === "InWork" &&*/}
            {/*                    user.id === task.userID ?*/}
            {/*                        <div className={s.buttons}>*/}
            {/*                            <Button onClick={() => {*/}
            {/*                                navigate("./ending")*/}
            {/*                                //navigate("./edit")*/}
            {/*                            }}>Завершить</Button>*/}
            {/*                            /!*<Button isSecond onClick={() => setOnReworkModalActive(true)}>На доработку</Button>*!/*/}
            {/*                        </div> : <></>*/}
            {/*                }*/}

            {/*            </div>*/}
            {/*        </div>*/}
            {/*    </div>*/}
            {/*    <div className={s.taskInformationRightContainer}>*/}
            {/*        <Status type={task.taskStatus}/>*/}
            {/*        <Tag>{task.quarter + " квартал"}</Tag>*/}
            {/*        <Tag>{task.year}</Tag>*/}
            {/*        {*/}
            {/*            task.plannedWeight !== -1 ?*/}
            {/*                <div>*/}
            {/*                    <p className={s.taskInformationPlannedWeightTitle}>Планируемый вес:</p>*/}
            {/*                    <p className={s.taskInformationPlannedWeight}>{task.plannedWeight + "%"}</p>*/}
            {/*                </div> : <></>*/}
            {/*        }*/}

            {/*        /!*{*!/*/}
            {/*        /!*    task.taskStatus === "OnChecking" &&*!/*/}
            {/*        /!*    (user.id === task.userID ||*!/*/}
            {/*        /!*        ((user.role === "boss" || user.role === "mainboss")*!/*/}
            {/*        /!*            && user.departmentID === task.departmentID)*!/*/}
            {/*        /!*    ) ?*!/*/}
            {/*        /!*        <Button onClick={() => navigate("./edit")}>Редактировать</Button> : <></>*!/*/}
            {/*        /!*}*!/*/}

            {/*        {*/}
            {/*            task.taskStatus === "OnChecking" &&*/}
            {/*            (user.id === task.userID) ?*/}
            {/*                <Button onClick={() => navigate("./edit")}>Редактировать</Button> : <></>*/}
            {/*        }*/}

            {/*    </div>*/}
            {/*</div>*/}









            <History history={task.history}/>

            <ModalWindow active={onReworkModalActive}
                         setActive={setOnReworkModalActive}
                         onClose={()=>resetOnRework({
                             onReworkComment: ''
                         })}>
                <OnReworkForm handleSubmit={handleSubmitOnRework}
                                    errors={errorsOnRework}
                                    register={registerOnRework}
                                    taskID={task.id}
                              setActive={setOnReworkModalActive}
                              reset={resetOnRework}
                />
            </ModalWindow>

            <ModalWindow active={cancellationModalActive}
                         setActive={setCancellationModalActive}
                         onClose={()=>resetToCancellation({
                             toCancellationComment: ''
                         })}>
                <ToCancellationForm handleSubmit={handleSubmitToCancellation}
                              errors={errorsToCancellation}
                              register={registerToCancellation}
                              taskID={task.id}
                              setActive={setCancellationModalActive}
                              reset={resetToCancellation}
                />
            </ModalWindow>

            <ModalWindow active={cancelCompletionModalActive}
                         setActive={setCancelCompletionModalActive}
                         onClose={()=>resetCancelCompletion({
                             cancelCompletionComment: ''
                         })}>
                <CancelCompletionForm handleSubmit={handleSubmitCancelCompletion}
                                    errors={errorsCancelCompletion}
                                    register={registerCancelCompletion}
                                    taskID={task.id}
                                    setActive={setCancelCompletionModalActive}
                                    reset={resetCancelCompletion}
                />
            </ModalWindow>
        </div>
    )
}

export default Task;
