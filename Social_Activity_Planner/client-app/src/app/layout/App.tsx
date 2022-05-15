import { useEffect, useState } from "react";
import { Container } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { v4 as uuid } from "uuid";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

function App() {
  const { activityStore } = useStore();
  const [activities, setActivities] = useState<Activity[]>([]); // set activity initial state
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined // undefine is not activity so we need union type which also means we can use undefined
  >(undefined); // set selected activity initial state
  const [editMode, setEditMode] = useState(false); // set edit mode initial state
  const [submitting, setSubmitting] = useState(false); // set submitting initial state

  // fetch activities from API server
  useEffect(() => {
    activityStore.loadActivities();
  }, [activityStore]); // pass activity store as dependency
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
    setSubmitting(true);
    if (activity.id) {
      agent.Activities.update(activity).then(() => {
        setActivities([
          ...activities.filter((x) => x.id !== activity.id),
          activity,
        ]);
        setSelectedActivity(activity); // display detail activity after done
        setEditMode(false);
        setSubmitting(false);
      });
    } else {
      activity.id = uuid();
      agent.Activities.create(activity).then(() => {
        setActivities([...activities, activity]);
        setSelectedActivity(activity);
        setEditMode(false);
        setSubmitting(false);
      });
    }
  }

  function handleDeleteActivity(id: string) {
    setSubmitting(true);
    agent.Activities.delete(id).then(() => {
      setActivities([...activities.filter((x) => x.id !== id)]);
      setSubmitting(false);
    });
    setActivities([...activities.filter((x) => x.id !== id)]);
  }

  if (activityStore.loadingInitial)
    return <LoadingComponent content="Loading app" />;

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activityStore.activities}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDeleteActivity}
          submitting={submitting}
        />
      </Container>
    </>
  );
}

export default observer(App);
