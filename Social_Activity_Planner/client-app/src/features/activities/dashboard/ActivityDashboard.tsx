import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { selectedActivity, editMode } = activityStore;

  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList />
      </Grid.Column>
      <Grid.Column width="6">
        {/* && means anything to the right will execute as long as activities[0] is NOT null or undefine */}
        {selectedActivity && !editMode && <ActivityDetails />}
        {/* only display activity form on editMode */}
        {editMode && <ActivityForm />}
      </Grid.Column>
    </Grid>
  );
});
