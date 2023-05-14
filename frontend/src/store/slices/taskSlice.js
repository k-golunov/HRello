import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import {useNavigate} from "react-router-dom";
import USER_API from '../../api/userAPI';
import {getProfile} from "./profileSlice";
import {togglePopup} from "./popupSlice";
import jwt from 'jwt-decode'
import {ToastContainer, toast} from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import TASK_API, {GET_TASK_URL, UPDATE_TASK_URL} from "../../api/taskAPI";

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


export const getTask = createAsyncThunk(
    'task/get',
    async function (taskId, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {

            let response = await fetch(TASK_API.GET_TASK_URL+taskId, {
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
            // response.accessToken = response.accessToken.accessToken;

            // debugger;
            console.log(response);
            dispatch(setTask({...response, id: taskId}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            //loginNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateTask = createAsyncThunk(
    'task/update',
    async function (task, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {

            let response = await fetch(TASK_API.UPDATE_TASK_URL, {
                method: 'put',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(task)
            });

            if (!response.ok) {
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.json();
            // response.accessToken = response.accessToken.accessToken;

            // debugger;
            console.log(response);
            //dispatch(setTask(response));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            //loginNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    id: null,
    name: null,
    year: null,
    quarter: null,
    category: null,
    block: null,
    plannedWeight: null,
    waitResult: null,
    taskStatus: null
};

const taskSlice = createSlice({
    name: 'task',
    initialState: initialState,
    reducers: {
        setTask(state, action) {
            console.log(action)
            state.id = action.payload.id
            state.name = action.payload.name
            state.year = action.payload.year
            state.quarter = action.payload.quarter
            state.category = action.payload.category
            state.block = action.payload.block
            state.plannedWeight = action.payload.plannedWeight
            state.waitResult = action.payload.waitResult
            state.taskStatus = action.payload.status
        },
        removeTask(state) {
            state.id = null
            state.name = null
            state.year = null
            state.quarter = null
            state.category = null
            state.block = null
            state.plannedWeight = null
            state.waitResult = null
            state.status = null
        },
    },
    extraReducers: {
        [getTask.pending]: (state, action) => {
            state.status = 'loading';
        },
        [getTask.fulfilled]: (state, action) => {
            state.status = 'resolved';
        },
        [getTask.rejected]: (state, action) => {
            state.status = 'rejected';
            state.error = action.payload;
        }
    },
});
debugger;
export const {setTask, removeTask} = taskSlice.actions;

export default taskSlice.reducer;
