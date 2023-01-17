import React from "react";
import { useParams } from "react-router-dom"
import Chart from "../../Components/Chart/Chart";
import "./Dashboard.css"
import { notification } from "antd";
import { useEffect } from 'react';

const Dashboard = ({currentUser, currentUserRole, notificationMessage}) => {

    let {username} = useParams();

    const notify = (message) => {

        notification.open({message: "New Notification", description: "Energy consumption exeeded!"});
    };

    useEffect(() => {

        if(notificationMessage !== '') {
            
            var msg = notificationMessage;
            notify(msg);
        }
    },[notificationMessage])

    if(currentUser === username && currentUser !== "" && currentUserRole !== "administrator") {

        return (

            <div>

                <div>Dashboard {username}</div>
                <Chart currentUser={currentUser}/>
            </div>
    
        );
    }
    else {

        return (

            <div className="notLoggedInMessage">You are not logged in!</div>
        )
    }
    
}

export default Dashboard;