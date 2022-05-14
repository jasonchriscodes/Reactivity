import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import { Container, Header, List } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]); // set activity initial state
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined // undefine is not activity so we need union type which also means we can use undefined
  >(undefined); // set selected activity initial state
  const [editMode, setEditMode] = useState(false); // set edit mode initial state

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

  // function to handle activity selection
  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find((x) => x.id === id)); // finding matching object that matches id
  }

  // function to handle activity cancelation
  function handleCancelSelectActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) {
    // id? means id is optional
    // check if id is empty or not, if empty handleSelectActivity will be called, if not handleEditActivity will be called
    id ? handleSelectActivity(id) : handleCancelSelectActivity();
    setEditMode(true);
  }

  function handleFormClose() {
    setEditMode(false);
  }

  // if there is activity has id then it will set activity with the activity that passes in,
  // if not it will set a new activity
  function handleCreateOrEditActivity(activity: Activity) {
    activity.id
      ? setActivities([
          ...activities.filter((x) => x.id !== activity.id),
          activity,
        ])
      : setActivities([...activities, activity]);
    setSelectedActivity(activity); // display detail activity after done
  }

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activities}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
        />
      </Container>
    </>
  );
}

export default App;
