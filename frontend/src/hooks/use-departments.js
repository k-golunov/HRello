import { useSelector } from 'react-redux';

export function useDepartments() {
  const departments = useSelector((state) => state.departments);

  return departments;
}
