import React, {useEffect, useState} from 'react';
import { useForm } from 'react-hook-form';
import { useDispatch } from 'react-redux';
import Button from '../components/Button';
import FormFrame from '../components/FormFrame';
import { togglePopup } from '../store/slices/popupSlice';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import md5 from 'md5';
import { signInUser } from '../store/slices/userSlice';
import Input from "../components/Input/Input";
import {Form} from "react-bootstrap";
import AuthorizationForm from "../components/AuthorizationForm/AuthorizationForm";
import RegistrationForm from "../components/RegistrationForm/RegistrationForm";
import {useParams} from "react-router-dom";
import NotFoundLink from "./NotFoundLink";
import CreateTaskForm from "../components/CreateTaskForm/CreateTaskForm";
import Breadcrumbs from "../components/Breadcrumbs/Breadcrumbs";
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
