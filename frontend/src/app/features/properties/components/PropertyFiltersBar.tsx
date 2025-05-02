import { Box, Button, TextField } from '@mui/material';
import { useState } from 'react';
import { PriceRangeFilter } from './PriceRangeFilter';

export interface PropertyFilters {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
}

interface Props {
  onApply: (filters: PropertyFilters) => void;
  onClear: () => void;
}

export const PropertyFiltersBar = ({ onApply, onClear }: Props) => {
  const [filters, setFilters] = useState<PropertyFilters>({});

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: value || undefined,
    }));
  };

  const handlePriceChange = (min?: number, max?: number) => {
    setFilters((prev) => ({
      ...prev,
      minPrice: min,
      maxPrice: max,
    }));
  };

  const handleApply = () => {
    onApply(filters);
  };

  const handleClear = () => {
    setFilters({});
    onClear();
  };

  return (
    <Box
      display="flex"
      flexDirection={{ xs: 'column', md: 'row' }}
      alignItems={{ xs: 'stretch', md: 'center' }}
      gap={2}
      flexWrap="wrap"
      mb={2}
    >
      <TextField
        name="name"
        label="Nombre"
        value={filters.name ?? ''}
        onChange={handleChange}
      />
      <TextField
        name="address"
        label="Dirección"
        value={filters.address ?? ''}
        onChange={handleChange}
      />
      <PriceRangeFilter
        min={filters.minPrice}
        max={filters.maxPrice}
        onApply={handlePriceChange}
      />
      <Button variant="contained" onClick={handleApply}>
        Buscar
      </Button>
      <Button variant="outlined" onClick={handleClear}>
        Limpiar
      </Button>
    </Box>
  );
};
