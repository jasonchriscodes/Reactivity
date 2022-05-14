import axios, { AxiosResponse } from 'axios';
import { TableBody } from 'semantic-ui-react';
import { Activity } from '../models/activity';

const sleep = (delay: number) => {
 return new Promise ((resolve) => {
   setTimeout(resolve, delay);
   });
}

axios.defaults.baseURL = 'http://localhost:5000/api';

// axios interceptors, loading delay screen and error handling
axios.interceptors.response.use(async response => {
 try { await sleep(1000);
   return response;
 }catch(error) {
   console.log(error);
   return await Promise.reject(error);
 }
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data; // data object from APP.tsx

const requests = {
 get: <T> (url: string) => axios.get<T>(url).then(responseBody),
 post: <T>(url: string, body:{}) => axios.post<T>(url, body).then(responseBody),
 put: <T>(url: string, body:{}) => axios.put<T>(url, body).then(responseBody),
 del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
}

const Activities ={
 list:() => requests.get<Activity[]>('/activities'),
}

const agent = {
 Activities
}

export default agent;