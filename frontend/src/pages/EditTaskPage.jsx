import React, {useEffect, useState} from 'react';
import { useDispatch } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import {useParams} from "react-router-dom";
import {useTask} from "../hooks/use-task";
import {getTask} from "../store/slices/taskSlice";
import EditTaskForm from "../components/EditTaskForm/EditTaskForm";

const EditTaskPage = () => {
    const { taskId } = useParams();
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getTask(taskId))
    }, []);

    const task = useTask();

    return (
        <div>
            <EditTaskForm task={task}
            />
            {/*<ToastContainer />*/}
        </div>
    );
};

export default EditTaskPage;
