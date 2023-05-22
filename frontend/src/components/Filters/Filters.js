import React from 'react';
import s from './Filters.module.css';
import Form from "react-bootstrap/Form";
import classNames from "classnames/bind";
import Select from "react-select";
import { components } from "react-select";
import FilterDropdown from "../FilterDropdown/FilterDropdown";

function Filters(props) {
    return (
        <div className={s.filters}>
            {
                props.filters.map(filter =>{
                    return <FilterDropdown options={filter.options}
                                           onChange={filter.setState}
                                           placeholder={filter.placeholder}
                                           isMulti={filter.isMulti}
                                           minWidth={filter.minWidth??'100px'}
                    />
                })
            }
        </div>
    )
}

export default Filters;
