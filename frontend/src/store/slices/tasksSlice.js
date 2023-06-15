import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import 'react-toastify/dist/ReactToastify.css';
import TASK_API from "../../api/taskAPI";
import {forEach} from "react-bootstrap/ElementChildren";
import RESULT_API from "../../api/resultAPI";


function downloadBlob(blob, name = 'file.txt') {
    if (
        window.navigator &&
        window.navigator.msSaveOrOpenBlob
    ) return window.navigator.msSaveOrOpenBlob(blob);

    // For other browsers:
    // Create a link pointing to the ObjectURL containing the blob.
    const data = window.URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = data;
    link.download = name;

    // this is necessary as link.click() does not work on the latest firefox
    link.dispatchEvent(
        new MouseEvent('click', {
            bubbles: true,
            cancelable: true,
            view: window
        })
    );

    setTimeout(() => {
        // For Firefox it is necessary to delay revoking the ObjectURL
        window.URL.revokeObjectURL(data);
        link.remove();
    }, 100);
}

export const getAllTasks = createAsyncThunk(
    'tasks/get',
    async function (props, {rejectWithValue, dispatch}) {
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            console.log("PROPS", props)
            let filter = [];
            if(props.year && props.year.length)
                filter.push("Year=" + props.year.join(", "))
            if(props.users && props.users.length)
                filter.push("User=" + props.users.join(", "))
            if(props.blocks && props.blocks.length)
                filter.push("Block=" + props.blocks.join(", "))
            if(props.departments && props.departments.length)
                filter.push("DepartmentId=" + props.departments.join(", "))
            if(props.quarter && props.quarter.length)
                filter.push("Quarter=" + props.quarter.join(", "))
            if(props.status && props.status.length)
                if(!Array.isArray(props.status[0]))
                    filter.push("Status=" + props.status.join(", "))
                else
                    filter.push("Status=" + props.status[0].join(", "))


            console.log("Filter", filter);

            let response = await fetch(TASK_API.GET_ALL_TASKS_URL+props.page + (filter.length !== 0 ?"?"+filter.join("&"):""), {
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

export const downloadTasks = createAsyncThunk(
    'tasks/download',
    async function (filters, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(
                TASK_API.DOWNLOAD_TASKS_URL,
                {
                    method: 'post',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: accessToken
                    },
                    body: JSON.stringify(filters)
                }
            );

            if (!response.ok) {
                //alert("Username or password is incorrect");
                throw new Error(
                    `${response.status}${
                        response.statusText ? ' ' + response.statusText : ''
                    }`
                );
            }

            response = await response.blob();
            downloadBlob(response, 'Задачи за '+filters.year+' год.xlsx')
            //dispatch(setUser({accessToken: response, email: user.email}));
            // dispatch(getResults());
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
    tasks:[],
    isLoading: true
};

const tasksSlice = createSlice({
    name: 'tasks',
    initialState: initialState,
    reducers: {
        setTasks(state, action) {

            state.allTasksCount = action.payload.allTasksCount;
            state.pagesCount = action.payload.pagesCount;
            state.tasks = action.payload.tasks;
            state.isLoading = false;
        },
        removeTasks(state) {
            state.allTasksCount = 0;
            state.pagesCount = 0;
            state.tasks = [];
            state.isLoading = false;
        },
        resetTasks(state) {
            state.isLoading = true;
            state.allTasksCount = 0;
            state.pagesCount = 0;
            state.tasks = [];
        },
    },
    extraReducers: {

    },
});
debugger;
export const {setTasks, removeTasks, resetTasks} = tasksSlice.actions;

export default tasksSlice.reducer;
