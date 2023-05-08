import React, {useEffect} from 'react';
import {Outlet} from 'react-router-dom';
import Header from './Header/Header';
import {useDispatch} from 'react-redux';
import {getProfile} from '../store/slices/profileSlice';
import Footer from "./Footer/Footer"

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import WidthContent from "./WidthContent/WidthContent";

const ProfileLayout = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(getProfile());
    }, [dispatch]);

    return (
        <>
            <Header/>
            <WidthContent>
                <Outlet/>
            </WidthContent>

            <ToastContainer
                position="bottom-right"
                autoClose={3000}
                hideProgressBar
                newestOnTop={false}
                closeOnClick
                rtl={false}
                pauseOnFocusLoss={false}
                draggable
                pauseOnHover
                theme="colored"
            />
            {/*<PageTitle/>*/}
        </>
    );
};

export default ProfileLayout;
