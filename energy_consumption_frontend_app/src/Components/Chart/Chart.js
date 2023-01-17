import React from "react";
import {useState} from "react";
import DatePicker from 'react-datepicker'
import 'react-datepicker/dist/react-datepicker.css'
import Constants from "../../Constants";

import {
    Tooltip,
    BarChart,
    XAxis,
    YAxis,
    Legend,
    CartesianGrid,
    Bar,
  } from "recharts";
  
function Chart({currentUser}) {

    const [selectedDate, setSelectedDate] = useState(null);

    const [values, setValues] = useState([]);

    async function setH(date) {

        var h = date;
        h.setHours(0,0,0);
        setSelectedDate(h);
        var month = h.getUTCMonth() + 1;
        var day = h.getUTCDate() + 1;
        var year = h.getUTCFullYear();

        var d = year + "-" + month + "-" + day + "T";

        if(day/10 < 1 && month/10 < 1) {
          
          d = year + "-0" + month + "-0" + day + "T";
        }
        else if(day/10 < 1) {

          d = year + "-" + month + "-0" + day + "T";
        }
        else if(month/10 < 1) {

          d = year + "-0" + month + "-" + day + "T";
        }
        var name = currentUser;
        getChartValues(d + "03:00:00Z", name);
      }
      
      async function getChartValues(timestamp, name) {

        const url = Constants.url + "/GetEnergies"
        console.log(url);
        console.log(name);
        console.log(timestamp);
        
        await fetch(url, {
      
          method: "POST",
          headers: { 'Accept': '*/*', 'Content-Type': 'application/json' },
          body: JSON.stringify({

            "username": name,
            "timestamp": timestamp
          })
        })
        .then(response => response.json())
        .then(val => {

            console.log(val)
            setValues(val);
        })
        .catch((error) => {
      
          console.log(error);
          alert(error);
        })
      }

    const data = [
        { name: "00:00", power_consumption: values[0] },
        { name: "01:00", power_consumption: values[1] },
        { name: "02:00", power_consumption: values[2] },
        { name: "03:00", power_consumption: values[3] },
        { name: "04:00", power_consumption: values[4] },
        { name: "05:00", power_consumption: values[5] },
        { name: "06:00", power_consumption: values[6] },
        { name: "07:00", power_consumption: values[7] },
        { name: "08:00", power_consumption: values[8] },
        { name: "09:00", power_consumption: values[9] },
        { name: "10:00", power_consumption: values[10] },
        { name: "11:00", power_consumption: values[11] },
        { name: "12:00", power_consumption: values[12] },
        { name: "13:00", power_consumption: values[13] },
        { name: "14:00", power_consumption: values[14] },
        { name: "15:00", power_consumption: values[15] },
        { name: "16:00", power_consumption: values[16] },
        { name: "17:00", power_consumption: values[17] },
        { name: "18:00", power_consumption: values[18] },
        { name: "19:00", power_consumption: values[19] },
        { name: "20:00", power_consumption: values[20] },
        { name: "21:00", power_consumption: values[21] },
        { name: "22:00", power_consumption: values[22] },
        { name: "23:00", power_consumption: values[23] },
      ];

    return (

            <div>
                <div style={{ textAlign: "center", marginTop: "100px", marginLeft: "7%" }}>
                    
                    <BarChart
                        width={1250}
                        height={300}
                        data={data}
                        margin={{
                            top: 5,
                            right: 30,
                            left: 50,
                            bottom: 5,
                        }}
                        barSize={20}
                        >
                        <XAxis
                            dataKey="name"
                            scale="point"
                            padding={{ left: 10, right: 10 }}
                        />
                        <YAxis />
                        <Tooltip />
                        <Legend />
                        <CartesianGrid strokeDasharray="3 3" />
                        <Bar dataKey="power_consumption" fill="#a7bad3" background={{ fill: "#eee" }} />
                    </BarChart>
                </div>

                <div style={{ textAlign: "center", marginTop: "100px", marginLeft: "auto" }}>

                    <DatePicker
                        style={{ textAlign: "center"}}
                        selected={selectedDate}
                        onChange={date => setH(date)}
                        >

                    </DatePicker>

                </div>

            </div>

    ) 
}

export default Chart;