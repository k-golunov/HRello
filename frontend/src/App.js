import React from 'react';
import './App.scss';
import {BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import NotFoundPage from './pages/NotFoundPage';
import ProfilePage from './pages/ProfilePage';
import HomeLayout from './components/HomeLayout';
import ProfileLayout from './components/ProfileLayout';
import TaskPage1 from './pages/TaskPage1';
import RequireAuth from './hoc/RequireAuth';
import DirectionsPage from './pages/DirectionsPage';
import ApplicationsPage from './pages/ApplicationsPage';
import {TestCaseSentOK} from './components/TestCaseSentOK';
import AdminApplicationsPage from './pages/AdminApplicationsPage';
import AdminSingleApplicationPage from './pages/AdminSingleApplicationPage';
import AdminAddingDirectionPage from './pages/AdminAddingDirectionPage';
import AdminAddingTestCasePage from "./pages/AdminAddingTestCasePage";
import RequireAdmin from "./hoc/RequireAdmin";
import AdminDirectionsPage from "./pages/AdminDirectionsPage";
import AdminTestCasesPage from "./pages/AdminTestCasesPage";
import AdminSingleTestPage from "./pages/AdminSingleTestPage";
import AdminPracticantsPage from "./pages/AdminPracticantsPage";
import AdminSinglePracticantPage from "./pages/AdminSinglePracticantPage";
import Redirection from "./hoc/Redirection";
import RequireUnauth from "./hoc/RequireUnauth";
import RegistrationPage from "./pages/RegistrationPage";
import InvitationsPage from "./pages/InvitationsPage";
import NotFoundLink from "./pages/NotFoundLink";
import MyTaskPage from "./pages/MyTaskPage";
import AllTaskPage from "./pages/AllTaskPage";
import TasksLayout from "./components/TasksLayout";
import CreateTaskPage from "./pages/CreateTaskPage";
import TaskLayout from "./components/TaskLayout";
import TaskPage from "./pages/TaskPage";
import EditTaskPage from "./pages/EditTaskPage";

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
                    <Route path='cardpop' element={<TestCaseSentOK/>}/>
                    <Route path='*' element={<NotFoundPage/>}/>
                </Route>

                <Route path='/tasks' element={<TasksLayout page='myTasks'/>}>
                    <Route path='my' element={
                        // <RequireAuth>
                        <MyTaskPage/>
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

                <Route path='/task' element={<TaskLayout />}>
                    <Route path=':taskId' element={
                        // <RequireAuth>
                        <TaskPage/>
                        // </RequireAuth>
                    }
                    />
                    <Route path=':taskId/edit' element={
                        // <RequireAuth>
                        <EditTaskPage/>
                        // </RequireAuth>
                    }
                    />
                </Route>


                <Route
                    path='/home'
                    element={
                        <ProfileLayout/>
                    }
                >
                    <Route index element={<HomePage/>}/>
                </Route>

                <Route
                    path='/profile'
                    element={
                        <ProfileLayout/>
                    }
                >
                    <Route index element={<ProfilePage/>}/>
                </Route>

                {/*<Route*/}
                {/*    path='/task/:userId/:taskId'*/}
                {/*    element={*/}
                {/*        <RequireAuth>*/}
                {/*            <ProfileLayout/>*/}
                {/*        </RequireAuth>*/}
                {/*    }*/}
                {/*>*/}
                {/*    <Route index element={<TaskPage1/>}/>*/}
                {/*</Route>*/}


                <Route
                    path='/directions'
                    element={
                        <RequireAuth>
                            <ProfileLayout/>
                        </RequireAuth>
                    }
                >
                    <Route index element={<DirectionsPage/>}/>
                </Route>


                <Route
                    path='/applications'
                    element={
                        <RequireAuth>
                            <ProfileLayout/>
                        </RequireAuth>
                    }
                >
                    <Route index element={<ApplicationsPage/>}/>
                </Route>

                {/*<Route index element={<ApplicationsPage />} />*/}
                <Route path='/admin/' element={<ProfileLayout/>}>
                    <Route
                        path='directions/'
                        element={<RequireAdmin><AdminDirectionsPage/></RequireAdmin>}
                    />

                    <Route
                        path='applications/'
                        element={<RequireAdmin><AdminApplicationsPage/></RequireAdmin>}
                    />

                    <Route
                        path='practicants/'
                        element={<RequireAdmin><AdminPracticantsPage/></RequireAdmin>}
                    />

                    <Route
                        path='practicant/:userId'
                        element={<RequireAdmin><AdminSinglePracticantPage/></RequireAdmin>}
                    />


                    <Route
                        path='application/:userId'
                        element={<RequireAdmin><AdminSingleApplicationPage/></RequireAdmin>}
                    />

                    <Route
                        path='testcases/'
                        element={<RequireAdmin><AdminTestCasesPage/></RequireAdmin>}
                    />

                    <Route
                        path='test/:userId/:testId'
                        element={<RequireAdmin><AdminSingleTestPage/></RequireAdmin>}
                    />

                    <Route path='create/'>
                        <Route path='direction/' element={<RequireAdmin><AdminAddingDirectionPage/></RequireAdmin>}/>
                        <Route path='testcase/:testId' element={<RequireAdmin><AdminAddingTestCasePage/></RequireAdmin>}/>
                    </Route>
                </Route>
            </Routes>
        </Router>
    );
}

export default App;
