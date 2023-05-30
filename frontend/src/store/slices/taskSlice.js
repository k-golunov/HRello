import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import TASK_API, {
    CHANGE_TASK_STATUS_URL, COMPLETE_TASK_URL,
    GET_TASK_HISTORY_URL,
    GET_TASK_USER_URL,
    SEND_TASK_TO_REVIEW_URL
} from "../../api/taskAPI";
import {toast} from "react-toastify";

let createTaskToast;
let updateTaskToast;

export const getTask = createAsyncThunk(
    'task/get',
    async function (taskId, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            console.log("GET TASK")
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

export const getTaskUser = createAsyncThunk(
    'task/get/user',
    async function (userId, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {

            let response = await fetch(TASK_API.GET_TASK_USER_URL+userId, {
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
            dispatch(setTaskUser(response));
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

export const getTaskHistory = createAsyncThunk(
    'task/get/history',
    async function (taskId, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {

            let response = await fetch(TASK_API.GET_TASK_HISTORY_URL+taskId, {
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
            dispatch(setTaskHistory(response));
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

export const changeTaskStatus = createAsyncThunk(
    'task/changeStatus',
    async function (props, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(TASK_API.CHANGE_TASK_STATUS_URL, {
                method: 'patch',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(props),
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
            // debugger;
            //console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            //return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const sendToReviewTask = createAsyncThunk(
    'task/sendToReview',
    async function (props, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(TASK_API.SEND_TASK_TO_REVIEW_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(props),
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
            // debugger;
            //console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            //return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const completeTask = createAsyncThunk(
    'task/complete',
    async function (props, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(TASK_API.COMPLETE_TASK_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(props),
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
            // debugger;
            //console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getProfile());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            //return response;
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
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(TASK_API.UPDATE_TASK_URL, {
                method: 'put',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
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

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    id: null,
    departmentID: null,
    userID: null,
    userName: null,
    name: null,
    year: null,
    quarter: null,
    category: null,
    block: null,
    plannedWeight: null,
    waitResult: null,
    taskStatus: null,
    isLoading: true,
    history: [],
    userResult: null,
    bossResult: null
};

const taskSlice = createSlice({
    name: 'task',
    initialState: initialState,
    reducers: {
        setTask(state, action) {
            console.log("SET TASK ACTION", action)
            state.id = action.payload.id;
            state.departmentID = action.payload.departmentId;
            state.userID = action.payload.userId;
            state.name = action.payload.name;
            state.year = action.payload.year;
            state.quarter = action.payload.quarter;
            state.category = action.payload.category;
            state.block = action.payload.block;
            state.plannedWeight = action.payload.plannedWeight;
            state.waitResult = action.payload.waitResult;
            state.taskStatus = action.payload.status;
            state.userResult = action.payload.userResult;
            state.bossResult = action.payload.bossResult;
            state.isLoading = false;
        },
        removeTask(state) {
            state.id = null
            state.departmentID = null
            state.userID = null
            state.userName = null
            state.name = null
            state.year = null
            state.quarter = null
            state.category = null
            state.block = null
            state.plannedWeight = null
            state.waitResult = null
            state.status = null
            state.userResult = null;
            state.bossResult = null;
            state.isLoading = true;
            state.history = [];
        },
        setTaskUser(state, action) {
            console.log(action)
            state.userName = action.payload.surname + " " + action.payload.name + " " + action.payload.patronymic
        },
        setTaskHistory(state, action) {
            console.log(action)
            state.history = action.payload.allHistory;
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
        },
        [createTask.pending]: (state, action) => {
            createTaskToast = toast.loading("Добавляю задачу...")
        },
        [createTask.fulfilled]: (state, action) => {
            toast.update(createTaskToast,
                {
                    render: "Задача успешно добавлена",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [createTask.rejected]: (state, action) => {
            toast.update(createTaskToast,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [updateTask.pending]: (state, action) => {
            updateTaskToast = toast.loading("Редактирую задачу...")
        },
        [updateTask.fulfilled]: (state, action) => {
            toast.update(updateTaskToast,
                {
                    render: "Задача успешно отредактирована",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [updateTask.rejected]: (state, action) => {
            toast.update(updateTaskToast,
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
export const {setTask, removeTask, setTaskUser, setTaskHistory} = taskSlice.actions;

export default taskSlice.reducer;
