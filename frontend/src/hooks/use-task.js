import { useSelector } from 'react-redux';

export function useTask() {
  const task = useSelector((state) => state.task);

  return task;
}
