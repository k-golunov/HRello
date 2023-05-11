
import React from 'react';
import s from './Tag.module.css';
import Form from "react-bootstrap/Form";

import classNames from "classnames/bind";

function Tag(props) {
    return (
        <div className={s.tag}>
            <p>{props.children}</p>
        </div>
    )
}

export default Tag;
