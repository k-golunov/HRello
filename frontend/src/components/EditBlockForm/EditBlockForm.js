import React, {useState} from 'react';
import s from './EditBlockForm.module.css';
import Input from "../Input/Input";
import Button from "../Button/Button";
import Form from "react-bootstrap/Form";
import {useForm} from "react-hook-form";
import {signInUser} from '../../store/slices/userSlice';
import {useDispatch} from "react-redux";
import md5 from 'md5';
import {toast} from "react-toastify";
import {Link, useNavigate} from "react-router-dom";
import {changeTaskStatus, getTask, getTaskHistory} from "../../store/slices/taskSlice";
import {createBlock, editBlock, getBlocks} from "../../store/slices/blocksSlice";

function EditBlockForm(props) {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);

    const onSubmit = (payload) => {
        if(!isLoading)
        {
            setIsLoading(true);
            dispatch(editBlock({
                id: props.block.id,
                value: payload.editBlockName
            })).then(response=>{
                dispatch(getBlocks())
                props.setActive(false)
                setIsLoading(false)
            })
        }
    }

    return (
        <div className={s.authorizationForm}>
            <div>
                <p className={s.authorization}>Редактирование блока</p>
                <Form className={s.form} onSubmit={props.handleSubmit(onSubmit)}>
                    <Input register={props.register}
                           registerName='editBlockName'
                           errors={props.errors}
                           title="Название"
                           options={
                               {
                                   required: {
                                       value: true,
                                       message: "Поле обязательно для ввода"
                                   },
                               }
                           }
                           //require={true}
                           type="text"
                           //description="Расскажите о возникших сложностях"
                    />
                    <div className={s.buttons}>
                        <Button type="submit">Сохранить</Button>
                        <Button isSecond click={()=>{
                            props.setActive(false)
                            props.reset()
                        }}>Отменить</Button>
                    </div>

                </Form>
            </div>
        </div>
    )
}

export default EditBlockForm;
