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
import {getBlocks} from "../store/slices/blocksSlice";
import Loading from "../components/Loading/Loading";
import {useBlocks} from "../hooks/use-blocks";
import CancelCompletionForm from "../components/CancelCompletionForm/CancelCompletionForm";
import {ModalWindow} from "../components/ModalWindow/ModalWindow";
import {useForm} from "react-hook-form";
import AddBlockForm from "../components/AddBlockForm/AddBlockForm";
import EditBlockForm from "../components/EditBlockForm/EditBlockForm";

const BlocksPage = () => {
    const dispatch = useDispatch();
    const isAuth = useAuth().isAuth;
    const navigate = useNavigate();

    const blocks = useBlocks();

    useEffect(() => {
        dispatch(getBlocks());
        dispatch(removeTask());
    }, []);

    const [addBlockModalActive, setAddBlockModalActive] = useState(false);
    const [editBlockModalActive, setEditBlockModalActive] = useState(false);
    const [editBlock, setEditBlock] = useState(null);

    const {register: registerAddBlock, reset: resetAddBlock, handleSubmit: handleSubmitAddBlock, formState: {errors: errorsAddBlock}} = useForm({
        defaultValues: {
            addBlockName: ''
        },
        mode: "onBlur"
    });

    const {register: registerEditBlock, reset: resetEditBlock, handleSubmit: handleSubmitEditBlock, formState: {errors: errorsEditBlock}} = useForm({
        defaultValues: {
            editBlockName: ''
        },
        mode: "onBlur"
    });

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


    const headers = [
        {type: "header", text: 'Название', alignment: "left", width: "1272px"}
    ]

    if(blocks.isLoading)
        return <Loading/>

    return (
        <>
            <PageTitle title="Блоки"/>


            <TableRow cells={headers} isHeader/>
            {
                blocks.length === 0 ?
                    <TableRow cells={[{type: "text", text: "Нет блоков!", alignment: "center", width: "1272px"}]}/> :
                    <></>
            }
            {
                blocks.blocks.map(block => {
                    let cells = [
                        {type: "text", text: block.value, alignment: "left", width: "1232px"},
                        {type: "deleteBlock", id: block.id, alignment: "left", width: "40px"}
                    ]
                    return <TableRow cells={cells}
                                     block={block}
                                     setEditBlock={setEditBlock}
                                     resetEditBlock={resetEditBlock}
                                     setActive={setEditBlockModalActive}
                    />
                })

            }

            <div className={s.serviceButtons}>
                <Button onClick={()=>setAddBlockModalActive(true)}>Новый блок</Button>
            </div>

            <ModalWindow active={addBlockModalActive}
                         setActive={setAddBlockModalActive}
                         onClose={()=>resetAddBlock({
                             addBlockName: ''
                         })}>
                <AddBlockForm handleSubmit={handleSubmitAddBlock}
                                      errors={errorsAddBlock}
                                      register={registerAddBlock}
                                      setActive={setAddBlockModalActive}
                                      reset={resetAddBlock}
                />
            </ModalWindow>
            <ModalWindow active={editBlockModalActive}
                         setActive={setEditBlockModalActive}
                         onClose={()=>{
                             resetEditBlock({ editBlockName: ''});
                             setEditBlock(null);
                         }}>
                <EditBlockForm handleSubmit={handleSubmitEditBlock}
                               errors={errorsEditBlock}
                               register={registerEditBlock}
                               setActive={setEditBlockModalActive}
                               setEditBlock={setEditBlock}
                               editBlock={editBlock}
                               reset={resetEditBlock}
                               block={editBlock}
                />
            </ModalWindow>
        </>
    );
};

export default BlocksPage;
