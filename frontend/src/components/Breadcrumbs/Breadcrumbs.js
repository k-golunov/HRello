import React from 'react';
import s from './Breadcrumbs.module.css';
import {Link} from "react-router-dom";

function Breadcrumbs(props) {
    const arr = [
        {id: 1, title: "Мои задачи", src: "/tasks/my"},
        {id: 2, title: "Просмотр задачи", src: "/tasks/all"}
    ]
    return (
        <div className={s.breadcrumbs}>
            {
                props.breadcrumbs.map(crumb => {
                    return <>
                        <svg width="12" height="12" viewBox="0 0 12 12" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                                  d="M8.51544 1.23468C8.55036 1.26952 8.57807 1.3109 8.59697 1.35646C8.61588 1.40202 8.62561 1.45086 8.62561 1.50018C8.62561 1.54951 8.61588 1.59835 8.59697 1.64391C8.57807 1.68947 8.55036 1.73085 8.51544 1.76568L4.28019 6.00018L8.51544 10.2347C8.58585 10.3051 8.62541 10.4006 8.62541 10.5002C8.62541 10.5998 8.58585 10.6953 8.51544 10.7657C8.44502 10.8361 8.34952 10.8757 8.24994 10.8757C8.15036 10.8757 8.05485 10.8361 7.98444 10.7657L3.48444 6.26568C3.44952 6.23085 3.42181 6.18947 3.4029 6.14391C3.384 6.09835 3.37427 6.04951 3.37427 6.00018C3.37427 5.95086 3.384 5.90202 3.4029 5.85646C3.42181 5.8109 3.44952 5.76952 3.48444 5.73468L7.98444 1.23468C8.01927 1.19976 8.06065 1.17205 8.10621 1.15315C8.15177 1.13424 8.20061 1.12451 8.24994 1.12451C8.29926 1.12451 8.3481 1.13424 8.39366 1.15315C8.43922 1.17205 8.4806 1.19976 8.51544 1.23468Z"
                                  fill="#818284"/>
                        </svg>

                        <Link className={s.breadcrumbsLink} to={crumb.src}>{crumb.title}</Link>
                        {/*<div className={s.icon}>*/}
                        {/*    <svg viewBox="0 0 24 24" focusable="false" className={s.breadcrumbsIcon}>*/}
                        {/*        <path fill="white" d="M10 6L8.59 7.41 13.17 12l-4.58 4.59L10 18l6-6z"/>*/}
                        {/*    </svg>*/}
                        {/*</div>*/}
                    </>
                })
            }
        </div>
    )
}

export default Breadcrumbs;
