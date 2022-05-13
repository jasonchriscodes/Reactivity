import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';

function App() {
    const [activities, setActivities] = useState([]); // set activity initial state

    // fetch activities from API server 
    useEffect(() => { // function with no parameter 
        axios.get('http://localhost:5000/api/activities').then(response => {
            console.log(response); // see axios in console
            setActivities(response.data); // set activity to the response we get from axios
        })
    }, []) // use empty array to ensure function only runs 1 times, not endless loop
  return (
    <div className="App">
      <header className="App-header">
              <img src={logo} className="App-logo" alt="logo" />
              <ul>
                  {activities.map((activity: any)=> ( // need to add type of activity
                      <li key={activity.id}>
                          {activity.title}
                      </li>
                   ))}
              </ul>
      </header>
    </div>
  );
}

export default App;
