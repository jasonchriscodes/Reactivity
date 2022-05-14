import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';

function App() {
    const [activities, setActivities] = useState([]); // set activity initial state

    // fetch activities from API server 
    useEffect(() => { // function with no parameter 
        axios.get('http://localhost:5000/api/activities').then(response => {
            setActivities(response.data); // set activity to the response we get from axios
        })
    }, []) // use empty array to ensure function only runs 1 times, not endless loop
  return (
      <div>
          <Header as='h2' icon='users' content='ActivityBud' />
          <List>
              {activities.map((activity: any) => ( // need to add type of activity
                  <List.Item key={activity.id}>
                      {activity.title}
                  </List.Item>
              ))}
          </List>
    </div>
  );
}

export default App;
