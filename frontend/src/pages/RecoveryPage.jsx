import React from 'react';
import 'react-toastify/dist/ReactToastify.css';
import SendRecoveryForm from "../components/SendRecoveryForm/SendRecoveryForm";
import {useParams} from "react-router-dom";
import RecoveryForm from "../components/RecoveryForm/RecoveryForm";
import NotFoundLink from "./NotFoundLink";
import NotFoundPage from "./NotFoundPage";

const LoginPage = () => {
    const { userID } = useParams();
    if(!userID)
        return <NotFoundPage/>
    return (
        <div>
            <RecoveryForm userID={userID}/>
        </div>
    );
};

export default LoginPage;
