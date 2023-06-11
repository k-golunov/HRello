import React, {useEffect, useState} from 'react';
import 'react-toastify/dist/ReactToastify.css';
import TitleDropdown from "../components/TitleDropdown/TitleDropdown";
import s from "./Pages.module.css";
import {useBlocks} from "../hooks/use-blocks";
import {getAllTasks, resetTasks} from "../store/slices/tasksSlice";
import {getDepartments} from "../store/slices/departmentsSlice";
import {getBlocks} from "../store/slices/blocksSlice";
import {getUsers} from "../store/slices/usersSlice";
import {removeTask} from "../store/slices/taskSlice";
import {useDispatch} from "react-redux";
import Loading from "../components/Loading/Loading";
import Pagination from "rc-pagination";
import {useTasks} from "../hooks/use-tasks";
import Filters from "../components/Filters/Filters";
import TableRow from "../components/TableRow/TableRow";
import {useUsers} from "../hooks/use-users";
import CreateResultForm from "../components/CreateResultForm/CreateResultForm";
import {useForm} from "react-hook-form";
import {toast} from "react-toastify";
import {createResult} from "../store/slices/resultSlice";
import {useNavigate} from "react-router-dom";

const CreateResultPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();

    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            createResultResult: 'План: \n\nФакт:\n'
        }, mode: "onBlur"
    });

    useEffect(() => {
        dispatch(resetTasks());
        dispatch(getAllTasks({page: currentPage, status: ["Completed"]}));
        dispatch(getDepartments());
        dispatch(getBlocks());
        dispatch(getUsers());
        dispatch(removeTask());
    }, []);

    const blocks = useBlocks();
    const tasks = useTasks();
    const users = useUsers();

    const [currentPage, setCurrentPage] = useState(1);

    const yearList = [
        { value: 2023, label: '2023'},
        { value: 2022, label: '2022'},
    ]

    const quarterList = [
        { value: 1, label: '1 квартал'},
        { value: 2, label: '2 квартал'},
        { value: 3, label: '3 квартал'},
        { value: 4, label: '4 квартал'},
    ]

    const colors = [
        {value: 'Green', color: '#A9F26F'},
        {value: 'Yellow', color: '#FFF964'},
        {value: 'Red', color: '#F27F6F'},
    ]

    const [selectedColor, setSelectedColor] = useState(colors[0]);
    const [selectedBlock, setSelectedBlock] = useState(null);
    const [selectedQuarter, setSelectedQuarter] = useState(quarterList[0]);
    const [selectedYear, setSelectedYear] = useState(yearList[0]);
    const [selectedTasksID, setSelectedTasksID] = useState([]);
    const [selectedTasks, setSelectedTasks] = useState([]);

    let blockFilter = []

    useEffect(() => {
        setSelectedBlock(blockFilter[0])
    }, [blocks]);

    useEffect(() => {
        dispatch(getAllTasks({
            year: [selectedYear.value],
            page: currentPage,
            blocks:[selectedBlock?.value],
            status: ["Completed"],
            quarter:[selectedQuarter.value]
        }));
    }, [selectedBlock, selectedQuarter, currentPage, selectedYear]);

    useEffect(() => {
        setSelectedTasksID([])
        setSelectedTasks([])
    }, [selectedBlock, selectedQuarter, selectedYear]);

    if(!blocks.isLoading)
        blockFilter = blocks.blocks.map(block =>{
            return { value: block.id, label: block.value}
        })

    const filters = [
        {
            'options': yearList,
            'state': selectedYear,
            'setState': setSelectedYear,
            'placeholder': "Год",
            "title": "Год"
        },
        {
            'options': quarterList,
            'state': selectedQuarter,
            'setState': setSelectedQuarter,
            'placeholder': "Квартал",
            "title": "Квартал",
            'minWidth': '120px'
        }
    ]

    const headers = [
        {type: "header", text: '', alignment: "left", width: "50px"},
        {type: "header", text: 'ФИО сотрудника', alignment: "left", width: "215px"},
        {type: "header", text: 'Название задачи', alignment: "left", width: "340px"},
        {type: "header", text: 'Планируемый результат', alignment: "left", width: "340px"},
        {type: "header", text: 'Фактический результат', alignment: "left", width: "340px"},
    ]

    if(tasks.isLoading || blocks.isLoading || users.isLoading)
        return <Loading/>

    const onSubmit = (payload) => {
        if(selectedTasksID.length === 0)
            toast.error("Выберите хотя бы одну задачу!");
        // if (payload.registrationPassword !== payload.registrationRetryPassword) {
        //     alert('Вы указали разные пароли!');
        //     return;
        // }
        //
        // delete payload.registrationRetryPassword;
        // payload.registrationPassword = md5(payload.registrationPassword);
        //

        const color = {
            "#A9F26F": 1
        }
        const data = {
            result: payload.createResultResult,
            color: selectedColor.value,
            year: selectedYear.value,
            quarter: selectedQuarter.value,
            tasksId: selectedTasksID
        }
        console.log("DATA", data)


        // if (payload.createTaskPlaningWeight && selectedCategory.value !== "NotPlanned")
        //     data["plannedWeight"] = parseInt(payload.createTaskPlaningWeight)
        // else
        //     data["plannedWeight"] = -1
        // console.log(data);
        dispatch(createResult(data)).then(()=>navigate("/results"));
    }

    return (
        <div>
            <div className={s.resultsTitle}>
                <h1>Создание итога по блоку</h1>
                <TitleDropdown options={blockFilter}
                               onChange={setSelectedBlock}
                               minWidth={'100px'}
                />
            </div>

            <div className={s.createResultFilters}>
                <Filters filters={filters}/>
            </div>

            <div className={s.createResultTasks}>
                <TableRow cells={headers} isHeader/>
                {
                    tasks.tasks.length === 0 ?
                        <TableRow cells={[{type: "text", text: "Нет задач!", alignment: "center", width: "1272px"}]}/> :
                        <></>
                }
                {
                    tasks.tasks.map(task => {
                        let taskUser = users.users.find(findUser => findUser.id === task.userId)
                        let isSelect = selectedTasksID.filter(sTask => task.id === sTask).length !== 0
                        let cells = [
                            {type: "selectTask", isSelect: isSelect, alignment: "left", width: "50px"},
                            {type: "text", text: taskUser.surname + " " + taskUser.name + " " + taskUser.patronymic, alignment: "left", width: "215px"},
                            {type: "text", text: task.name, alignment: "left", width: "340px"},
                            {type: "text", text: task.waitResult, alignment: "left", width: "340px"},
                            // {type: "text", text: task.factResult, alignment: "left", width: "340px"},
                        ]
                        return <TableRow cells={cells}
                                         taskID={task.id}
                                         task={task}
                                         selectedTasksID={selectedTasksID}
                                         setSelectedID={setSelectedTasksID}
                                         selectedTasks={selectedTasks}
                                         setSelected={setSelectedTasks}
                                         isSelect={isSelect}
                        />
                    })
                }
            </div>



            <div className={s.pagination}>
                <Pagination total={tasks.pagesCount*10}
                            current={ currentPage }
                            onChange={page => setCurrentPage(page)}
                            pageSize={10}
                            hideOnSinglePage
                />
            </div>

            <div className={s.createResultForm}>
                <CreateResultForm register={register}
                                  handleSubmit={handleSubmit}
                                  errors={errors}
                                  selectedColor={selectedColor}
                                  setSelectedColor={setSelectedColor}
                                  colors={colors}
                                  tasks={tasks.tasks}
                                  selectedTasksID={selectedTasksID}
                                  setSelectedTasksID={setSelectedTasksID}
                                  selectedTasks={selectedTasks}
                                  setSelectedTasks={setSelectedTasks}
                                  onSubmit={onSubmit}
                />
            </div>
        </div>
    );
};

export default CreateResultPage;
