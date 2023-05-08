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

const HomePage = () => {
    const dispatch = useDispatch();
    const isAuth = useAuth().isAuth;
    const navigate = useNavigate();

    const tasks = useTasks();

    useEffect(() => {
        dispatch(getAllTasks({page: 1}));
    }, []);

    const yearList = [
        { value: '2022', label: '2022'},
        { value: '2023', label: '2023'}
    ]

    const quarterlist = [
        { value: '1', label: '1 квартал'},
        { value: '2', label: '2 квартал'},
        { value: '3', label: '3 квартал'},
        { value: '4', label: '4 квартал'},
    ]

    const employeeFilter = [
        { value: '123', label: 'Кочнева Анжелика Дмитриевна'},
        { value: '234', label: 'Батрутдинова Элина'},
        { value: '345', label: 'Бакирова Мария'},
        { value: '456', label: 'Астафьева Анна Викторовна'},
    ]

    const blockFilter = [
        { value: 'Selection', label: 'Подбор'},
        { value: 'Adaptation', label: 'Адаптация'},
        { value: 'StaffDevelopment', label: 'Развитие персонала'},
        { value: 'HRSupport', label: 'HR-сопровождение'},
        { value: 'CorporateCulture', label: 'Корпоративная культура'},
        { value: 'PersonnelAccountingAndSalary', label: 'Кадровый учет и з/п'},
        { value: 'HRBrandExternal', label: 'HR-бренд внешний'},
        { value: 'InternalWork', label: 'Внутренняя работа'},
        { value: 'Estimation', label: 'Оценка'},
    ]

    const sortWeightList = [
        { value: 'desc', label: 'По возрастанию'},
        { value: 'asc', label: 'По убыванию'},
    ]

    const departmentFilter = [
        { value: 'Selection', label: 'Подбор'},
        { value: 'Adaptation', label: 'Адаптация'},
        { value: 'StaffDevelopment', label: 'Развитие персонала'},
        { value: 'HRSupport', label: 'HR-сопровождение'},
        { value: 'CorporateCulture', label: 'Корпоративная культура'},
        { value: 'PersonnelAccountingAndSalary', label: 'Кадровый учет и з/п'},
        { value: 'HRBrandExternal', label: 'HR-бренд внешний'},
        { value: 'InternalWork', label: 'Внутренняя работа'},
        { value: 'Estimation', label: 'Оценка'},
    ]

    const statusList = [
        { value: 'OnChecking', label: 'На проверке'},
        { value: 'OnRework', label: 'На доработку'},
        { value: 'InWork', label: 'В работе'},
        { value: 'AwaitingCancellation', label: 'Ожидает отмены'},
        { value: 'Canceled', label: 'Отменена'},
        { value: 'CompletionCheck', label: 'Проверка завершения'},
        { value: 'Completed', label: 'Завершена'},
    ]

    // const departmentList = [
    //     { value: '123', label: 'Интеграторы производства'},
    //     { value: '123', label: 'Разработка'},
    //     { value: '123', label: 'Компенсации и льготы'},
    // ]

    const [selectedYear, setSelectedYear] = useState([]);
    const [selectedQuarter, setSelectedQuarter] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState([]);
    const [selectedBlock, setSelectedBlock] = useState([]);
    const [selectedDepartment, setSelectedDepartment] = useState([]);
    const [selectedSort, setSelectedSort] = useState([]);

    const filters = [
        {
            'options': yearList,
            'state': selectedYear,
            'setState': setSelectedYear,
            'placeholder': "Год",
            'isMulti': false
        },
        {
            'options': quarterlist,
            'state': selectedQuarter,
            'setState': setSelectedQuarter,
            'placeholder': "Квартал",
            'isMulti': true,
        },
        {
            'options': employeeFilter,
            'state': selectedEmployee,
            'setState': setSelectedEmployee,
            'placeholder': "Сотрудник",
            'isMulti': true,
            'minWidth': '150px'
        },
        {
            'options': blockFilter,
            'state': selectedBlock,
            'setState': setSelectedBlock,
            'placeholder': "Блок",
            'isMulti': true,
            'minWidth': '170px'
        },
        {
            'options': departmentFilter,
            'state': selectedDepartment,
            'setState': setSelectedDepartment,
            'placeholder': "Отдел",
            'isMulti': true,
            'minWidth': '170px'
        },
        {
            'options': sortWeightList,
            'state': selectedSort,
            'setState': setSelectedSort,
            'placeholder': "Сортировка веса по",
            'isMulti': false,
        }
    ]

    const headers = [
        {type: "header", text: 'Квартал', alignment: "left", width: "100px"},
        {type: "header", text: 'Отдел', alignment: "left", width: "150px"},
        {type: "header", text: 'ФИО сотрудника', alignment: "left", width: "200px"},
        {type: "header", text: 'Блок', alignment: "left", width: "260px"},
        {type: "header", text: 'Название', alignment: "left", width: "300px"},
        {type: "header", text: 'Вес', alignment: "left", width: "80px"},
        {type: "header", text: 'Статус', alignment: "left", width: "260px"},
    ]

    const cells = [
        {type: "text", text: '2', alignment: "left", width: "100px"},
        {type: "text", text: 'Интеграторы производства', alignment: "left", width: "150px"},
        {type: "text", text: 'Кочнева Анжелика Дмитриевна ', alignment: "left", width: "200px"},
        {type: "block", block: "CorporateCulture", alignment: "left", width: "260px"},
        {type: "text", text: "Подбор согласно плану вакансий", alignment: "left", width: "300px"},
        {type: "percent", percent: 70, alignment: "left", width: "80px"},
        {type: "status", status: 'CompletionCheck', alignment: "left", width: "260px"},
    ]


    return (
        <>
            <PageTitle title="Все задачи"/>
            <div className={s.myTaskPageFilters}>
                <Filters filters={filters}/>
                <Button onClick={()=>navigate("/tasks/create")}>Новая задача</Button>
            </div>

            <TableRow cells={headers} isHeader/>
            {
                tasks.tasks.length === 0 ?
                    <TableRow cells={[{type: "text", text: "Нет задач!", alignment: "center", width: "1272px"}]}/> :
                    <></>
            }
            {
                tasks.tasks.map(task => {
                    let cells = [
                        {type: "text", text: task.quarter, alignment: "left", width: "100px"},
                        {type: "text", text: 'Интеграторы производства', alignment: "left", width: "150px"},
                        {type: "text", text: 'Кочнева Анжелика Дмитриевна', alignment: "left", width: "200px"},
                        {type: "block", block: task.block, alignment: "left", width: "260px"},
                        {type: "text", text: task.name, alignment: "left", width: "300px"},
                        {type: "percent", percent: task.plannedWeight, alignment: "left", width: "80px"},
                        {type: "status", status: task.status, alignment: "left", width: "260px"},
                    ]
                    return <TableRow cells={cells} taskID={task.id}/>
                })
            }
            {/*<Table1 />*/}
            {/*<Table columns={columns} data={tasks} />*/}
        </>
    );
};

export default HomePage;
