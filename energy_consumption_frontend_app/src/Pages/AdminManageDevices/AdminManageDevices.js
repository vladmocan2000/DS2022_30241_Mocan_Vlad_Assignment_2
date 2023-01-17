import React from "react";
import { useParams } from "react-router-dom"
import DeviceTable from "../../Components/DeviceTable/DeviceTable";
import "./AdminManageDevices.css"

const AdminManageDevices = ({currentUser, currentUserDevices, setCurrentUserDevices}) => {

    let {username} = useParams();

    if(currentUser === username && currentUser !== "") {

        return (

            <div>
    
                <DeviceTable currentUserDevices={currentUserDevices} setCurrentUserDevices={setCurrentUserDevices}/>

            </div>
    
        );
    }
    else {

        return (

            <div className="notLoggedInMessage">You are not logged in!</div>
        )
    }
    
}

export default AdminManageDevices;