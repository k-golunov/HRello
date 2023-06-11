import React from 'react';
import s from './TitleDropdown.module.css';
import Form from "react-bootstrap/Form";
import classNames from "classnames/bind";
import Select from "react-select";
import { components } from "react-select";

const indicatorSeparatorStyle = {
    alignSelf: 'stretch',
    // backgroundColor: "white",
    marginBottom: 0,
    marginTop: 0,
    width: 0,
};


const IndicatorSeparator = ({innerProps}) => {
    return <span style={indicatorSeparatorStyle} {...innerProps} />;
};

function TitleDropdown(props) {
    const dropdownStyles = {

        control: (styles) => ({
            ...styles,
            border: 0,
            boxShadow: 'none',
            borderRadius: '0px',
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
        singleValue: (base) => ({
            ...base,
            padding: 2,
            borderRadius: 0,
            display: 'flex',
            fontWeight: "500",
            fontSize: "24px",
            lineHeight: "33px",
        }),
        valueContainer: (styles) => ({ ...styles, paddingRight: "0"})
    };

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
                components={{
                    IndicatorSeparator
                }}
            />
        </div>
    )
}

export default TitleDropdown;
