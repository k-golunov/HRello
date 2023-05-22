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

const WorkersPage = () => {
    const dispatch = useDispatch();
    const isAuth = useAuth().isAuth;
    const navigate = useNavigate();

    const departments = useDepartments();

    useEffect(() => {
        dispatch(getDepartments());
        dispatch(removeTask());
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

    const roles = {
        'worker': "Сотрудник",
        'boss': 'Руководитель',
        'mainboss': "Главный руководитель"
    }




    const [selectedDepartment, setSelectedDepartment] = useState([]);
    const [selectedRole, setSelectedRole] = useState([]);

    if(!departments.isLoading)
        departmentFilter = departments.departments.map(department =>{
            return { value: department.id, label: department.name}
        })

    if(departments.isLoading)
        return <Loading/>

    const filters = [
        {
            'options': departmentFilter,
            'state': selectedDepartment,
            'setState': setSelectedDepartment,
            'placeholder': "Отдел",
            'isMulti': true,
            'minWidth': '200px'
        },
        {
            'options': rolesList,
            'state': selectedRole,
            'setState': setSelectedRole,
            'placeholder': "Роль",
            'isMulti': true,
            'minWidth': '200px'
        }
    ]

    const headers = [
        {type: "header", text: 'ФИО', alignment: "left", width: "400px"},
        {type: "header", text: 'Отдел', alignment: "left", width: "400px"},
        {type: "header", text: 'Роль', alignment: "left", width: "400px"},
    ]

    const workers = [
        {name: "Кочнева Анжелика Дмитриевна", department: "Selection", role: 'boss'},
        {name: "Бурханова Екатерина Сергеевна", department: "InternalWork", role: 'boss'},
        {name: "Астафьева Анна Викторовна", department: "Selection", role: 'boss'},
    ]


    return (
        <>
            <PageTitle title="Сотрудники"/>
            <div className={s.workersPageFilters}>
                <Filters filters={filters}/>
                <Button onClick={()=>navigate("/invitations")}>Пригласить нового сотрудника</Button>
            </div>

            <TableRow cells={headers} isHeader/>
            {
                workers.length === 0 ?
                    <TableRow cells={[{type: "text", text: "Нет сотрудников!", alignment: "center", width: "1272px"}]}/> :
                    <></>
            }
            {
                workers.map(worker => {
                    let cells = [
                        {type: "text", text: worker.name, alignment: "left", width: "400px"},
                        {type: "text", text: block[worker.department], alignment: "left", width: "400px"}, //TODO
                        {type: "text", text: roles[worker.role], alignment: "left", width: "400px"}
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
