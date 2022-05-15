import { observer } from "mobx-react-lite";
import { Grid } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { useStore } from "../../../app/stores/store";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";
import ActivityList from "./ActivityList";

interface Props {
  activities: Activity[];
  createOrEdit: (activity: Activity) => void;
  deleteActivity: (id: string) => void;
  submitting: boolean;
}

export default observer(function ActivityDashboard({
  activities,
  createOrEdit,
  deleteActivity,
  submitting,
}: Props) {
  const { activityStore } = useStore();
  const { selectedActivity, editMode } = activityStore;

  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList
          activities={activities}
          deleteActivity={deleteActivity}
          submitting={submitting}
        />
      </Grid.Column>
      <Grid.Column width="6">
        {/* && means anything to the right will execute as long as activities[0] is NOT null or undefine */}
        {selectedActivity && !editMode && <ActivityDetails />}
        {/* only display activity form on editMode */}
        {editMode && (
          <ActivityForm createOrEdit={createOrEdit} submitting={submitting} />
        )}
      </Grid.Column>
    </Grid>
  );
});
