import React from 'react';
import s from './FilterDropdown.module.css';
import Form from "react-bootstrap/Form";
import classNames from "classnames/bind";
import Select from "react-select";
import { components } from "react-select";

const Option = (props) => {
    return (
        <div>
            <components.Option {...props} className={s.option}>
                <input
                    type="checkbox"
                    checked={props.isSelected}
                    onChange={() => null}
                />{" "}
                <label>{props.label}</label>
            </components.Option>
        </div>
    );
};

function FilterDropdown(props) {
    const dropdownStyles = {

        control: (styles) => ({
            ...styles,
            border: '1px solid #565A5A',
            borderRadius: '5px',
            minHeight: '48px',
            minWidth: props.minWidth??'100px'
        }),
        option: (styles) => {
            return {
                ...styles,
                fontFamily:'\'Open Sans\', sans-serif',
                fontSize: '16px',
                fontWeight: '500',
                fontStyle: 'normal',
                display: 'flex',
                flexDirection: 'row'
            };
        },
        input: (styles) => ({ ...styles, color: 'black', fontFamily:'\'Open Sans\', sans-serif', fontSize: '16px', paddingLeft: '6px' }),
        placeholder: (styles) => ({ ...styles, color: 'black', fontFamily:'\'Open Sans\', sans-serif', fontSize: '16px'}),
        singleValue: (styles, { data }) => ({ ...styles, fontFamily:'\'Open Sans\', sans-serif', fontSize: '16px', paddingLeft: '6px' }),
    };
    console.log(props.isMulti)
    console.log(!props.isMulti?props.options[0]:{})
    return (

        <div>
            {
                props.title ?
                    <p className={s.title}>{props.title}</p> :<></>
            }

            <Select
                className={s.dropdown}
                defaultValue={!props.isMulti?props.options[0]:{}}
                isDisabled={false}
                isLoading={!props.options.length}
                isClearable={false}
                isRtl={false}
                isSearchable={false}
                // name="department"
                options={props.options}
                styles={dropdownStyles}
                onChange={props.onChange}
                closeMenuOnSelect={!props.isMulti}
                hideSelectedOptions={!props.isMulti}
                placeholder={props.placeholder}
                controlShouldRenderValue={!props.isMulti}
                isMulti={props.isMulti}
                components={props.isMulti?{
                    Option
                }:{}}
            />
        </div>
    )
}

export default FilterDropdown;
