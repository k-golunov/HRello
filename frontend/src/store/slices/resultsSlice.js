import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import TASK_API, {
    CHANGE_TASK_STATUS_URL, COMPLETE_TASK_URL,
    GET_TASK_HISTORY_URL,
    GET_TASK_USER_URL,
    SEND_TASK_TO_REVIEW_URL
} from "../../api/taskAPI";
import {toast} from "react-toastify";
import RESULT_API, {DOWNLOAD_RESULTS_URL} from "../../api/resultAPI";

let createResultToast;
let deleteResultToast;

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


export const getResults = createAsyncThunk(
    'results/get',
    async function (_, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(RESULT_API.GET_RESULTS_URL, {
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


            //dispatch(setUser({accessToken: response, email: user.email}));
            dispatch(setResults(response));
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const createResult = createAsyncThunk(
    'result/create',
    async function (result, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(RESULT_API.CREATE_RESULT_URL, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(result),
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
            dispatch(getResults());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const deleteResult = createAsyncThunk(
    'result/delete',
    async function (resultID, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(RESULT_API.DELETE_RESULT_URL+"?id="+resultID, {
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

            // response = await response.json();
            // debugger;
            // console.log(response)
            //dispatch(setUser({accessToken: response, email: user.email}));
            dispatch(getResults());
            // dispatch(togglePopup("signIn"));
            // createNotify();

            // navigate(`/profile`);

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);


export const downloadResult = createAsyncThunk(
    'results/download',
    async function (filters, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(
                RESULT_API.DOWNLOAD_RESULTS_URL + "?Year="+filters.year+"&Quarters="+filters.quarter+"&DepartmentsId="+filters.department ,
                {
                    method: 'get',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: accessToken
                    }
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
            downloadBlob(response, 'Итоги за '+filters.year+' год.xlsx')
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
    results: [],
    isLoading: true,
};

const resultsSlice = createSlice({
    name: 'results',
    initialState: initialState,
    reducers: {
        setResults(state, action) {
            state.results = action.payload;
            state.isLoading = false;
        },
        removeResults(state) {
            state.results = []
            state.isLoading = true;
        }
    },
    extraReducers: {
        [createResult.pending]: (state, action) => {
            createResultToast = toast.loading("Добавляю итог...")
        },
        [createResult.fulfilled]: (state, action) => {
            toast.update(createResultToast,
                {
                    render: "Итог успешно добавлен",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [createResult.rejected]: (state, action) => {
            toast.update(createResultToast,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [deleteResult.pending]: (state, action) => {
            deleteResultToast = toast.loading("Удаляю итог...")
        },
        [deleteResult.fulfilled]: (state, action) => {
            toast.update(deleteResultToast,
                {
                    render: "Итог успешно удалён",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [deleteResult.rejected]: (state, action) => {
            toast.update(deleteResultToast,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        }
    },
});
debugger;
export const {setResults, removeResults} = resultsSlice.actions;

export default resultsSlice.reducer;
