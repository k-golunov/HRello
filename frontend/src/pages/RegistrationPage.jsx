import React, {useEffect} from 'react';
import { useDispatch } from 'react-redux';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import RegistrationForm from "../components/RegistrationForm/RegistrationForm";
import {useParams} from "react-router-dom";
import NotFoundLink from "./NotFoundLink";
import {getAllTasks, resetTasks} from "../store/slices/tasksSlice";
import {getDepartments} from "../store/slices/departmentsSlice";
import {getBlocks} from "../store/slices/blocksSlice";
import {getUsers} from "../store/slices/usersSlice";
import {removeTask} from "../store/slices/taskSlice";
import {checkInvitation} from "../store/slices/userSlice";
import {useAuth} from "../hooks/use-auth";
import Loading from "../components/Loading/Loading";

const RegistrationPage = () => {
    const dispatch = useDispatch();
    const user = useAuth();

    const { link } = useParams();

    useEffect(() => {
        dispatch(checkInvitation(link));
    }, []);

    if(user.isLoading)
        return <Loading/>

    if(!link || !user.inviteStatus)
        return <NotFoundLink/>

    return (
        <div>
            <RegistrationForm userId={link}/>
        </div>
    );
};

export default RegistrationPage;
