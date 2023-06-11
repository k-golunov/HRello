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
import {getAllTasks, removeTasks, resetTasks} from "../store/slices/tasksSlice";
import {useDepartments} from "../hooks/use-departments";
import {getDepartments} from "../store/slices/departmentsSlice";
import Loading from "../components/Loading/Loading";
import {getBlocks} from "../store/slices/blocksSlice";
import {useBlocks} from "../hooks/use-blocks";
import {getUsers} from "../store/slices/usersSlice";
import {useUsers} from "../hooks/use-users";
import {removeTask} from "../store/slices/taskSlice";
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import TitleDropdown from "../components/TitleDropdown/TitleDropdown";

const AllTasksPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const tasks = useTasks();
    const departments = useDepartments();
    const blocks = useBlocks();
    const users = useUsers();

    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        dispatch(resetTasks());
        dispatch(getAllTasks({page: currentPage}));
        dispatch(getDepartments());
        dispatch(getBlocks());
        dispatch(getUsers());
        dispatch(removeTask());
    }, []);

    const yearList = [
        { value: '2023', label: '2023'},
        { value: '2022', label: '2022'},
    ]

    const quarterlist = [
        { value: '1', label: '1 квартал'},
        { value: '2', label: '2 квартал'},
        { value: '3', label: '3 квартал'},
        { value: '4', label: '4 квартал'},
    ]

    let employeeFilter = []

    let blockFilter = []
    let departmentFilter = []

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

    if(!departments.isLoading)
        departmentFilter = departments.departments.map(department =>{
            return { value: department.id, label: department.name}
        })

    if(!blocks.isLoading)
        blockFilter = blocks.blocks.map(block =>{
            return { value: block.id, label: block.value}
        })

    if(!users.isLoading)
        employeeFilter = users.users.filter(user => user.emailConfirmed).map(user =>{
            return { value: user.id, label: user.surname + " " + user.name + " " + user.patronymic}
        })

    const [selectedYear, setSelectedYear] = useState(yearList[0]);
    const [selectedQuarter, setSelectedQuarter] = useState([]);
    const [selectedEmployee, setSelectedEmployee] = useState([]);
    const [selectedBlock, setSelectedBlock] = useState([]);
    const [selectedDepartment, setSelectedDepartment] = useState([]);
    const [selectedStatus, setSelectedStatus] = useState(statusList[0]);

    useEffect(() => {
        dispatch(getAllTasks({
            year: [selectedYear.value],
            page: currentPage,
            users:selectedEmployee.filter(worker=> worker.value).map(worker => worker.value?worker.value:""),
            blocks:selectedBlock.filter(block=> block.value).map(block => block.value?block.value:""),
            departments:selectedDepartment.filter(department=> department.value).map(department => department.value?department.value:""),
            quarter:selectedQuarter.filter(quarter=> quarter.value).map(quarter => quarter.value?quarter.value:""),
            status: [selectedStatus.value]
        }));
    }, [selectedYear, selectedEmployee, selectedBlock, selectedDepartment, selectedQuarter, selectedStatus, currentPage]);

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
            'options': departmentFilter,
            'state': selectedDepartment,
            'setState': setSelectedDepartment,
            'placeholder': "Отдел",
            'isMulti': true,
            'minWidth': '170px'
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

    if(tasks.isLoading || departments.isLoading || blocks.isLoading || users.isLoading)
        return <Loading/>

    return (
        <>
            <div className={s.titleContainer}>
                <h1>Все задачи за</h1>
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
                    let taskUser = users.users.find(findUser => findUser.id === task.userId)
                    let taskDepartment = departments.departments.find(findDepartment => findDepartment.id === task.departmentId)
                    let cells = [
                        {type: "text", text: task.quarter, alignment: "left", width: "100px"},
                        {type: "text", text: taskDepartment.name, alignment: "left", width: "150px"},
                        {type: "text", text: taskUser.surname + " " + taskUser.name + " " + taskUser.patronymic, alignment: "left", width: "200px"},
                        {type: "text", text: task.block, alignment: "left", width: "260px"},
                        {type: "text", text: task.name, alignment: "left", width: "300px"},
                        {type: "percent", percent: task.plannedWeight, alignment: "left", width: "80px"},
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


            {/*<Table1 />*/}
            {/*<Table columns={columns} data={tasks} />*/}
        </>
    );
};

export default AllTasksPage;
