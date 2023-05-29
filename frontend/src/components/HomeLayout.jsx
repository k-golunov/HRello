import React from 'react';
import {Outlet} from 'react-router-dom';
import Footer from './Footer/Footer';
import Header from './Header/Header';

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const HomeLayout = () => {
    return (
        <>
            <Header/>
            <Outlet/>
            {/*<PageTitle/>*/}
        </>
    );
};

export default HomeLayout;
