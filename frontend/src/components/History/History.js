import React from 'react';
import s from './History.module.css';
import ReactLoading from "react-loading";
import HistoryRow from "../HistoryRow/HistoryRow";

function History(props) {
    return (
        <div className={s.historyContainer}>
           <div className={s.divider}/>
            <p className={s.title}>История</p>
            <div className={s.history}>
                {
                    props.history.map(historyRow => {
                        return <HistoryRow history={historyRow}/>
                    })
                }
            </div>
        </div>
    )
}

export default History;
