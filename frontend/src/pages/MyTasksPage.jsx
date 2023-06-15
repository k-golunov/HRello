import React, {useEffect, useState} from 'react';
import {useDispatch} from 'react-redux';
import Button from '../components/Button/Button';
import {useNavigate} from 'react-router-dom';
import {useAuth} from '../hooks/use-auth';
import Filters from "../components/Filters/Filters";
import s from './Pages.module.css';
import TableRow from "../components/TableRow/TableRow";
import {getAllTasks, resetTasks} from "../store/slices/tasksSlice";
import {useTasks} from "../hooks/use-tasks";
import {useBlocks} from "../hooks/use-blocks";
import {getBlocks} from "../store/slices/blocksSlice";
import Loading from "../components/Loading/Loading";
import {removeTask} from "../store/slices/taskSlice";
import Pagination from "rc-pagination";
import TitleDropdown from "../components/TitleDropdown/TitleDropdown";

const MyTasksPage = () => {
    const dispatch = useDispatch();
    const user = useAuth();
    const navigate = useNavigate();
    const tasks = useTasks();
    const blocks = useBlocks();

    const [currentPage, setCurrentPage] = useState(1);



    const yearList = [
        { value: '2023', label: '2023'},
        { value: '2022', label: '2022'}
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
        { value: ["OnChecking", "OnRework", "InWork", "AwaitingCancellation", "Canceled", "CompletionCheck", "Completed"], label: 'Любой статус'},
        { value: "OnChecking", label: 'На проверке'},
        { value: "OnRework", label: 'На доработку'},
        { value: "InWork", label: 'В работе'},
        { value: "AwaitingCancellation", label: 'Ожидает отмены'},
        { value: "Canceled", label: 'Отменена'},
        { value: "CompletionCheck", label: 'Проверка завершения'},
        { value: "Completed", label: 'Завершена'},
    ]

    useEffect(() => {
        dispatch(resetTasks());
        dispatch(getAllTasks({
            year: [yearList[0].value],
            page: currentPage,
            users:[user.id],
            status: [statusList[0].value]
        }));
        dispatch(getBlocks());
        dispatch(removeTask());
    }, []);

    const [selectedYear, setSelectedYear] = useState(yearList[0]);
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
            year: [selectedYear.value],
            page: currentPage,
            users:[user.id],
            blocks:selectedBlock.filter(block=> block.value).map(block => block.value?block.value:""),
            quarter:selectedQuarter.filter(quarter=> quarter.value).map(quarter => quarter.value?quarter.value:""),
            status: [selectedStatus.value]
        }));
    }, [selectedBlock, selectedQuarter, selectedStatus, currentPage, selectedYear]);

    const filters = [
        {
            'options': quarterlist,
            'state': selectedQuarter,
            'setState': setSelectedQuarter,
            'placeholder': "Квартал",
            'isMulti': true,
            'minWidth': '120px'
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
            // 'title': "Статус задачи",
            'isMulti': false,
            'minWidth': '232px'
        }
    ]

    const headers = [
        {type: "header", text: 'Год', alignment: "left", width: "100px"},
        {type: "header", text: 'Квартал', alignment: "left", width: "100px"},
        {type: "header", text: 'Блок', alignment: "left", width: "212px"},
        {type: "header", text: 'Название', alignment: "left", width: "500px"},
        {type: "header", text: 'Вес', alignment: "left", width: "100px"},
        {type: "header", text: 'Статус', alignment: "left", width: "260px"},
    ]

    if(tasks.isLoading || blocks.isLoading)
        return <Loading/>

    return (
        <>

            <div className={s.titleContainer}>
                <h1>Мои задачи за</h1>
                {/*<PageTitle title="Мои задачи"/>*/}
                <TitleDropdown options={yearList}
                               onChange={setSelectedYear}
                               minWidth={'100px'}
                />
                <h1>год</h1>
            </div>

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
                            {type: "text", text: task.block, alignment: "left", width: "212px"},
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
