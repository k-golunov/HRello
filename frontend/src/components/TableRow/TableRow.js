import React from 'react';
import s from './TableRow.module.css';
import TableCell from "../TableCell/TableCell";
import classNames from "classnames/bind";
import Status from "../Status/Status";
import {useNavigate} from "react-router-dom";

function TableRow(props) {
    const navigate = useNavigate();

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

    return (
        <div className={classNames(s.tableRow, props.isHeader?s.tableRowHeader:"")} onClick={props.taskID? ()=>navigate("/task/"+props.taskID) : ()=>{}}>
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
                        <div className={s.copyLink}>Скопировать</div>
                    </TableCell>
                })
            }
        </div>
    )
}

export default TableRow;
