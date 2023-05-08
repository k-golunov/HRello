import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import { useNavigate } from "react-router-dom";
import USER_API from '../../api/userAPI';
import {getProfile} from "./profileSlice";
import {togglePopup} from "./popupSlice";
import jwt from 'jwt-decode'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const loginNotify = () => toast.success('🦄 Вы успешно авторизовались!', {
    position: "bottom-right",
    autoClose: 3000,
    hideProgressBar: true,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "colored",
});

const createNotify = () => toast.success('🦄 Приглашение успешно отправлено!', {
    position: "bottom-right",
    autoClose: 3000,
    hideProgressBar: true,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "colored",
});

const registrationNotify = () => toast.success('🦄 Вы успешно зарегестрировались в системе!', {
        position: "bottom-right",
        autoClose: 3000,
        hideProgressBar: true,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "colored",
    });

export const signInUser = createAsyncThunk(
    'user/signIn',
    async function (user, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {

            let response = await fetch(USER_API.SIGN_IN_USER_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(user),
            });

            if (!response.ok) {
                alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.text();
            // response.accessToken = response.accessToken.accessToken;

            // debugger;
            console.log(response)
            dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            loginNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const createUser = createAsyncThunk(
    'user/createUser',
    async function (user, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('accessToken');
            let response = await fetch(USER_API.CREATE_USER_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(user),
            });

            if (!response.ok) {
                //alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.text();
            // debugger;
            console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const signUpUser = createAsyncThunk(
    'user/signUp',
    async function (user, {rejectWithValue, dispatch}) {
        try {
            let response = await fetch(USER_API.SIGN_UP_USER_URL+'?userId='+user.userId, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(user),
            });
            debugger;
            if (!response.ok) {
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.json();

            // dispatch(signInUser(user));
            // dispatch(togglePopup("signUp"));
            registrationNotify();

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    id: null,
    email: null,
    accessToken: null,
    role: null,
    // id: null,
    status: null,
    error: null,
};

const userSlice = createSlice({
    name: 'user',
    initialState: initialState,
    reducers: {
        setUser(state, action) {
            console.log(action)
            state.email = action.payload.email;
            // state.id = action.payload.id;
            state.accessToken = action.payload.accessToken;
            state.id = jwt(action.payload.accessToken).Id;
            state.role = jwt(action.payload.accessToken).Role;
            // state.role = action.payload.role;
            // const accessToken = action.payload.accessToken.json();

            localStorage.setItem('USSCHR-accessToken', action.payload.accessToken);
            localStorage.setItem('USSCHR-userId', state.id);
            // localStorage.setItem('userId', action.payload.id);
            localStorage.setItem('USSCHR-email', action.payload.email);
            // localStorage.setItem('role', action.payload.role);
        },
        removeUser(state) {
            state.email = null;
            state.id = null;
            state.accessToken = null;
            // state.role = null;
            localStorage.removeItem('USSCHR-accessToken');
            localStorage.removeItem('USSCHR-userId');
            // localStorage.removeItem('userId');
            localStorage.removeItem('USSCHR-email');
            // localStorage.removeItem('role');
        },
    },
    extraReducers: {
        [signInUser.pending]: (state, action) => {
            state.status = 'loading';
        },
        [signInUser.fulfilled]: (state, action) => {
            state.status = 'resolved';
        },
        [signInUser.rejected]: (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        },
        [signUpUser.pending]: (state, action) => {
        },
        [signUpUser.fulfilled]: (state, action) => {
        },
        [signUpUser.rejected]: (state, action) => {
        },
        [createUser.pending]: (state, action) => {
        },
        [createUser.fulfilled]: (state, action) => {
        },
        [createUser.rejected]: (state, action) => {
        },
    },
});
debugger;
export const {setUser, removeUser} = userSlice.actions;

export default userSlice.reducer;
