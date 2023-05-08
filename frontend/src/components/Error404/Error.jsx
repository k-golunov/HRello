import React from 'react';
import s from './Error.module.css';
import phoneIcon from "../../img/phoneIcon.svg";
import mailIcon from "../../img/mailIcon.svg";

const Error = (props) => {
  return (
      <div className={s.error}>
          <div>
              <p className={s.title}>{props.title}</p>
              <p className={s.description}>{props.description}</p>
          </div>
      </div>);
};

export default Error;
