import React from 'react';
import s from './ResultCell.module.css';

function ResultCell(props) {
    const colors = {
        "Green": "#A9F26F",
        "Yellow": "#FFF964",
        "Red": "#F27F6F",
    }
    return (
        <div className={s.resultCell} style={{backgroundColor: colors[props.color]}} onClick={()=>{
            props.setResultModalActive(true)
            props.setSelectedResult(props.result)
        }}>
            {props.text}
        </div>
    )
}

export default ResultCell;
