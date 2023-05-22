import React from 'react';
import './App.scss';
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import NotFoundPage from './pages/NotFoundPage';
import HomeLayout from './components/HomeLayout';
import Redirection from "./hoc/Redirection";
import RequireUnauth from "./hoc/RequireUnauth";
import RegistrationPage from "./pages/RegistrationPage";
import InvitationsPage from "./pages/InvitationsPage";
import NotFoundLink from "./pages/NotFoundLink";
import MyTasksPage from "./pages/MyTasksPage";
import AllTaskPage from "./pages/AllTasksPage";
import TasksLayout from "./components/TasksLayout";
import CreateTaskPage from "./pages/CreateTaskPage";
import TaskLayout from "./components/TaskLayout";
import TaskPage from "./pages/TaskPage";
import EditTaskPage from "./pages/EditTaskPage";
import EndingTaskPage from "./pages/EndingTaskPage";
import WorkersPage from "./pages/WorkersPage";
import DepartmentsPage from "./pages/DepartmentsPage";
import OnCheckTaskPage from "./pages/OnCheckTaskPage";
import WorkersLayout from "./components/WorkersLayout";

function App() {

    return (
        <Router>
            <Routes>
                <Route path='/' element={<HomeLayout/>}>
                    <Route index element={<Redirection/>}/>
                    <Route path='login' element={
                        <RequireUnauth>
                            <LoginPage/>
                        </RequireUnauth>}/>
                    <Route path='registration/:link' element={
                        <RequireUnauth>
                            <RegistrationPage/>
                        </RequireUnauth>}/>
                    <Route path='registration/' element={
                            <NotFoundLink/>}/>
                    <Route path='invitations' element={<InvitationsPage/>}/>
                    <Route path='*' element={<NotFoundPage/>}/>
                </Route>

                <Route path='/tasks' element={<TasksLayout page='myTasks'/>}>
                    <Route path='my' element={
                        // <RequireAuth>
                        <MyTasksPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>

                <Route path='/tasks' element={<TasksLayout page='onCheck'/>}>
                    <Route path='onCheck' element={
                        // <RequireAuth>
                        <OnCheckTaskPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>

                <Route path='/tasks' element={<TasksLayout page='allTasks'/>}>
                    <Route path='all' element={
                        // <RequireAuth>
                        <AllTaskPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>

                <Route path='/tasks' element={<TaskLayout />}>
                    <Route path='create' element={
                        // <RequireAuth>
                        <CreateTaskPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>

                <Route path='/workers' element={<WorkersLayout page='workers'/>}>
                    <Route path='' element={
                        // <RequireAuth>
                        <WorkersPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>
                <Route path='/departments' element={<WorkersLayout page='departments' />}>
                    <Route path='' element={
                        // <RequireAuth>
                        <DepartmentsPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>

                <Route path='/task' element={<TaskLayout />}>
                    <Route path=':taskId' element={
                        // <RequireAuth>
                        <TaskPage/>
                        // </RequireAuth>
                    }/>

                    <Route path=':taskId/edit' element={
                        // <RequireAuth>
                        <EditTaskPage/>
                        // </RequireAuth>
                    }/>

                    <Route path=':taskId/ending' element={
                        // <RequireAuth>
                        <EndingTaskPage/>
                        // </RequireAuth>
                    }/>
                </Route>
            </Routes>
        </Router>
    );
}

export default App;
