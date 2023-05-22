import { useSelector } from 'react-redux';

export function useTasks() {
  const tasks = useSelector((state) => state.tasks);

  return tasks;
}
