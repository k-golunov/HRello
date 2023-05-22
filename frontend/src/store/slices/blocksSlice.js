import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import 'react-toastify/dist/ReactToastify.css';
import BLOCKS_API from "../../api/blocksAPI";

export const getBlocks = createAsyncThunk(
    'blocks/get',
    async function (_, {rejectWithValue, dispatch}) {
        try {
            let response = await fetch(BLOCKS_API.GET_BLOCKS, {
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
            dispatch(setBlocks(response));

            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

const initialState = {
    blocks: [],
    isLoading: true,
};

const blocksSlice = createSlice({
    name: 'blocks',
    initialState: initialState,
    reducers: {
        setBlocks(state, action) {
            state.blocks = action.payload;
            state.isLoading = false;
        },
        removeBlocks(state) {
            state.blocks = [];
            state.isLoading = true;
        },
    },
    extraReducers: {

    },
});
debugger;
export const {setBlocks, removeBlocks} = blocksSlice.actions;

export default blocksSlice.reducer;
