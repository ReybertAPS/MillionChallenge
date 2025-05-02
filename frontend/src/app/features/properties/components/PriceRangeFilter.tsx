import {
  Button,
  Popover,
} from '@mui/material';
import { ArrowDropDown } from '@mui/icons-material';
import { useState } from 'react';
import { PriceRangePopover } from './PriceRangePopover';

interface Props {
  min?: number;
  max?: number;
  onApply: (min?: number, max?: number) => void;
}

export const PriceRangeFilter = ({ min, max, onApply }: Props) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [minPrice, setMinPrice] = useState<number | undefined>(min);
  const [maxPrice, setMaxPrice] = useState<number | undefined>(max);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => setAnchorEl(null);
  const open = Boolean(anchorEl);

  const handleApply = () => {
    onApply(minPrice, maxPrice);
    handleClose();
  };

  const getLabel = () => {
    const format = (val: number) => `$${(val / 1000000).toFixed(0)}M`;
    if (min && max) return `Precio: ${format(min)} - ${format(max)}`;
    if (min) return `Desde ${format(min)}`;
    if (max) return `Hasta ${format(max)}`;
    return 'Precio';
  };

  return (
    <>
      <Button
        onClick={handleClick}
        variant="outlined"
        endIcon={<ArrowDropDown />}
        sx={{
          borderColor: '#bdbdbd',
          color: '#424242',
          '&:hover': {
            borderColor: '#9e9e9e',
            backgroundColor: 'transparent',
          },
        }}
      >
        {getLabel()}
      </Button>

      <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
      >
        <PriceRangePopover
          minPrice={minPrice}
          maxPrice={maxPrice}
          setMinPrice={setMinPrice}
          setMaxPrice={setMaxPrice}
          onApply={handleApply}
        />
      </Popover>
    </>
  );
};
