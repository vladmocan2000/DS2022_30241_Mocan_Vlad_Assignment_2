import React from "react"
import {useState} from "react";
import "./DeviceTable.css"
import "bootstrap/dist/css/bootstrap.min.css"
import Constants from "../../Constants";

function DeviceTable({currentUser, currentUserDevices, currentUserRole, setCurrentUserDevices}) {

    const [id, setId] = useState("");
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [address, setAddress] = useState("");
    const [maximumHourlyEnergyConsumption, setMaximumHourlyEnergyConsumption] = useState(0);
    const [username, setUsername] = useState(currentUser);

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

    async function updateDevice() {
  
        const url = Constants.url + "/UpdateDevice"
      
        await fetch(url, {
      
          method: "PUT",
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "id": id,
            "name": name,
            "description": description,
            "address": address,
            "maximumHourlyEnergyConsumption": maximumHourlyEnergyConsumption,
            "username": username
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

    async function deleteDevice(id) {
  
        const url = Constants.url + "/DeleteDevice"
 
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

    async function createDevice() {
  
        const url = Constants.url + "/CreateDevice"
      
        await fetch(url, {
      
          method: "POST",
          headers: {'Accept': '*/*', 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "name": name,
            "description": description,
            "address": address,
            "maximumHourlyEnergyConsumption": maximumHourlyEnergyConsumption,
            "username": username
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

    const updateClicked = (event, deviceId) => {

        const device = currentUserDevices.find(device => device.id === deviceId);
        setId(device.id);
        setName(device.name);
        setDescription(device.description);
        setAddress(device.address);
        setMaximumHourlyEnergyConsumption(device.maximumHourlyEnergyConsumption);
        setUsername(device.username);
    }

    const deleteClicked = async (event, id) => {

        await deleteDevice(id);
        if(currentUserRole === "client") {

            await getCurrentUserDevices();
        }
        else {

            await getAllDevices();
        }
    }

    const createUpdateClicked = async () => {

        const device = currentUserDevices.find(device => device.name === name);
        if(device === undefined) {

            await createDevice();
            
            if(currentUserRole === "client") {

                await getCurrentUserDevices();
            }
            else {
    
                await getAllDevices();
            }
        }
        else {

            await updateDevice();

            if(currentUserRole === "client") {

                await getCurrentUserDevices();
            }
            else {
    
                await getAllDevices();
            }
        }
    }

    if(currentUserRole === "client") {

        return (

            <div>
    
                <table className="table">
        
                    <thead>
    
                    <tr>
                        <th className="tableHeading" scope="col">Name</th>
                        <th className="tableHeading" scope="col">Description</th>
                        <th className="tableHeading" scope="col">Address</th>
                        <th className="tableHeading" scope="col">Max energy/h</th>
                        <th className="tableHeading" scope="col">Actions</th>
                    </tr>
    
                    <tr height="60px">
                        <td><input className="form-control mt-1" type="text" value={name} onChange={event => setName(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={description} onChange={event => setDescription(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={address} onChange={event => setAddress(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={maximumHourlyEnergyConsumption} onChange={event => setMaximumHourlyEnergyConsumption(event.target.value)}></input></td>
                        <td>
                            <div className="buttonTableList">
                
                                <button className="btn btn-success" onClick={createUpdateClicked}>Create / Update device</button>
    
                            </div>
                        </td>
                    </tr>
    
                    </thead>
            
                    <tbody>
    
                        {currentUserDevices.map((device) => (
            
                            <tr key={device.id}>
                            <td className="tableData">{device.name}</td>
                            <td className="tableData">{device.description}</td>
                            <td className="tableData">{device.address}</td>
                            <td className="tableData">{device.maximumHourlyEnergyConsumption} KWh</td>
                            <td className="tableData">
                                <div className="buttonTableList">
                    
                                    <button className="btn btn-success" onClick={event => deleteClicked(event, device.id)}>Delete</button>
                                    <button className="btn btn-success" onClick={event => updateClicked(event, device.id)}>Update</button>
    
                                </div>
                            </td>
                            </tr>
            
                        ))}
                        
                    </tbody>
        
                </table>
        
            </div>
        );
    }
    else {

        return (

            <div>
    
                <table className="table">
        
                    <thead>
    
                    <tr>
                        <th className="tableHeading" scope="col">Name</th>
                        <th className="tableHeading" scope="col">Description</th>
                        <th className="tableHeading" scope="col">Address</th>
                        <th className="tableHeading" scope="col">Max energy/h</th>
                        <th className="tableHeading" scope="col">Username</th>
                        <th className="tableHeading" scope="col">Actions</th>
                    </tr>
    
                    <tr height="60px">
                        <td><input className="form-control mt-1" type="text" value={name} onChange={event => setName(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={description} onChange={event => setDescription(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={address} onChange={event => setAddress(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={maximumHourlyEnergyConsumption} onChange={event => setMaximumHourlyEnergyConsumption(event.target.value)}></input></td>
                        <td><input className="form-control mt-1" type="text" value={username} onChange={event => setUsername(event.target.value)}></input></td>
                        <td>
                            <div className="buttonTableList">
                
                                <button className="btn btn-success" onClick={createUpdateClicked}>Create / Update device</button>
    
                            </div>
                        </td>
                    </tr>
    
                    </thead>
            
                    <tbody>
    
                        {currentUserDevices.map((device) => (
            
                            <tr key={device.id}>
                            <td className="tableData">{device.name}</td>
                            <td className="tableData">{device.description}</td>
                            <td className="tableData">{device.address}</td>
                            <td className="tableData">{device.maximumHourlyEnergyConsumption} KWh</td>
                            <td className="tableData">{device.username}</td>
                            <td className="tableData">
                                <div className="buttonTableList">
                    
                                    <button className="btn btn-success" onClick={event => deleteClicked(event, device.id)}>Delete</button>
                                    <button className="btn btn-success" onClick={event => updateClicked(event, device.id)}>Update</button>
    
                                </div>
                            </td>
                            </tr>
            
                        ))}
                        
                    </tbody>
        
                </table>
        
            </div>
        );
    }
}
export default DeviceTable;