import React, {useState} from "react";
import "bootstrap/dist/css/bootstrap.min.css"
import { useNavigate } from "react-router-dom"
import "./Register.css"
import Constants from "../../Constants";

const Register = () => {

    let navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [firstname, setFirstname] = useState("");
    const [lastname, setLastname] = useState("");
    const [password, setPassword] = useState("");
    const [role, setRole] = useState("");

    const [error, setError] = useState("");

    const handleSubmit = async event => {

        event.preventDefault();
      
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

                setError("");
                navigate("/authentication/login");
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
        
        <div className="registerContainer">
        
            <form onSubmit={handleSubmit} className="form">

                <div className="formContent">

                    <h3 className="formTitle">Register</h3>

                    <div className="form-group mt-3">

                        <label className="label">Username</label>
                        <input className="form-control mt-1" placeholder="Username" onChange={event => setUsername(event.target.value)}/>

                    </div>

                    <div className="form-group mt-3">

                        <label className="label">Firstname</label>
                        <input className="form-control mt-1" placeholder="Firstname" onChange={event => setFirstname(event.target.value)}/>

                    </div>

                    <div className="form-group mt-3">

                        <label className="label">Lastname</label>
                        <input className="form-control mt-1" placeholder="Lasttname" onChange={event => setLastname(event.target.value)}/>

                    </div>

                    <div className="form-group mt-3">

                        <label className="label">Role</label>
                        <input className="form-control mt-1" placeholder="Role" onChange={event => setRole(event.target.value)}/>

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

export default Register;