import React from 'react';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import AuthorizationForm from "../components/AuthorizationForm/AuthorizationForm";
import PageTitle from "../components/PageTitle/PageTitle";
import InvitationForm from "../components/InvitationForm/InvitationForm";
import TableRow from "../components/TableRow/TableRow";

import s from "./Pages.module.css";

const InvitationsPage = () => {
    const headers = [
        {type: "header", text: 'Почта', alignment: "left", width: "250px"},
        {type: "header", text: 'ФИО', alignment: "left", width: "400px"},
        {type: "header", text: 'Дата отправки', alignment: "left", width: "174px"},
        {type: "header", text: 'Ссылка', alignment: "left", width: "200px"},
        {type: "header", text: 'Статус', alignment: "left", width: "260px"},
    ]

    const invitations = [
        {mail: "klm1302@yandex.ru", name: "Крашенинникова Любовь Михайловна", date: "26.02.2023", link: "123", status: "Wait"},
        {mail: "klm1302@yandex.ru", name: "Крашенинникова Любовь Михайловна", date: "26.02.2023", link: "123", status: "NotWorking"},
        {mail: "klm1302@yandex.ru", name: "Крашенинникова Любовь Михайловна", date: "26.02.2023", link: "123", status: "Registr"},
    ]

    return (
        <div>
            <PageTitle title="Приглашения"/>
            <InvitationForm/>
            <div className={s.InvitationsPageTable}>
                <TableRow cells={headers} isHeader/>
                {
                    invitations.length === 0 ?
                        <TableRow cells={[{type: "text", text: "Нет приглашений!", alignment: "center", width: "1272px"}]}/> :
                        <></>
                }
                {
                    invitations.map(invitation => {
                        let cells = [
                            {type: "text", text: invitation.mail, alignment: "left", width: "250px"},
                            {type: "text", text: invitation.name, alignment: "left", width: "400px"},
                            {type: "text", text: invitation.date, alignment: "left", width: "174px"},
                            {type: "copyLink", link: invitation.link, alignment: "left", width: "200px"},
                            {type: "status", status: invitation.status, alignment: "left", width: "260px"},
                        ]
                        return <TableRow cells={cells}/>
                    })
                }
            </div>

            <ToastContainer />
        </div>
    );
};

export default InvitationsPage;
