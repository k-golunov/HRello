import { useSelector } from 'react-redux';

export function useTasks() {
    const tasks = useSelector((state) => state.tasks);

    // const statusPriority = {
    //     "OnChecking" : 1,
    //     "CompletionCheck": 2,
    //     "AwaitingCancellation": 3,
    //     "InWork": 4,
    //     "OnRework": 5,
    //     "Completed": 6,
    //     "Canceled": 7,
    // }
    //
    // const tempTasks = tasks.tasks.slice()
    //
    // const t = tempTasks.sort((t1, t2) => {
    //     return statusPriority[t1.status] - statusPriority[t2.status]
    // })
    //
    // console.log("SORT TASKS", t)
    // return {...tasks, tasks: t};
    return tasks
}
