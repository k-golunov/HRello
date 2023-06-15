import React, {useEffect} from 'react';
import s from './ResultModal.module.css';
import Status from "../Status/Status";
import Tag from "../Tag/Tag";
import {useBlocks} from "../../hooks/use-blocks";
import {useDepartments} from "../../hooks/use-departments";
import {useDispatch} from "react-redux";
import {getTask, getTaskHistory} from "../../store/slices/taskSlice";
import {getUsers} from "../../store/slices/usersSlice";
import {getDepartments} from "../../store/slices/departmentsSlice";
import {getBlocks} from "../../store/slices/blocksSlice";
import {useUsers} from "../../hooks/use-users";
import Loading from "../Loading/Loading";
import {useNavigate} from "react-router-dom";

function ResultModal(props) {
    console.log(props)
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const blocks = useBlocks();
    const departments = useDepartments();
    const users = useUsers();

    useEffect(() => {
        dispatch(getBlocks());
        dispatch(getDepartments());
        dispatch(getUsers());
    }, []);

    if(!props.selectedResult)
        return;

    if (blocks.isLoading || departments.isLoading || users.isLoading)
        return <Loading/>

    let department = departments.departments.filter(department => department.id === props.selectedResult?.tasks[0].departamentId)[0]
    let bossID = department.bossId;
    let boss = users.users.filter(user => user.id === bossID)[0]

    const colors = {
        "Green": "#a9f26f",
        "Yellow": "#fff964",
        "Red": "#f27f6f"
    }

    return (
        <div className={s.resultModal}>
            <p className={s.title}>Подбор</p>
            <div className={s.tags}>
                <Tag>{props.selectedResult?.year}</Tag>
                <Tag>{props.selectedResult?.quarter + " квартал"}</Tag>
            </div>


            <div className={s.taskInformationTable}>
                <div className={s.taskInformationTableRow}>
                    <p className={s.taskInformationTableTitle}>Руководитель</p>
                    <p>
                        {
                            bossID ? boss.surname + " " + boss.name + " " + boss.patronymic : "-"
                        }
                    </p>
                </div>
                <div className={s.taskInformationTableRow}>
                    <p className={s.taskInformationTableTitle}>Отдел</p>
                    <p>
                        {
                            department.name
                        }
                    </p>
                </div>
                <div className={s.taskInformationTableRow}>
                    <p className={s.taskInformationTableTitle}>Задачи</p>
                    <div className={s.tasks}>
                        {
                            props.selectedResult?.tasks.map(task => {
                                return <p className={s.taskRow} onClick={() => {
                                    navigate("/task/"+task.id)
                                }}>{task.name}</p>
                            })
                        }
                    </div>
                </div>
                <div className={s.taskInformationTableRow}>
                    <p className={s.taskInformationTableTitle}>Итог</p>
                    <p className={s.result} style={{backgroundColor: colors[props.selectedResult?.color]}}>{props.selectedResult?.result}</p>
                </div>
            </div>


        </div>
    )
}

export default ResultModal;
