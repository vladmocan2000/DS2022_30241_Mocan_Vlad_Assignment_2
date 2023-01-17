import React, {useState} from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom"
import Login from './Pages/Login/Login';
import Register from './Pages/Register/Register';
import Dashboard from './Pages/Dashboard/Dashboard';
import Navbar from './Navbar/Navbar';
import ManageDevices from "./Pages/ManageDevices/ManageDevices";
import ManageUsers from "./Pages/ManageUsers/ManageUsers";
import { Notifications } from "./Pages/Notifications/Notifications";
import { useEffect } from 'react';

function App() {

    const [currentUser, setCurrentUser] = useState("");
    const [currentUserRole, setCurrentUserRole] = useState("");
    const [currentUserDevices, setCurrentUserDevices] = useState([]);
    const [users, setUsers] = useState([]);

    const [, setSocket] = useState(null);
    const [notificationMessage, setNotificationMessage] = useState('');

    useEffect(() => {

      if(currentUser !== '') {

        const newSocket = new WebSocket('ws://localhost:5000/ws');

        newSocket.onopen = () => {

          newSocket.send(currentUser);
          console.log('Socket connection opened');
        };
    
        newSocket.onmessage = event => {
    
          setNotificationMessage(event.data);
          console.log('Received message:', event.data);
        };
    
        newSocket.onclose = () => {
          console.log('Socket connection closed');
        };
    
        setSocket(newSocket); 
      }
    },[currentUser])

    return (

      <React.StrictMode>
        <div>
          
            <BrowserRouter>

              <Navbar currentUser={currentUser} currentUserRole={currentUserRole}/>

              <Routes>
                <Route path='/authentication/login' element={<Login setUsers={setUsers} setCurrentUser={setCurrentUser} setCurrentUserRole={setCurrentUserRole} setCurrentUserDevices={setCurrentUserDevices}/>}/>
                <Route path='/authentication/register' element={<Register/>}/>
                <Route path='/dashboard/:username' element={<Dashboard currentUser={currentUser} currentUserRole={currentUserRole} notificationMessage={notificationMessage}/>}/>
                <Route path='/managedevices/:username' element={<ManageDevices currentUser={currentUser} currentUserRole={currentUserRole} currentUserDevices={currentUserDevices} setCurrentUserDevices={setCurrentUserDevices}/>}/>
                <Route path='/manageusers' element={<ManageUsers currentUser={currentUser} users={users} setUsers={setUsers}/>}/>
                <Route path='/notifications/:username' element={<Notifications currentUser={currentUser}/>}/>
              </Routes>
            </BrowserRouter>
          
        </div>
      </React.StrictMode>

    );
}

export default App;
