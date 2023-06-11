import React, {useEffect, useState} from 'react';
import 'react-toastify/dist/ReactToastify.css';
import s from "./Pages.module.css";
import TitleDropdown from "../components/TitleDropdown/TitleDropdown";
import Filters from "../components/Filters/Filters";
import Button from "../components/Button/Button";
import {resetTasks} from "../store/slices/tasksSlice";
import {getDepartments} from "../store/slices/departmentsSlice";
import {removeTask} from "../store/slices/taskSlice";
import {useDispatch} from "react-redux";
import {useNavigate} from "react-router-dom";
import {useDepartments} from "../hooks/use-departments";
import {useAuth} from "../hooks/use-auth";

const ResultsPage = () => {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const user = useAuth();

    const departments = useDepartments();

    const yearList = [
        { value: '2023', label: '2023'},
        { value: '2022', label: '2022'},
    ]

    const quarterList = [
        { value: '1', label: '1 квартал'},
        { value: '2', label: '2 квартал'},
        { value: '3', label: '3 квартал'},
        { value: '4', label: '4 квартал'},
    ]

    useEffect(() => {
        dispatch(resetTasks());
        dispatch(getDepartments());
        dispatch(removeTask());
    }, []);


    let departmentFilter = []

    const [selectedYear, setSelectedYear] = useState(yearList[0]);
    const [selectedQuarter, setSelectedQuarter] = useState([]);
    const [selectedDepartment, setSelectedDepartment] = useState([]);

    if(!departments.isLoading)
        departmentFilter = departments.departments.map(department =>{
            return { value: department.id, label: department.name}
        })

    const filters = [
        {
            'options': quarterList,
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
        }
    ]

    return (
        <div>
            <div className={s.titleContainer}>
                <h1>Итоги за</h1>
                <TitleDropdown options={yearList}
                               onChange={setSelectedYear}
                               minWidth={'100px'}
                />
                <h1>год</h1>
            </div>
            <div className={s.resultsPageFilters}>
                <Filters filters={filters}/>
                {
                    user.role !== "employee" ?
                        <Button onClick={()=>navigate("/results/create")}>Новый итог</Button>
                        : <></>
                }


            </div>
        </div>
    );
};

export default ResultsPage;
