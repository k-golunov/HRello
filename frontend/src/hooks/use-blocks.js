import { useSelector } from 'react-redux';

export function useBlocks() {
  const blocks = useSelector((state) => state.blocks);

  return blocks;
}
