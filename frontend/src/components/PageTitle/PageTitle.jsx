import React from 'react';
import s from './PageTitle.module.css';
import phoneIcon from "../../img/phoneIcon.svg";
import mailIcon from "../../img/mailIcon.svg";

const PageTitle = (props) => {
  return (
      <div className={s.pageTitle}>
          <p className={s.title}>{props.title}</p>
      </div>);
};

export default PageTitle;
