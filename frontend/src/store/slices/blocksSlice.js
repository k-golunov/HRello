import {createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import 'react-toastify/dist/ReactToastify.css';
import BLOCKS_API, {CREATE_BLOCK, DELETE_BLOCK} from "../../api/blocksAPI";
import TASK_API from "../../api/taskAPI";
import {toast} from "react-toastify";
import {updateTask} from "./taskSlice";

let createBlockToast;
let editBlockToast;
let deleteBlockToast;
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

export const createBlock = createAsyncThunk(
    'block/create',
    async function (block, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(BLOCKS_API.CREATE_BLOCK, {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(block),
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


            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const editBlock = createAsyncThunk(
    'block/edit',
    async function (block, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(BLOCKS_API.EDIT_BLOCK, {
                method: 'put',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: accessToken
                },
                body: JSON.stringify(block),
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


            return response;
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const deleteBlock = createAsyncThunk(
    'block/delete',
    async function (blockID, {rejectWithValue, dispatch}) {
        // let navigate = useNavigate();
        try {
            const accessToken = 'Bearer ' + localStorage.getItem('USSCHR-accessToken')
            let response = await fetch(BLOCKS_API.DELETE_BLOCK+blockID, {
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
            state.blocks = action.payload.allBlocks;
            state.isLoading = false;
        },
        removeBlocks(state) {
            state.blocks = [];
            state.isLoading = true;
        },
    },
    extraReducers: {
        [createBlock.pending]: (state, action) => {
            createBlockToast = toast.loading("Добавляю блок...")
        },
        [createBlock.fulfilled]: (state, action) => {
            toast.update(createBlockToast,
                {
                    render: "Блок успешно добавлен!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [createBlock.rejected]: (state, action) => {
            toast.update(createBlockToast,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [editBlock.pending]: (state, action) => {
            editBlockToast = toast.loading("Редактирую блок...")
        },
        [editBlock.fulfilled]: (state, action) => {
            toast.update(editBlockToast,
                {
                    render: "Блок успешно отредактирован!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [editBlock.rejected]: (state, action) => {
            toast.update(editBlockToast,
                { render: action.payload,
                    type: "error",
                    isLoading: false,
                    autoClose: 10000,
                }
            );
        },
        [deleteBlock.pending]: (state, action) => {
            deleteBlockToast = toast.loading("Удаляю блок...")
        },
        [deleteBlock.fulfilled]: (state, action) => {
            toast.update(deleteBlockToast,
                {
                    render: "Блок успешно удалён!",
                    type: "success",
                    isLoading: false,
                    autoClose: 4000,
                    hideProgressBar: false
                });
        },
        [deleteBlock.rejected]: (state, action) => {
            toast.update(deleteBlockToast,
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
export const {setBlocks, removeBlocks} = blocksSlice.actions;

export default blocksSlice.reducer;
