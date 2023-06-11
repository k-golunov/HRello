import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import TASK_API, {
    CHANGE_TASK_STATUS_URL, COMPLETE_TASK_URL,
    GET_TASK_HISTORY_URL,
    GET_TASK_USER_URL,
    SEND_TASK_TO_REVIEW_URL
} from "../../api/taskAPI";
import {toast} from "react-toastify";
import RESULT_API from "../../api/resultAPI";

let createResultToast;

export const getResults = createAsyncThunk(
    'results/get',
    async function (_, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(RESULT_API.CREATE_RESULT_URL, {
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

const initialState = {
    results: [],
    isLoading: true,
};

const resultSlice = createSlice({
    name: 'results',
    initialState: initialState,
    reducers: {
        setResults(state, action) {
            state.results = action.payload.allTaskResultResponse;
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
        }
    },
});
debugger;
export const {setResults, removeResults} = resultSlice.actions;

export default resultSlice.reducer;
