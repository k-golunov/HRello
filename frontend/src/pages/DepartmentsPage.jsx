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
import {getUsers} from "../store/slices/usersSlice";
import {useUsers} from "../hooks/use-users";
import {useForm} from "react-hook-form";
import AddBlockForm from "../components/AddBlockForm/AddBlockForm";
import {ModalWindow} from "../components/ModalWindow/ModalWindow";
import AddDepartmentForm from "../components/AddDepartmentForm/AddDepartmentForm";
import EditBlockForm from "../components/EditBlockForm/EditBlockForm";
import EditDepartmentForm from "../components/EditDepartmentForm/EditDepartmentForm";
import Loading from "../components/Loading/Loading";

const DepartmentsPage = () => {
    const dispatch = useDispatch();
    const isAuth = useAuth().isAuth;
    const navigate = useNavigate();

    const departments = useDepartments();
    const users = useUsers();

    useEffect(() => {
        dispatch(getDepartments());
        dispatch(getUsers());
        dispatch(removeTask());
    }, []);

    let employeeFilter = []

    if(!users.isLoading)
        employeeFilter = users.users.filter(user => user.emailConfirmed).map(user =>{
            return { value: user.id, label: user.surname + " " + user.name + " " + user.patronymic}
        })

    // useEffect(() => {
    //     setSelectedEmployee(employeeFilter.find(employee => employee.value === props.department?.bossId))
    // }, [users]);

    const [addDepartmentModalActive, setAddDepartmentModalActive] = useState(false);
    const [editDepartmentModalActive, setEditDepartmentModalActive] = useState(false);
    const [editDepartment, setEditDepartment] = useState(null);
    const [selectedEmployee, setSelectedEmployee] = useState(null);

    const {register: registerAddDepartment, reset: resetAddDepartment, handleSubmit: handleSubmitAddDepartment, formState: {errors: errorsAddDepartment}} = useForm({
        defaultValues: {
            addDepartmentName: ''
        },
        mode: "onBlur"
    });

    const {register: registerEditDepartment, reset: resetEditDepartment, handleSubmit: handleSubmitEditDepartment, formState: {errors: errorsEditDepartment}} = useForm({
        defaultValues: {
            editDepartmentName: ''
        },
        mode: "onBlur"
    });


    const rolesList = [
        { value: 'worker', label: "Сотрудник"},
        { value: 'boss', label: "Руководитель"},
        { value: 'mainboss', label: "Главный руководитель"},
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
        {type: "header", text: 'Название', alignment: "left", width: "600px"},
        {type: "header", text: 'Руководитель', alignment: "left", width: "600px"},
    ]

    /*departments = [
        {boss: "Бурханова Екатерина Сергеевна", name: "Компенсации и льготы"},
        {boss: "Астафьева Анна Викторовна", name: "Интеграторы производства"},
        {boss: "Астафьева Анна Викторовна", name: "Разработка"},
    ]*/

    if(departments.isLoading || users.isLoading)
        return <Loading/>


    return (
        <>
            <PageTitle title="Отделы"/>


            <TableRow cells={headers} isHeader/>
            {
                departments.length === 0 ?
                    <TableRow cells={[{type: "text", text: "Нет отделов!", alignment: "center", width: "1272px"}]}/> :
                    <></>
            }
            {
                departments.departments.map(department => {
                    let user = department.bossId ? users.users.filter(user => user.id === department.bossId)[0]:null
                    console.log(user)
                    let cells = [
                        {type: "text", text: department.name, alignment: "left", width: "600px"},
                        {type: "text", text: user? user.surname+" "+user.name+" "+user.patronymic: "-", alignment: "left", width: "600px"},
                    ]
                    return <TableRow cells={cells}
                                     department={department}
                                     setEditDepartment={setEditDepartment}
                                     resetEditDepartment={resetEditDepartment}
                                     setActive={setEditDepartmentModalActive}
                                     setSelectedEmployee={setSelectedEmployee}
                                     employeeFilter={employeeFilter}
                    />
                })
            }
            <div className={s.serviceButtons}>
                <Button onClick={()=>setAddDepartmentModalActive(true)}>Новый отдел</Button>
            </div>

            <ModalWindow active={addDepartmentModalActive}
                         setActive={setAddDepartmentModalActive}
                         onClose={()=>resetAddDepartment({
                             addBlockName: ''
                         })}>
                <AddDepartmentForm handleSubmit={handleSubmitAddDepartment}
                                   errors={errorsAddDepartment}
                                   register={registerAddDepartment}
                                   setActive={setAddDepartmentModalActive}
                                   reset={resetAddDepartment}
                />
            </ModalWindow>

            <ModalWindow active={editDepartmentModalActive}
                         setActive={setEditDepartmentModalActive}
                         onClose={()=>{
                             resetEditDepartment({ editDepartmentName: ''});
                             setEditDepartment(null);
                             setSelectedEmployee(null);
                         }}>
                <EditDepartmentForm handleSubmit={handleSubmitEditDepartment}
                                    errors={errorsEditDepartment}
                                    register={registerEditDepartment}
                                    setActive={setEditDepartmentModalActive}
                                    setEditDepartment={setEditDepartment}
                                    editDepartment={editDepartment}
                                    reset={resetEditDepartment}
                                    department={editDepartment}
                                    selectedEmployee={selectedEmployee}
                                    setSelectedEmployee={setSelectedEmployee}
                                    employeeFilter={employeeFilter}
                />
            </ModalWindow>
        </>
    );
};

export default DepartmentsPage;
