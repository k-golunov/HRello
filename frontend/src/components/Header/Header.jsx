import React from 'react';
import s from './Header.module.css';
import Nav from './Nav';
import NavLogo from './NavLogo';
import NavAuth from './NavAuth';
import {Link} from "react-router-dom";
import classNames from "classnames/bind";

const Header = (props) => {
    return (
        <div className={s.header}>
            <div className={s.headerContainer}>
                <div className={s.header_wrapper}>
                    <NavLogo/>
                    <Nav/>
                    <NavAuth/>
                </div>
            </div>
            {
                props.withSecond ?
                    <div className={s.headerContainer}>
                        <div className={s.secondHeader}>
                            <Link className={classNames(s.secondHeaderLink, props.page==='myTasks'?s.active:"")} to='/tasks/my'>Мои задачи</Link>
                            <Link className={classNames(s.secondHeaderLink, props.page==='allTasks'?s.active:"")} to='/tasks/all'><div >Все задачи</div></Link>
                        </div>
                    </div>
                    :<></>
            }
        </div>
    );
};

export default Header;
