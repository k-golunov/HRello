import React, {useState} from 'react';
import s from './TableRow.module.css';
import TableCell from "../TableCell/TableCell";
import classNames from "classnames/bind";
import Status from "../Status/Status";
import {useNavigate} from "react-router-dom";
import {toast} from "react-toastify";
import {useDispatch} from "react-redux";
import {deleteUser} from "../../store/slices/userSlice";
import {getUsers} from "../../store/slices/usersSlice";
import {deleteBlock, getBlocks} from "../../store/slices/blocksSlice";

function TableRow(props) {
    const navigate = useNavigate();
    const dispatch = useDispatch();

    const [isLoadingDeleteInvitation, setIsLoadingDeleteInvitation] = useState(false);
    const [isLoadingDeleteBlock, setIsLoadingDeleteBlock] = useState(false);

    const blocks = {
        'Selection': 'Подбор',
        'Adaptation':'Адаптация',
        'StaffDevelopment': 'Развитие персонала',
        'HRSupport': 'HR-сопровождение',
        'CorporateCulture': 'Корпоративная культура',
        'PersonnelAccountingAndSalary': 'Кадровый учет и з/п',
        'HRBrandExternal': 'HR-бренд внешний',
        'InternalWork': 'Внутренняя работа',
        'Estimation': 'Оценка'
    }

    const status = {
        'OnChecking': 'На проверке',
        'OnRework': 'На доработку',
        'InWork': 'В работе',
        'AwaitingCancellation': 'Ожидает отмены',
        'Canceled': 'Отменена',
        'CompletionCheck': 'Проверка завершения',
        'Completed': 'Завершена'
    }

    const unsecuredCopyToClipboard = (text) => {
        const textArea = document.createElement("textarea");
        textArea.value= text;
        document.body.appendChild(textArea);
        textArea.focus();
        textArea.select();
        try {
            document.execCommand('copy');
        } catch(err) {
            console.error('Unable to copy to clipboard',err);
        }

        document.body.removeChild(textArea)
    };


    return (
        <div className={classNames(s.tableRow, props.isHeader?s.tableRowHeader:"")}
             onClick={props.taskID? ()=>navigate("/task/"+props.taskID) : props.block ? () => {
                 props.setEditBlock(props.block);
                 props.resetEditBlock({editBlockName:props.block.value})
                 props.setActive(true);
             }: () => {}
        }>
            {
                props.cells.map(cell =>{
                    if(cell.type === "text")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <p className={s.text}>
                                {cell.text}
                            </p>
                    </TableCell>

                    if(cell.type === "percent")
                        return <TableCell width={cell.width}>{cell.percent === -1? "-":cell.percent+"%"}</TableCell>

                    if(cell.type === "header")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <p className={classNames(s.header)}>
                                {cell.text}
                            </p>
                    </TableCell>

                    if(cell.type === "block")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <p className={classNames(s.text)}>
                                {blocks[cell.block]}
                            </p>
                        </TableCell>

                    if(cell.type === "status")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <Status type={cell.status}/>
                        </TableCell>

                    if(cell.type === "copyLink")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <div className={s.copyLink} onClick={()=> {

                                if (window.isSecureContext && navigator.clipboard) {
                                    navigator.clipboard.writeText(cell.link).then(()=>toast.success('Ссылка успешно скопирована!', {
                                            position: "bottom-right",
                                            autoClose: 2000,
                                            hideProgressBar: false,
                                            closeOnClick: true,
                                            pauseOnHover: true,
                                            draggable: true,
                                            progress: undefined,
                                            theme: "colored",
                                        })
                                    )
                                } else {
                                    unsecuredCopyToClipboard(cell.link);
                                    toast.success('Ссылка успешно скопирована!', {
                                        position: "bottom-right",
                                        autoClose: 2000,
                                        hideProgressBar: false,
                                        closeOnClick: true,
                                        pauseOnHover: true,
                                        draggable: true,
                                        progress: undefined,
                                        theme: "colored",
                                    })
                                }


                            }}>Скопировать</div>
                        </TableCell>

                    if(cell.type === "deleteInvention")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <svg width="11" height="11" viewBox="0 0 11 11" fill="none" xmlns="http://www.w3.org/2000/svg" onClick={()=>{
                                if(!isLoadingDeleteInvitation)
                                {
                                    setIsLoadingDeleteInvitation(true);
                                    dispatch(deleteUser(cell.id)).then(() => {
                                        dispatch(getUsers());
                                        setIsLoadingDeleteInvitation(false)
                                    });
                                }
                            }}>
                                <path d="M0.134398 0.78333C0.0917886 0.740721 0.0579891 0.690136 0.034929 0.634464C0.0118689 0.578792 4.48964e-10 0.519123 0 0.458864C-4.48964e-10 0.398605 0.0118689 0.338936 0.034929 0.283264C0.0579891 0.227592 0.0917886 0.177008 0.134398 0.134398C0.177008 0.0917886 0.227592 0.0579891 0.283264 0.034929C0.338936 0.0118689 0.398605 -4.48964e-10 0.458864 0C0.519123 4.48964e-10 0.578792 0.0118689 0.634464 0.034929C0.690136 0.0579891 0.740721 0.0917886 0.78333 0.134398L5.5 4.85198L10.2167 0.134398C10.2593 0.0917886 10.3099 0.0579891 10.3655 0.034929C10.4212 0.0118689 10.4809 0 10.5411 0C10.6014 0 10.6611 0.0118689 10.7167 0.034929C10.7724 0.0579891 10.823 0.0917886 10.8656 0.134398C10.9082 0.177008 10.942 0.227592 10.9651 0.283264C10.9881 0.338936 11 0.398605 11 0.458864C11 0.519123 10.9881 0.578792 10.9651 0.634464C10.942 0.690136 10.9082 0.740721 10.8656 0.78333L6.14802 5.5L10.8656 10.2167C10.9082 10.2593 10.942 10.3099 10.9651 10.3655C10.9881 10.4212 11 10.4809 11 10.5411C11 10.6014 10.9881 10.6611 10.9651 10.7167C10.942 10.7724 10.9082 10.823 10.8656 10.8656C10.823 10.9082 10.7724 10.942 10.7167 10.9651C10.6611 10.9881 10.6014 11 10.5411 11C10.4809 11 10.4212 10.9881 10.3655 10.9651C10.3099 10.942 10.2593 10.9082 10.2167 10.8656L5.5 6.14802L0.78333 10.8656C0.740721 10.9082 0.690136 10.942 0.634464 10.9651C0.578792 10.9881 0.519123 11 0.458864 11C0.398605 11 0.338936 10.9881 0.283264 10.9651C0.227592 10.942 0.177008 10.9082 0.134398 10.8656C0.0917886 10.823 0.0579891 10.7724 0.034929 10.7167C0.0118689 10.6611 0 10.6014 0 10.5411C0 10.4809 0.0118689 10.4212 0.034929 10.3655C0.0579891 10.3099 0.0917886 10.2593 0.134398 10.2167L4.85198 5.5L0.134398 0.78333Z" fill="#565A5A"/>
                            </svg>
                        </TableCell>

                    if(cell.type === "deleteBlock")
                        return <TableCell width={cell.width} alignment={cell.alignment}>
                            <svg width="11" height="11" viewBox="0 0 11 11" fill="none" xmlns="http://www.w3.org/2000/svg" onClick={e=>{
                                if(!isLoadingDeleteBlock)
                                {
                                    setIsLoadingDeleteBlock(true);
                                    dispatch(deleteBlock(cell.id)).then(() => {
                                        dispatch(getBlocks());
                                        setIsLoadingDeleteBlock(false)
                                    });
                                }
                                if (!e) e = window.event;
                                e.cancelBubble = true;
                                if (e.stopPropagation) e.stopPropagation();
                            }}>
                                <path className={s.cross} d="M0.134398 0.78333C0.0917886 0.740721 0.0579891 0.690136 0.034929 0.634464C0.0118689 0.578792 4.48964e-10 0.519123 0 0.458864C-4.48964e-10 0.398605 0.0118689 0.338936 0.034929 0.283264C0.0579891 0.227592 0.0917886 0.177008 0.134398 0.134398C0.177008 0.0917886 0.227592 0.0579891 0.283264 0.034929C0.338936 0.0118689 0.398605 -4.48964e-10 0.458864 0C0.519123 4.48964e-10 0.578792 0.0118689 0.634464 0.034929C0.690136 0.0579891 0.740721 0.0917886 0.78333 0.134398L5.5 4.85198L10.2167 0.134398C10.2593 0.0917886 10.3099 0.0579891 10.3655 0.034929C10.4212 0.0118689 10.4809 0 10.5411 0C10.6014 0 10.6611 0.0118689 10.7167 0.034929C10.7724 0.0579891 10.823 0.0917886 10.8656 0.134398C10.9082 0.177008 10.942 0.227592 10.9651 0.283264C10.9881 0.338936 11 0.398605 11 0.458864C11 0.519123 10.9881 0.578792 10.9651 0.634464C10.942 0.690136 10.9082 0.740721 10.8656 0.78333L6.14802 5.5L10.8656 10.2167C10.9082 10.2593 10.942 10.3099 10.9651 10.3655C10.9881 10.4212 11 10.4809 11 10.5411C11 10.6014 10.9881 10.6611 10.9651 10.7167C10.942 10.7724 10.9082 10.823 10.8656 10.8656C10.823 10.9082 10.7724 10.942 10.7167 10.9651C10.6611 10.9881 10.6014 11 10.5411 11C10.4809 11 10.4212 10.9881 10.3655 10.9651C10.3099 10.942 10.2593 10.9082 10.2167 10.8656L5.5 6.14802L0.78333 10.8656C0.740721 10.9082 0.690136 10.942 0.634464 10.9651C0.578792 10.9881 0.519123 11 0.458864 11C0.398605 11 0.338936 10.9881 0.283264 10.9651C0.227592 10.942 0.177008 10.9082 0.134398 10.8656C0.0917886 10.823 0.0579891 10.7724 0.034929 10.7167C0.0118689 10.6611 0 10.6014 0 10.5411C0 10.4809 0.0118689 10.4212 0.034929 10.3655C0.0579891 10.3099 0.0917886 10.2593 0.134398 10.2167L4.85198 5.5L0.134398 0.78333Z" fill="#565A5A"/>
                            </svg>
                        </TableCell>
                })
            }
        </div>
    )
}

export default TableRow;
