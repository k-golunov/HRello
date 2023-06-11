import React, {useEffect, useState} from 'react';
import s from './CreateResultForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser, signUpUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import PageTitle from "../PageTitle/PageTitle";
import Dropdown from "../Dropdown/Dropdown";
import {getAllTasks} from "../../store/slices/tasksSlice";
import Breadcrumbs from "../Breadcrumbs/Breadcrumbs";
import {useAuth} from "../../hooks/use-auth";
import {useDepartments} from "../../hooks/use-departments";
import {useBlocks} from "../../hooks/use-blocks";
import Loading from "../Loading/Loading";
import {getBlocks} from "../../store/slices/blocksSlice";
import {createTask} from "../../store/slices/taskSlice";
import ColorDropdown from "../ColorDropdown/ColorDropdown";
import Badge from "../Badge/Badge";


function CreateResultForm(props) {
    const dispatch = useDispatch();




    return (
        <div className={s.createTaskForm}>
            <div>
                <Form className={s.form} onSubmit={props.handleSubmit(props.onSubmit)}>
                    <div className={s.formRow}>
                        <div className={s.input}>
                            <Input register={props.register}
                                    registerName='createResultResult'
                                    errors={props.errors}
                                    title="Итог"
                                    options={{
                                        required: true
                                    }}
                                    require={true}
                                    type="text"
                                    rows={2}
                                    as="textarea"
                            />
                        </div>


                        <ColorDropdown title="Статус" options={props.colors} minWidth="120px" onChange={props.setSelectedColor}/>

                        <div className={s.badges}>
                            {
                                props.selectedTasks.map(task => {
                                    // let t = props.tasks.filter(fTask => fTask.id === taskID)[0]
                                    return <Badge title={task.name}
                                                  onClick={() => props.setSelectedTasksID(props.selectedTasksID.filter(sTask => sTask !== task.id))}
                                    />
                                })
                            }
                        </div>

                        {/*<Dropdown title="Статус" options={quarter} minWidth="353px" onChange={setSelectedQuarter}/>*/}
                    </div>

                    <div className={s.buttons}>
                        <Button type="submit">Создать итог</Button>
                    </div>

                </Form>
            </div>
        </div>)
}

export default CreateResultForm;
