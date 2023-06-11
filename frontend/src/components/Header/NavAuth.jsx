import React, {useEffect} from 'react';
import s from './Header.module.css';
import {useDispatch} from 'react-redux';
import {useAuth} from '../../hooks/use-auth';
import {getUserName, removeUser} from '../../store/slices/userSlice';
import { useNavigate } from 'react-router-dom';

const NavAuth = () => {
    let navigate = useNavigate();
    const dispatch = useDispatch();

    const user = useAuth();

    useEffect(() => {
        if(user.id)
            dispatch(getUserName(user.id));
    }, []);

    const logout = () => {
        debugger
        dispatch(removeUser());
        navigate("/");
    };

    if (user.isAuth) {
        return (
            <div className={s.nav_auth}>
                <a className={s.nav_item1}>
                    {user.surname && user.name
                        ? `${user.surname} ${user.name}`
                        : 'Аноним'}
                </a>
                <span className={s.sep}/>
                <div className={s.nav_item} onClick={logout}>Выйти</div>
            </div>
        );
    }

    return (
        <div className={s.nav_auth}>
            {/*<Link to={"/login"}><a className={s.nav_item} >Войти</a></Link>*/}
            {/*<span className={s.sep}></span>*/}
            {/*<Link to={"/registration"}><a className={s.nav_item}>Зарегистрироваться</a></Link>*/}
        </div>
    );
};

export default NavAuth;
