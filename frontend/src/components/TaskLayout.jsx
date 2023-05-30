import React, {useEffect} from 'react';
import {Outlet} from 'react-router-dom';
import Header from './Header/Header';
import 'react-toastify/dist/ReactToastify.css';
import WidthContent from "./WidthContent/WidthContent";

const TaskLayout = (props) => {
    return (
        <>
            <Header/>
            <WidthContent>
                <Outlet/>
            </WidthContent>
            {/*<PageTitle/>*/}
        </>
    );
};

export default TaskLayout;
