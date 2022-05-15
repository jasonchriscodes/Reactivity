import { Activity } from "./../models/activity";
import { makeAutoObservable } from "mobx";
import agent from "../api/agent";

export default class ActivityStore {
  activities: Activity[] = [];
  selectedActivity: Activity | null = null;
  editMode = false;
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  loadActivities = async () => {
    this.loadingInitial = true;
    try {
      const activities = await agent.Activities.list();
      activities.forEach((activity) => {
        activity.date = activity.date.split("T")[0]; // only date not include time info
        this.activities.push(activity); // unlike Redux, we can mutate the state here
      });
      this.loadingInitial = false;
    } catch (error) {
      console.log(error);
      this.loadingInitial = false;
    }
  };
}
