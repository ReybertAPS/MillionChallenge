import { Box, Button, TextField, Typography } from '@mui/material';

interface Props {
  minPrice?: number;
  maxPrice?: number;
  setMinPrice: (value: number | undefined) => void;
  setMaxPrice: (value: number | undefined) => void;
  onApply: () => void;
}

export const PriceRangePopover = ({
  minPrice,
  maxPrice,
  setMinPrice,
  setMaxPrice,
  onApply,
}: Props) => {
  return (
    <Box p={2} display="flex" flexDirection="column" gap={1} width={300}>
      <Typography variant="body2" fontWeight="bold">Rango de precio</Typography>
      <Box display="flex" gap={1}>
        <TextField
          label="Min $"
          type="number"
          fullWidth
          size="small"
          value={minPrice ?? ''}
          onChange={(e) => setMinPrice(Number(e.target.value) || undefined)}
        />
        <TextField
          label="Max $"
          type="number"
          fullWidth
          size="small"
          value={maxPrice ?? ''}
          onChange={(e) => setMaxPrice(Number(e.target.value) || undefined)}
        />
      </Box>
      <Button variant="contained" onClick={onApply}>
        Aplicar rango
      </Button>
    </Box>
  );
};
