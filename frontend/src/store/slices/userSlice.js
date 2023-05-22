import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import USER_API, {GET_USER_URL} from '../../api/userAPI';
import jwt from 'jwt-decode'
import { toast } from 'react-toastify';
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

            response = await response.json();

            console.log(response)
            dispatch(setUser({accessToken: response.accessToken, email: user.email}));

            loginNotify();

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
export const {setUser, removeUser, setUserName} = userSlice.actions;

export default userSlice.reducer;
