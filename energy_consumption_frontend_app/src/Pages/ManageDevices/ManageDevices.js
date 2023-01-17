import React from "react";
import { useParams } from "react-router-dom"
import DeviceTable from "../../Components/DeviceTable/DeviceTable";
import "./ManageDevices.css"

const ManageDevices = ({currentUser, currentUserRole, currentUserDevices, setCurrentUserDevices}) => {

    let {username} = useParams();

    if(currentUser === username && currentUser !== "") {

        return (

            <div>
    
                <DeviceTable currentUser={currentUser} currentUserDevices={currentUserDevices} currentUserRole={currentUserRole} setCurrentUserDevices={setCurrentUserDevices}/>

            </div>
    
        );
    }
    else {

        return (

            <div className="notLoggedInMessage">You are not logged in!</div>
        )
    }
    
}

export default ManageDevices;