import React from 'react';
import { useNavigate } from "react-router-dom";

import "./Navbar.css";

const Navbar = ({currentUser, currentUserRole}) => {

    let navigate = useNavigate();

    const loginButtonPressed = () => {

        navigate("/authentication/login");
    }

    const registerButtonPressed = () => {

        navigate("/authentication/register");
    }

    const dashboardButtonPressed = () => {

        navigate("/dashboard/" + currentUser);
    }

    const notificationsButtonPressed = () => {

        navigate("/notifications/" + currentUser);
    }

    const manageDevicesButtonPressed = () => {

        navigate("/managedevices/" + currentUser);
    }

    const manageUsersButtonPressed = () => {

        navigate("/manageusers");
    }

    return (
        
        <div className='navbar'>
            <ul>
                {
                    (currentUser === "") 
                        &&
                    (<button className='button' onClick={loginButtonPressed}>Login</button>)
                }

                {
                    (currentUser === "") 
                        &&
                    (<button className='button' onClick={registerButtonPressed}>Register</button>)
                }

                {
                    (currentUser !== "" && currentUserRole === "client") 
                        &&
                    (<button className='button' onClick={dashboardButtonPressed}>Dashboard</button>)
                }
                {
                    (currentUser !== "" && currentUserRole === "client") 
                        &&
                    (<button className='button' onClick={notificationsButtonPressed}>Notifications</button>)
                }
                {
                    (currentUser !== "") 
                        &&
                    (<button className='button' onClick={manageDevicesButtonPressed}>Manage Devices</button>)
                }
                {
                    (currentUser !== ""  && currentUserRole === "administrator") 
                        &&
                    (<button className='button' onClick={manageUsersButtonPressed}>Manage Users</button>)
                }
            </ul>
        </div>
    );

};
  
export default Navbar;