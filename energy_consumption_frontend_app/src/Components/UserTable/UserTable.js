import React from "react"
import {useState} from "react";
import "bootstrap/dist/css/bootstrap.min.css"
import Constants from "../../Constants";

function UserTable({users, setUsers}) {

    const [id, setId] = useState("");
    const [username, setUsername] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [role, setRole] = useState("");
    const [password, setPassword] = useState("");

    async function getAllUsers() {

        const url = Constants.url + "/GetAllUsersDetails"

        await fetch(url, {
      
            method: "GET"
        })
        .then(response => response.json())
        .then(users => {
  
          setUsers(users);
        })
    }

    async function updateUser() {
  
        const url = Constants.url + "/UpdateUser"
      
        await fetch(url, {
      
          method: "PUT",
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "id": id,
            "username": username,
            "firstname": firstname,
            "lastname": lastname,
            "role": role
          })
        })
        .then(function(response) {
            
            if(response.status === 200) {

            }
            else {

                response.text().then(function(data){

                    console.log(data);
                })
            }
        }) 
        .catch((error) => {
      
          console.log(error);
          alert(error);
        })
    }

    async function deleteUser(id) {
  
        const url = Constants.url + "/DeleteUser"
 
        await fetch(url, {
      
          method: "DELETE",
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "id": id
          })
        })
        .then(function(response) {
            
            if(response.status === 200) {

            }
            else {

                response.text().then(function(data){

                    console.log(data);
                })
            }
        }) 
        .catch((error) => {
      
          console.log(error);
          alert(error);
        })
    }

    async function createUser() {
  
        const url = Constants.url + "/Register"
      
        await fetch(url, {
      
          method: "POST",
          headers: {'Accept': '*/*', 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "username": username,
            "firstname": firstname,
            "lastname": lastname,
            "role": role,
            "password": password
          })
        })
        .then(function(response) {
            
            if(response.status === 200) {

            }
            else {

                response.text().then(function(data){

                    console.log(data);
                })
            }
        }) 
        .catch((error) => {
      
          console.log(error);
          alert(error);
        })
    }

    const updateClicked = (event, userId) => {

        const user = users.find(user => user.id === userId);
        setId(user.id);
        setUsername(user.username);
        setFirstname(user.firstname);
        setLastname(user.lastname);
        setRole(user.role);
    }

    const deleteClicked = async (event, id) => {

        await deleteUser(id);
        await getAllUsers();
    }

    const createUpdateClicked = async () => {

        const user =users.find(user => user.username === username);
        if(user === undefined) {

            await createUser();
            await getAllUsers();
        }
        else {

            await updateUser();
            await getAllUsers();
        }
    }

        return (

            <div>
    
                <table className="table">
        
                    <thead>
    
                    <tr>
                        <th className="tableHeading" scope="col">Username</th>
                        <th className="tableHeading" scope="col">Firstname</th>
                        <th className="tableHeading" scope="col">Lastname</th>
                        <th className="tableHeading" scope="col">Role</th>
                        <th className="tableHeading" scope="col">Password</th>
                        <th className="tableHeading" scope="col">Actions</th>
                    </tr>
    
                    <tr height="60px">
                        <td><input className="form-control mt-1" type="text" value={username} onChange={event => setUsername(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={firstname} onChange={event => setFirstname(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={lastname} onChange={event => setLastname(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={role} onChange={event => setRole(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={password} onChange={event => setPassword(event.target.value)}></input></td>
                        <td>
                            <div className="buttonTableList">
                
                                <button className="btn btn-success" onClick={createUpdateClicked}>Create / Update user</button>
    
                            </div>
                        </td>
                    </tr>
    
                    </thead>
            
                    <tbody>
    
                        {users.map((user) => (
            
                            <tr key={user.id}>
                            <td className="tableData">{user.username}</td>
                            <td className="tableData">{user.firstname}</td>
                            <td className="tableData">{user.lastname}</td>
                            <td className="tableData">{user.role}</td>
                            <td className="tableData"></td>
                            <td className="tableData">
                                <div className="buttonTableList">
                    
                                    <button className="btn btn-success" onClick={event => deleteClicked(event, user.id)}>Delete</button>
                                    <button className="btn btn-success" onClick={event => updateClicked(event, user.id)}>Update</button>
    
                                </div>
                            </td>
                            </tr>
            
                        ))}
                        
                    </tbody>
        
                </table>
        
            </div>
        );
}
    
export default UserTable;