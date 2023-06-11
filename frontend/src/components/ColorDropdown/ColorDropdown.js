import React, {useState} from 'react';
import {GithubPicker} from 'react-color';
import s from "./ColorDropdown.module.css"

const ColorDropdown = (props) => {

    const [isOpenPicker, setIsOpenPicker] = useState(false)
    const [pickedColor, setPickedColor] = useState("#A9F26F")

    const handleChangeComplete = (color) => {
        setPickedColor(color.hex);
        setIsOpenPicker(false)
        console.log(color)
    };

    return (
        <div className={s.container}>
            <div className={s.titleContainer}>
                {
                    props.title ? <p className={s.title}>{props.title}</p> : ""
                }
            </div>
            <div className={s.colorViewerContainer} onClick={ () => {
                setIsOpenPicker(!isOpenPicker);
            }}>
                <span className={s.colorViewer}>
                    <div className={s.color} style={{backgroundColor: pickedColor}}/>

                </span>
            </div>
            {
                isOpenPicker ?
                    <GithubPicker className={s.picker}
                                  width={37}
                                  colors={["#A9F26F", "#FFF964", "#F27F6F"]}
                                  color={ pickedColor }
                                  onChangeComplete={ handleChangeComplete }
                    /> : <></>
            }
        </div>


    );
}

export default ColorDropdown;
