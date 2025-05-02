import {
  Box,
  Card,
  CardContent,
  CardMedia,
  Typography,
  Pagination,
  CircularProgress,
  Grid,
  CssBaseline,
  Toolbar,
  Chip,
} from '@mui/material';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  useGetAllPropertiesQuery,
  useGetFilteredPropertiesQuery,
} from '../services/propertiesApi';
import { PropertyFiltersBar, PropertyFilters } from '../components/PropertyFiltersBar';

export const PropertiesListPage = () => {
  const navigate = useNavigate();
  const [pageIndex, setPageIndex] = useState(1);
  const pageSize = 6;
  const [filters, setFilters] = useState<PropertyFilters | null>(null);

  const handleApplyFilters = (newFilters: PropertyFilters) => {
    setFilters(newFilters);
    setPageIndex(1);
  };

  const handleClearFilters = () => {
    setFilters(null);
    setPageIndex(1);
  };

  const handleRemoveFilter = (key: 'name' | 'address' | 'price') => {
    if (!filters) return;
    const updated = { ...filters };
    if (key === 'price') {
      delete updated.minPrice;
      delete updated.maxPrice;
    } else {
      delete updated[key];
    }
    setFilters(updated);
    handleApplyFilters(updated);
  };

  const handlePageChange = (_: React.ChangeEvent<unknown>, value: number) => {
    setPageIndex(value);
  };

  const { data: allData, isLoading: loadingAll } = useGetAllPropertiesQuery(
    { pageIndex, pageSize },
    { skip: !!filters }
  );

  const { data: filteredData, isLoading: loadingFiltered } = useGetFilteredPropertiesQuery(
    {
      ...filters,
      pageIndex,
      pageSize,
    },
    { skip: !filters }
  );

  const data = filters ? filteredData : allData;
  const isLoading = filters ? loadingFiltered : loadingAll;

  const totalCount = data?.totalRecords ?? 0;
  const start = (pageIndex - 1) * pageSize + 1;
  const end = Math.min(pageIndex * pageSize, totalCount);

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', px: 2, pt: 1, pb: 3 }}>
      <CssBaseline />
      <Toolbar sx={{ mb: 1 }} />

      <PropertyFiltersBar onApply={handleApplyFilters} onClear={handleClearFilters} />

      {/* Chips de filtros activos */}
      {filters && (
        <Box display="flex" gap={1} flexWrap="wrap" mb={2}>
          {filters.name && (
            <Chip
              label={`Nombre: ${filters.name}`}
              onDelete={() => handleRemoveFilter('name')}
            />
          )}
          {filters.address && (
            <Chip
              label={`Dirección: ${filters.address}`}
              onDelete={() => handleRemoveFilter('address')}
            />
          )}
          {(filters.minPrice || filters.maxPrice) && (
            <Chip
              label={`Precio: ${filters.minPrice ?? '∞'} - ${filters.maxPrice ?? '∞'}`}
              onDelete={() => handleRemoveFilter('price')}
            />
          )}
        </Box>
      )}

      {/* Indicador de resultados */}
      {totalCount > 0 && (
        <Typography variant="body2" color="text.secondary" mb={2}>
          Mostrando <strong>{start}</strong> - <strong>{end}</strong> de <strong>{totalCount}</strong> resultados
        </Typography>
      )}

      {isLoading ? (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      ) : (
        <>
          <Grid container spacing={2}>
            {data?.data.map((property) => (
              <Grid size={{ xs: 12, sm: 6, md: 4 }} key={property.id}>
                <Card
                  onClick={() => navigate(`/property/${property.id}`)}
                  sx={{ cursor: 'pointer' }}
                >
                  {property.imageUrl && (
                    <CardMedia
                      component="img"
                      height="160"
                      image={property.imageUrl}
                      alt={property.name}
                    />
                  )}
                  <CardContent>
                    <Typography variant="h6">{property.name}</Typography>
                    <Typography variant="body2">{property.address}</Typography>
                    <Typography variant="subtitle2" color="text.secondary">
                      ${property.price.toLocaleString()}
                    </Typography>
                  </CardContent>
                </Card>
              </Grid>
            ))}
          </Grid>

          <Box display="flex" justifyContent="center" mt={4}>
            <Pagination
              count={data?.totalPages ?? 0}
              page={pageIndex}
              onChange={handlePageChange}
              color="primary"
            />
          </Box>
        </>
      )}
    </Box>
  );
};
