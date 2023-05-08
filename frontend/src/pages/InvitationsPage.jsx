import React from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AuthorizationForm from "../components/AuthorizationForm/AuthorizationForm";
import PageTitle from "../components/PageTitle/PageTitle";
import InvitationForm from "../components/InvitationForm/InvitationForm";

const LoginPage = () => {
    return (
        <div>
            <PageTitle title="Приглашения"/>
            <InvitationForm/>
            <ToastContainer />
        </div>
    );
};

export default LoginPage;
