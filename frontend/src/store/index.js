import {configureStore} from '@reduxjs/toolkit';
import userReducer from './slices/userSlice';
import usersReducer from './slices/usersSlice';
import tasksReducer from './slices/tasksSlice';
import taskReducer from './slices/taskSlice';
import departmentsReducer from './slices/departmentsSlice';
import blocksReducer from './slices/blocksSlice';

export default configureStore({
    reducer: {
        user: userReducer,
        users: usersReducer,
        tasks: tasksReducer,
        task: taskReducer,
        departments: departmentsReducer,
        blocks: blocksReducer,
    },
});
