import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from 'react-redux';
import Button from '../components/Button/Button';
import {Navigate, useNavigate} from 'react-router-dom';
import {useAuth} from '../hooks/use-auth';
import PageTitle from "../components/PageTitle/PageTitle";
import Filters from "../components/Filters/Filters";
import Table1 from "../components/Table/Table";
import s from './Pages.module.css';
import Table from 'rc-table';
import TableRow from "../components/TableRow/TableRow";
import {useTasks} from "../hooks/use-tasks";
import {getAllTasks} from "../store/slices/tasksSlice";
import {removeTask} from "../store/slices/taskSlice";
import {getDepartments} from "../store/slices/departmentsSlice";
import {useDepartments} from "../hooks/use-departments";
import Loading from "../components/Loading/Loading";
import {useUsers} from "../hooks/use-users";
import {getUsers} from "../store/slices/usersSlice";

const WorkersPage = () => {
    const dispatch = useDispatch();
    const user = useAuth();
    const navigate = useNavigate();

    const departments = useDepartments();
    const users = useUsers();

    useEffect(() => {
        dispatch(getDepartments());
        dispatch(removeTask());
        dispatch(getUsers());
    }, []);

    const rolesList = [
        { value: 'worker', label: "Сотрудник"},
        { value: 'boss', label: "Руководитель"},
        { value: 'mainboss', label: "Главный руководитель"},
    ]


    const sortWeightList = [
        { value: 'desc', label: 'По возрастанию'},
        { value: 'asc', label: 'По убыванию'},
    ]

    let departmentFilter = []

    const block = {
        'Selection': 'Подбор',
        'Adaptation': 'Адаптация',
        'StaffDevelopment': 'Развитие персонала',
        'HRSupport': 'HR-сопровождение',
        'CorporateCulture': 'Корпоративная культура',
        'PersonnelAccountingAndSalary': 'Кадровый учет и з/п',
        'HRBrandExternal': 'HR-бренд внешний',
        'InternalWork': 'Внутренняя работа',
        'Estimation': 'Оценка'
    } //TODO

    // const roles = {
    //     'worker': "Сотрудник",
    //     'boss': 'Руководитель',
    //     'mainboss': "Главный руководитель"
    // }




    const [selectedDepartment, setSelectedDepartment] = useState([]);
    const [selectedRole, setSelectedRole] = useState([]);

    if(!departments.isLoading)
        departmentFilter = departments.departments.map(department =>{
            return { value: department.id, label: department.name}
        })

    if(departments.isLoading)
        return <Loading/>

    let workers = users.users.filter(worker => worker.name);

    const roles = selectedRole.filter(role => role.value).map(role => role.value)
    const departmentsFilter = selectedDepartment.filter(department => department.value).map(department => department.value)

    if(roles.length !== 0)
        workers = workers.filter(result => roles.includes(result.quarter))
    if(departmentsFilter.length !== 0)
        workers = workers.filter(worker => departmentsFilter.includes(worker.departamentId))

    const filters = [
        {
            'options': departmentFilter,
            'state': selectedDepartment,
            'setState': setSelectedDepartment,
            'placeholder': "Отдел",
            'isMulti': true,
            'minWidth': '200px'
        },
        // {
        //     'options': rolesList,
        //     'state': selectedRole,
        //     'setState': setSelectedRole,
        //     'placeholder': "Роль",
        //     'isMulti': true,
        //     'minWidth': '200px'
        // }
    ]

    const headers = [
        {type: "header", text: 'ФИО', alignment: "left", width: "400px"},
        {type: "header", text: 'Отдел', alignment: "left", width: "400px"},
        {type: "header", text: 'Роль', alignment: "left", width: "400px"},
    ]

    if(users.isLoading || departments.isLoading)
        return <Loading/>


    return (
        <>
            <PageTitle title="Сотрудники"/>
            <div className={s.workersPageFilters}>
                <Filters filters={filters}/>
                {
                    user.role !== "employee" ? <Button onClick={()=>navigate("/invitations")}>Пригласить нового сотрудника</Button> : <></>
                }

            </div>

            <TableRow cells={headers} isHeader/>
            {
                workers.length === 0 ?
                    <TableRow cells={[{type: "text", text: "Нет сотрудников!", alignment: "center", width: "1272px"}]}/> :
                    <></>
            }
            {
                workers.map(user => {
                    let cells = [
                        {type: "text", text: user.surname + " " + user.name + " " + user.patronymic, alignment: "left", width: "400px"},
                        {type: "text", text: departments.departments.filter(department => department.id === user.departamentId)[0].name, alignment: "left", width: "400px"}, //TODO
                        {type: "text", text: "В разработке", alignment: "left", width: "400px"}
                    ]
                    return <TableRow cells={cells}/>
                })
            }
            {/*<Table1 />*/}
            {/*<Table columns={columns} data={tasks} />*/}
        </>
    );
};

export default WorkersPage;
