import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import USER_API, {DELETE_USER_URL, GET_USER_URL, SEND_RECOVERY_PASSWORD_URL} from '../../api/userAPI';
import jwt from 'jwt-decode'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {updateTask} from "./taskSlice";

let loginNotify;
let registrationNotify;
let createUserNotify;
let deleteUserNotify;

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
                //alert("Username or password is incorrect");
                if(response.status === 401)
                    throw new Error("Неверный логин или пароль");
                else
                    throw new Error("Ошибка сервера");
            }

            response = await response.json();

            console.log(response)
            dispatch(setUser({accessToken: response.accessToken, email: user.email}));

            //loginNotify();

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const getUserName = createAsyncThunk(
    'user/get/name',
    async function (userID, {rejectWithValue, dispatch}) {
        try {

            let response = await fetch(USER_API.GET_USER_URL+"/"+userID, {
                method: 'get',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (!response.ok) {
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.json();

            dispatch(setUserName(response));
            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const createUser = createAsyncThunk(
    'user/createUser',
    async function (user, {rejectWithValue, dispatch}) {
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

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const deleteUser = createAsyncThunk(
    'user/delete',
    async function (userID, {rejectWithValue, dispatch}) {
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('accessToken');
            let response = await fetch(USER_API.DELETE_USER_URL+userID, {
                method: 'delete',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                }
            });

            if (!response.ok) {
                //alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            //response = await response.json();
            //console.log(response)

            //return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const recoveryUserPassword = createAsyncThunk(
    'user/sendRecovery',
    async function (email, {rejectWithValue, dispatch}) {
        try {
            let response = await fetch(USER_API.SEND_RECOVERY_PASSWORD_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(email),
            });

            if (!response.ok) {
                //alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            //response = await response.json();
            //console.log(response)

            //return response;
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

            //response = await response.json();

            //dispatch(signInUser(user));
            // dispatch(togglePopup("signUp"));
            //registrationNotify();

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
    departmentID: null,
    role: null,
    name: null,
    surname: null,
    patronymic: null,

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
            state.id = jwt(action.payload.accessToken).userId;
            state.role = jwt(action.payload.accessToken).Role;
            state.departmentID = parseInt(jwt(action.payload.accessToken).DepartmentId);

            localStorage.setItem('USSCHR-accessToken', action.payload.accessToken);
            localStorage.setItem('USSCHR-userId', state.id);
            localStorage.setItem('USSCHR-email', action.payload.email);
            localStorage.setItem('USSCHR-departmentID', state.departmentID);
            localStorage.setItem('USSCHR-role', state.role);
        },
        setUserName(state, action) {
            state.name = action.payload.name;
            state.surname = action.payload.surname;
            state.patronymic = action.payload.patronymic;
        },
        removeUser(state) {
            state.id = null;
            state.email = null;
            state.accessToken = null;
            state.departmentID = null;
            state.role = null;
            localStorage.removeItem('USSCHR-accessToken');
            localStorage.removeItem('USSCHR-userId');
            localStorage.removeItem('USSCHR-email');
            localStorage.removeItem('USSCHR-departmentID');
            localStorage.removeItem('USSCHR-role');
        },
    },
    extraReducers: {
        [signUpUser.pending]: (state, action) => {
            registrationNotify = toast.loading("Регистрирую пользователя...")
        },
        [signUpUser.fulfilled]: (state, action) => {
            toast.update(registrationNotify,
                {
                    render: "Вы успешно зарегистрировались!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [signUpUser.rejected]: (state, action) => {
            toast.update(registrationNotify,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [createUser.pending]: (state, action) => {
            createUserNotify = toast.loading("Отправляю приглашение пользователю...")
        },
        [createUser.fulfilled]: (state, action) => {
            toast.update(createUserNotify,
                {
                    render: "Приглашение успешно отправлено!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [createUser.rejected]: (state, action) => {
            toast.update(createUserNotify,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [signInUser.pending]: (state, action) => {
            loginNotify = toast.loading("Вхожу в систему...")
        },
        [signInUser.fulfilled]: (state, action) => {
            toast.update(loginNotify,
                {
                    render: "Вы успешно вошли в систему",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [signInUser.rejected]: (state, action) => {
            toast.update(loginNotify,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },

        [deleteUser.pending]: (state, action) => {
            deleteUserNotify = toast.loading("Аннулирую приглашение...")
        },
        [deleteUser.fulfilled]: (state, action) => {
            toast.update(deleteUserNotify,
                {
                    render: "Приглашение успешно аннулировано!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [deleteUser.rejected]: (state, action) => {
            toast.update(deleteUserNotify,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
    },
});
debugger;
export const {setUser, removeUser, setUserName} = userSlice.actions;

export default userSlice.reducer;
