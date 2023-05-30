import React, {useEffect} from 'react';
import { useDispatch } from 'react-redux';
import 'react-toastify/dist/ReactToastify.css';
import CreateTaskForm from "../components/CreateTaskForm/CreateTaskForm";

const CreateTaskPage = () => {
    return (
        <div>
            <CreateTaskForm/>
        </div>
    );
};

export default CreateTaskPage;
