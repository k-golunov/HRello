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
import {getAllTasks, resetTasks} from "../store/slices/tasksSlice";
import {useTasks} from "../hooks/use-tasks";
import {useDepartments} from "../hooks/use-departments";
import {useBlocks} from "../hooks/use-blocks";
import {getDepartments} from "../store/slices/departmentsSlice";
import {getBlocks} from "../store/slices/blocksSlice";
import Loading from "../components/Loading/Loading";
import {removeTask} from "../store/slices/taskSlice";
import Pagination from "rc-pagination";

const MyTasksPage = () => {
    const dispatch = useDispatch();
    const user = useAuth();
    const navigate = useNavigate();
    const tasks = useTasks();
    const blocks = useBlocks();

    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        dispatch(resetTasks());
        dispatch(getAllTasks({page: currentPage, users: [user.id]}));
        dispatch(getBlocks());
        dispatch(removeTask());
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

    let blockFilter = []

    const sortWeightList = [
        { value: 'desc', label: 'По возрастанию'},
        { value: 'asc', label: 'По убыванию'},
    ]

    const statusList = [
        { value: ["OnChecking", "OnRework", "InWork", "AwaitingCancellation", "Canceled", "CompletionCheck", "Completed"], label: '-'},
        { value: "OnChecking", label: 'На проверке'},
        { value: "OnRework", label: 'На доработку'},
        { value: "InWork", label: 'В работе'},
        { value: "AwaitingCancellation", label: 'Ожидает отмены'},
        { value: "Canceled", label: 'Отменена'},
        { value: "CompletionCheck", label: 'Проверка завершения'},
        { value: "Completed", label: 'Завершена'},
    ]

    const [selectedYear, setSelectedYear] = useState([]);
    const [selectedQuarter, setSelectedQuarter] = useState([]);
    const [selectedBlock, setSelectedBlock] = useState([]);
    const [selectedSort, setSelectedSort] = useState([]);
    const [selectedStatus, setSelectedStatus] = useState(statusList[0]);

    if(!blocks.isLoading)
        blockFilter = blocks.blocks.map(block =>{
            return { value: block.id, label: block.value}
        })

    useEffect(() => {
        dispatch(getAllTasks({
            page: currentPage,
            users:[user.id],
            blocks:selectedBlock.filter(block=> block.value).map(block => block.value?block.value:""),
            quarter:selectedQuarter.filter(quarter=> quarter.value).map(quarter => quarter.value?quarter.value:""),
            status: [selectedStatus.value]
        }));
    }, [selectedBlock, selectedQuarter, selectedStatus, currentPage]);

    const filters = [
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
            'options': statusList,
            'state': selectedStatus,
            'setState': setSelectedStatus,
            'placeholder': "Статус задачи",
            'title': "Статус задачи",
            'isMulti': false,
            'minWidth': '232px'
        }
    ]

    const headers = [
        {type: "header", text: 'Год', alignment: "left", width: "100px"},
        {type: "header", text: 'Квартал', alignment: "left", width: "100px"},
        {type: "header", text: 'Блок', alignment: "left", width: "260px"},
        {type: "header", text: 'Название', alignment: "left", width: "500px"},
        {type: "header", text: 'Вес', alignment: "left", width: "100px"},
        {type: "header", text: 'Статус', alignment: "left", width: "260px"},
    ]

    if(tasks.isLoading || blocks.isLoading)
        return <Loading/>

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
                            {type: "text", text: task.block, alignment: "left", width: "260px"},
                            {type: "text", text: task.name, alignment: "left", width: "500px"},
                            {type: "percent", percent: task.plannedWeight, alignment: "left", width: "100px"},
                            {type: "status", status: task.status, alignment: "left", width: "260px"},
                        ]
                    return <TableRow cells={cells} taskID={task.id}/>
                })
            }

            <div className={s.pagination}>
                <Pagination total={tasks.pagesCount*10}
                            current={ currentPage }
                            onChange={page => setCurrentPage(page)}
                            pageSize={10}
                            hideOnSinglePage
                />
            </div>
        </>
    );
};

export default MyTasksPage;
