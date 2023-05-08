import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import { useNavigate } from "react-router-dom";
import USER_API from '../../api/userAPI';
import {getProfile} from "./profileSlice";
import {togglePopup} from "./popupSlice";
import jwt from 'jwt-decode'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import TASK_API, {GET_ALL_TASKS_URL} from "../../api/taskAPI";

const createNotify = () => toast.success('🦄 Задача успешно создана!', {
    position: "bottom-right",
    autoClose: 3000,
    hideProgressBar: true,
    closeOnClick: true,
    pauseOnHover: true,
    draggable: true,
    progress: undefined,
    theme: "colored",
});

export const createTask = createAsyncThunk(
    'task/create',
    async function (task, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            console.log("CreateTask")
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            console.log(accessToken)
            let response = await fetch(TASK_API.CREATE_TASK_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(task),
            });

            if (!response.ok) {
                //alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.json();
            // debugger;
            console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const getAllTasks = createAsyncThunk(
    'tasks/get',
    async function (props, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            console.log("CreateTask")
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            console.log(accessToken)
            let response = await fetch(TASK_API.GET_ALL_TASKS_URL+props.page + (props.userID?"?User"+props.userID:""), {
                method: 'get',
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

            response = await response.json();
            // debugger;
            console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            dispatch(setTasks(response));
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    allTasksCount: 0,
    pagesCount: 0,
    tasks:[]
};

const tasksSlice = createSlice({
    name: 'tasks',
    initialState: initialState,
    reducers: {
        setTasks(state, action) {
            state.allTasksCount = action.payload.allTasksCount;
            state.pagesCount = action.payload.pagesCount;
            state.tasks = action.payload.tasks;
        },
        removeTasks(state) {
            state.allTasksCount = 0;
            state.pagesCount = 0;
            state.tasks = [];
        },
    },
    extraReducers: {
        [createTask.pending]: (state, action) => {
            state.status = 'loading';
        },
        [createTask.fulfilled]: (state, action) => {
            state.status = 'resolved';
        },
        [createTask.rejected]: (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        }
    },
});
debugger;
export const {setTasks, removeTasks} = tasksSlice.actions;

export default tasksSlice.reducer;
