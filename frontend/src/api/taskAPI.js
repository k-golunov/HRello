import { HOST } from './host';

export const GET_TASK_URL = `${HOST}/task/`;
export const GET_ALL_TASKS_URL = `${HOST}/task/all/`;
export const CREATE_TASK_URL = `${HOST}/task`;
export const UPDATE_TASK_URL = `${HOST}/task`;

const TASK_API = {
  GET_TASK_URL,
  CREATE_TASK_URL,
  GET_ALL_TASKS_URL,
  UPDATE_TASK_URL

};

export default TASK_API;
