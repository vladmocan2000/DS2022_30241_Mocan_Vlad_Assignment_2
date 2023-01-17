import React from "react";
import UserTable from "../../Components/UserTable/UserTable";
import "./ManageUsers.css"

const ManageUsers = ({currentUser, users, setUsers}) => {

    if(currentUser !== "") {

        return (

            <div>
    
                <UserTable users={users} setUsers={setUsers}/>

            </div>
    
        );
    }
    else {

        return (

            <div className="notLoggedInMessage">You are not logged in!</div>
        )
    }
    
}

export default ManageUsers;