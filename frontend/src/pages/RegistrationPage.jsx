import React from 'react';
import { useDispatch } from 'react-redux';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import RegistrationForm from "../components/RegistrationForm/RegistrationForm";
import {useParams} from "react-router-dom";
import NotFoundLink from "./NotFoundLink";

const RegistrationPage = () => {
    const dispatch = useDispatch();

    const { link } = useParams();

    if(!link)
        return <NotFoundLink/>

    return (
        <div>
            <RegistrationForm userId={link}/>
            <ToastContainer />
        </div>
    );
};

export default RegistrationPage;
