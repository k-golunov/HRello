import React, {useEffect, useState} from 'react';
import { useDispatch } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import {useParams} from "react-router-dom";
import {useTask} from "../hooks/use-task";
import {getTask, getTaskHistory} from "../store/slices/taskSlice";
import EditTaskForm from "../components/EditTaskForm/EditTaskForm";
import Loading from "../components/Loading/Loading";
import History from "../components/History/History";

const EditTaskPage = () => {
    const { taskId } = useParams();
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getTask(taskId))
        dispatch(getTaskHistory(taskId))
    }, []);

    const task = useTask();

    if(task.isLoading)
        return <Loading />

    return (
        <div>
            <EditTaskForm task={task}/>
            <History history={task.history}/>
            {/*<ToastContainer />*/}
        </div>
    );
};

export default EditTaskPage;
