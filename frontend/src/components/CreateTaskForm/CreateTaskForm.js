import React, {useState} from 'react';
import s from './CreateTaskForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser, signUpUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link} from "react-router-dom";
import PageTitle from "../PageTitle/PageTitle";
import Dropdown from "../Dropdown/Dropdown";
import {createTask} from "../../store/slices/tasksSlice";
import Breadcrumbs from "../Breadcrumbs/Breadcrumbs";

function CreateTaskForm(props) {
    const dispatch = useDispatch();
    const {register, handleSubmit, formState: {errors}} = useForm({
        defaultValues: {
            createTaskName: '',
            createTaskYear: '',
            createTaskPlaningWeight: '',
            createTaskPlaningResult: '',
        },
        mode: "onBlur"
    });

    const blocks = [
        {value: 'Selection', label: 'Подбор'},
        {value: 'Adaptation', label: 'Адаптация'},
        {value: 'StaffDevelopment', label: 'Развитие персонала'},
        {value: 'HRSupport', label: 'HR-сопровождение'},
        {value: 'CorporateCulture', label: 'Корпоративная культура'},
        {value: 'PersonnelAccountingAndSalary', label: 'Кадровый учет и з/п'},
        {value: 'HRBrandExternal', label: 'HR-бренд внешний'},
        {value: 'InternalWork', label: 'Внутренняя работа'},
        {value: 'Estimation', label: 'Оценка'},
    ]

    const category = [
        {value: 'Planned', label: 'Запланированная'},
        {value: 'NotPlanned', label: 'Незапланированная'},
    ]

    const quarter = [
        {value: '1', label: '1 квартал'},
        {value: '2', label: '2 квартал'},
        {value: '3', label: '3 квартал'},
        {value: '4', label: '4 квартал'}
    ]

    const [selectedBlock, setSelectedBlock] = useState(blocks[0]);
    const [selectedQuarter, setSelectedQuarter] = useState(quarter[0]);
    const [selectedCategory, setSelectedCategory] = useState(category[0]);


    const onSubmit = (payload) => {
        // if (payload.registrationPassword !== payload.registrationRetryPassword) {
        //     alert('Вы указали разные пароли!');
        //     return;
        // }
        //
        // delete payload.registrationRetryPassword;
        // payload.registrationPassword = md5(payload.registrationPassword);
        //
        const data = {
            name: payload.createTaskName,
            year: parseInt(payload.createTaskYear),
            quarter: parseInt(selectedQuarter.value),
            category: selectedCategory.value,
            block: selectedBlock.value,
            plannedWeight: parseInt(payload.createTaskPlaningWeight),
            waitResult: payload.createTaskPlaningResult,
        }
        console.log(data);
        dispatch(createTask(data));
    }

    return (
        <div className={s.createTaskForm}>
            <Breadcrumbs breadcrumbs={[{id: 1, title: "Мои задачи", src: "/tasks/my"}]}/>
            <PageTitle title="Новая задача"/>
            <div>
                <Form className={s.form} onSubmit={handleSubmit(onSubmit)}>
                    <div className={s.formRow}>
                        <Input register={register}
                               registerName='createTaskName'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Название"
                               require={true}
                               type="text"/>
                        <Input register={register}
                               registerName='createTaskYear'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Год"
                               require={true}
                               type="number"/>
                    </div>
                    <div className={s.formRow}>
                        <Dropdown title="Блок задачи" options={blocks} minWidth="353px" onChange={setSelectedBlock}/>
                        <Dropdown title="Квартал" options={quarter} minWidth="353px" onChange={setSelectedQuarter}/>
                    </div>

                    <div className={s.formRow}>
                        <Dropdown title="Категория задачи" options={category} minWidth="353px" onChange={setSelectedCategory}/>
                        <Input register={register}
                               registerName='createTaskPlaningWeight'
                               options={
                                   {
                                       required: true
                                   }
                               }
                               errors={errors}
                               title="Планируемый вес задачи, %"
                               require={true}
                               type="number"/>
                    </div>

                    <Input register={register}
                           registerName='createTaskPlaningResult'
                           errors={errors}
                           title="Планируемый результат"
                           options={
                               {
                                   required: true
                               }
                           }
                           require={true}
                           type="text"
                           rows={2}
                           as="textarea"
                    />
                    <div className={s.buttons}>
                        <Button type="submit">Отправить на проверку</Button>
                    </div>

                </Form>
            </div>
        </div>
    )
}

export default CreateTaskForm;
