import React, {useState} from "react";
import "bootstrap/dist/css/bootstrap.min.css"
import { useNavigate } from "react-router-dom"
import "./Login.css"
import Constants from "../../Constants";

const Login = ({setUsers, setCurrentUser, setCurrentUserRole, setCurrentUserDevices}) => {

    let navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const [error, setError] = useState("");

    async function getCurrentUserDevices() {

        const url = Constants.url + "/GetUserDevicesDetails?Username=" + username

        await fetch(url, {
      
            method: "GET"
        })
        .then(response => response.json())
        .then(devices => {
  
          setCurrentUserDevices(devices);
        })
    }

    async function getAllDevices() {

        const url = Constants.url + "/GetAllDevicesDetails"

        await fetch(url, {
      
            method: "GET"
        })
        .then(response => response.json())
        .then(devices => {
  
          setCurrentUserDevices(devices);
        })
    }

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

    const handleSubmit = async (event) => {

        event.preventDefault();
      
        const url = Constants.url + "/Login"
        console.log(url)
        await fetch(url, {
      
            method: "POST",
            headers: {'Accept': '*/*', 'Content-Type': 'application/json' },
            body: JSON.stringify({

                "username": username,
                "password": password
            })
        })
        .then(async function(response) {
            
            if(response.status === 200) {

                // setError("");
                setCurrentUser(username)
                response.json().then(u => {

                    setCurrentUserRole(u.role);
                    if(u.role === "client") {

                        getCurrentUserDevices();

                        navigate("/dashboard/" + username);
                    }
                    else {

                        getAllDevices();
                        getAllUsers();
                        navigate("/managedevices/" + username);
                    }
                })
            }
            else {

                response.text().then(function(data){

                    setError(data);
                })
            }
        }) 
        .catch((error) => {
      
          console.log(error);
        })
    };

    return (

        <div className="loginContainer">
            
            <form onSubmit={handleSubmit} className="form">

                <div className="formContent">

                    <h3 className="formTitle">Login</h3>

                    <div className="form-group mt-3">

                        <label className="label">Email address</label>
                        <input className="form-control mt-1" placeholder="Username" onChange={event => setUsername(event.target.value)}/>

                    </div>

                    <div className="form-group mt-3">

                        <label className="label">Password</label>
                        <input className="form-control mt-1" placeholder="Password" onChange={event => setPassword(event.target.value)}/>
                        
                    </div>

                    <div className="d-grid gap-2 mt-3">

                        <button type="submit" className="btn btn-success">Submit</button>

                    </div>

                </div>
                
            </form>

            {error !== "" && <p className="errorMessage">{error}</p>}

        </div>

    )
}

export default Login;