import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import { Container, Header, List } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]); // set activity initial state

  // fetch activities from API server
  useEffect(() => {
    // function with no parameter
    axios
      .get<Activity[]>("http://localhost:5000/api/activities")
      .then((response) => {
        setActivities(response.data); // set activity to the response we get from axios, get type safety from activity.ts
      });
  }, []); // use empty array to ensure function only runs 1 times, not endless loop
  // <> and </> is Fragment shortcut
  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <List>
          {activities.map(
            (
              activity // need to add type of activity
            ) => (
              <List.Item key={activity.id}>{activity.title}</List.Item>
            )
          )}
        </List>
      </Container>
    </>
  );
}

export default App;
