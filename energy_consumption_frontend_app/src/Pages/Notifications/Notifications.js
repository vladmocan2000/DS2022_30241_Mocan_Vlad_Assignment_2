
import { Button, notification } from "antd";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom"

export const Notifications = ({currentUser}) => {

  let {username} = useParams();

  const [cnt, setCnt] = useState(0);
  const [socket, setSocket] = useState(null);
  const [message, setMessage] = useState('');

  useEffect(() => {
    
    if(username === currentUser && cnt === 0) {

      setCnt(1);
      const newSocket = new WebSocket('ws://localhost:5000/ws');

      newSocket.onopen = () => {
        newSocket.send(username);
        console.log('Socket connection opened');
      };

      newSocket.onmessage = event => {

        notify();
        console.log('Received message:', event.data);
        setMessage(event.data);
      };

      newSocket.onclose = () => {
        console.log('Socket connection closed');
      };

      setSocket(newSocket);
      // return () => {
      //   if (newSocket.readyState === 1) { // <-- This is important
      //       newSocket.close();
      //   }
      // };
    }
  }, []);
  /*useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          connection.on("ReceiveMessage", (message) => {
            notification.open({
              message: "New Notification",
              description: message,
            });
          });
        })
        .catch((error) => console.log(error));
    }
  }, [connection]);*/

  const sendMessage = () => {

    if (socket) {
      socket.send('Hello, world!');
    }
    else {
      console.log("error sending");
    }
  };

  const notify = () => {

    notification.open({message: "New Notification", description: "message"});
  };

  if(currentUser === username && currentUser !== "") {

    return (

        <div>
            <button onClick={notify}>baaaa</button>
            Oanaa

        </div>

    );
  }
  else {

      return (

          <div className="notLoggedInMessage">You are not logged in!</div>
      )
  }
  

  
};