import React from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AuthorizationForm from "../components/AuthorizationForm/AuthorizationForm";

const LoginPage = () => {
    return (
        <div>
            <AuthorizationForm/>
        </div>
    );
};

export default LoginPage;
