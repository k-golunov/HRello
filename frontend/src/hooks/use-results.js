import { useSelector } from 'react-redux';

export function useResults() {
  const results = useSelector((state) => state.results);

  return results;
}
