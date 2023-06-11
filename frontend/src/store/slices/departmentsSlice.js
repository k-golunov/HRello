import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import 'react-toastify/dist/ReactToastify.css';
import DEPARTMENTS_API from "../../api/departmentsAPI";

export const getDepartments = createAsyncThunk(
    'departments/get',
    async function (_, {rejectWithValue, dispatch}) {
        try {
            let response = await fetch(DEPARTMENTS_API.GET_DEPARTMENTS, {
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

            console.log(response)
            dispatch(setDepartments(response));

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    departments: [],
    isLoading: true,
};

const departmentsSlice = createSlice({
    name: 'departments',
    initialState: initialState,
    reducers: {
        setDepartments(state, action) {
            console.log(action)
            state.departments = action.payload;
            state.isLoading = false;
        },
        removeDepartments(state) {
            state.departaments = [];
            state.isLoading = true;
        },
    },
    extraReducers: {

    },
});
debugger;
export const {setDepartments, removeDepartments} = departmentsSlice.actions;

export default departmentsSlice.reducer;
