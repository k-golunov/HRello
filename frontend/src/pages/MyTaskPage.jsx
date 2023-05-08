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
import {getAllTasks} from "../store/slices/tasksSlice";
import {useTasks} from "../hooks/use-tasks";

const MyTaskPage = () => {
    const dispatch = useDispatch();
    const user = useAuth();
    const navigate = useNavigate();
    const tasks = useTasks();

    useEffect(() => {
        dispatch(getAllTasks({page: 1, userID: user.id}));
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

    // const employeeList = [
    //     { value: '123', label: 'Кочнева Анжелика Дмитриевна'},
    //     { value: '234', label: 'Батрутдинова Элина'},
    //     { value: '345', label: 'Бакирова Мария'},
    //     { value: '456', label: 'Астафьева Анна Викторовна'},
    // ]

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
    const [selectedBlock, setSelectedBlock] = useState([]);
    const [selectedSort, setSelectedSort] = useState([]);
    const [selectedStatus, setSelectedStatus] = useState([]);

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
            'options': blockFilter,
            'state': selectedBlock,
            'setState': setSelectedBlock,
            'placeholder': "Блок",
            'isMulti': true,
            'minWidth': '230px'
        },
        {
            'options': sortWeightList,
            'state': selectedSort,
            'setState': setSelectedSort,
            'placeholder': "Сортировка веса по",
            'isMulti': false,
            'minWidth': '186px'
        },
        {
            'options': statusList,
            'state': selectedStatus,
            'setState': setSelectedStatus,
            'placeholder': "Статус задачи",
            'isMulti': false,
            'minWidth': '232px'
        }
    ]

    const columns = [
        {
            title: "Год",
            dataIndex: 'year',
            key: 'year',
            width: 100,
        },
        {
            title: 'Квартал',
            dataIndex: 'quarter',
            key: 'quarter',
            width: 100,
        },
        {
            title: 'Название',
            dataIndex: 'name',
            key: 'name',
            width: 476,
        },
        {
            title: 'Weight',
            dataIndex: 'weight',
            key: 'weight',
            width: 185,
        },
        {
            title: 'Status',
            dataIndex: 'status',
            key: 'status',
            width: 232,
        }
        // },
        // {
        //     title: 'Name',
        //     dataIndex: '',
        //     key: 'operations',
        //     render: () => <a href="#">Delete</a>,
        // },
    ];

    // const tasks = [
    //     {
    //         year: 2023,
    //         quarter: 1,
    //         block: "Selection",
    //         name: "Подбор согласно плану вакансий",
    //         weight: 25,
    //         status: "OnChecking"
    //     }
    // ];

    const headers = [
        {type: "header", text: 'Год', alignment: "left", width: "100px"},
        {type: "header", text: 'Квартал', alignment: "left", width: "100px"},
        {type: "header", text: 'Блок', alignment: "left", width: "260px"},
        {type: "header", text: 'Название', alignment: "left", width: "500px"},
        {type: "header", text: 'Вес', alignment: "left", width: "100px"},
        {type: "header", text: 'Статус', alignment: "left", width: "260px"},
    ]

    // const cells = [
    //     {type: "text", text: '2023', alignment: "left", width: "100px"},
    //     {type: "text", text: '1', alignment: "left", width: "100px"},
    //     {type: "block", block: "CorporateCulture", alignment: "left", width: "260px"},
    //     {type: "text", text: "Подбор согласно плану вакансий", alignment: "left", width: "500px"},
    //     {type: "percent", percent: 25, alignment: "left", width: "100px"},
    //     {type: "status", status: 'CompletionCheck', alignment: "left", width: "260px"},
    // ]


    return (
        <>
            <PageTitle title="Мои задачи"/>
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
                            {type: "text", text: task.year, alignment: "left", width: "100px"},
                            {type: "text", text: task.quarter, alignment: "left", width: "100px"},
                            {type: "block", block: task.block, alignment: "left", width: "260px"},
                            {type: "text", text: task.name, alignment: "left", width: "500px"},
                            {type: "percent", percent: task.plannedWeight, alignment: "left", width: "100px"},
                            {type: "status", status: task.status, alignment: "left", width: "260px"},
                        ]
                    return <TableRow cells={cells} taskID={task.id}/>
                })
            }
            {/*<TableRow cells={cells} onClick={navigate("/task/ba62e168-6f36-43c7-ad57-5372a644a188")}/>*/}
            {/*<TableRow cells={cells}/>*/}
            {/*<Table1 />*/}
            {/*<Table columns={columns} data={tasks} />*/}
        </>
    );
};

export default MyTaskPage;
