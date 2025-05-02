import {
  Box,
  Button,
  Divider,
  Grid,
  ImageList,
  ImageListItem,
  Paper,
  Toolbar,
  Typography,
} from '@mui/material';
import ContactMailIcon from '@mui/icons-material/ContactMail';
import { useNavigate, useParams } from 'react-router-dom';
import { useState } from 'react';
import { useGetPropertyByIdQuery } from '../services/propertiesApi';
import { ContactModal } from '../components/ContactModal';
import { ImagePreviewModal } from '../components/ImagePreviewModal';

export const PropertyDetailPage = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { data: property, isLoading } = useGetPropertyByIdQuery(id!);
  const [open, setOpen] = useState(false);
  const [selectedImage, setSelectedImage] = useState<string | null>(null);

  if (isLoading || !property) return <Typography sx={{ p: 3 }}>Cargando...</Typography>;

  return (
    <Box sx={{ px: 2, py: 4 }}>
      <Toolbar />
      <Button onClick={() => navigate('/')} variant="outlined" sx={{ mb: 2 }}>
        ← Volver al listado
      </Button>

      <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
        <Typography variant="h4" fontWeight="bold" gutterBottom>
          {property.name}
        </Typography>
        <Typography variant="subtitle1" color="text.secondary" gutterBottom>
          {property.address}
        </Typography>

        <Divider sx={{ my: 3 }} />

        <Grid container spacing={2} mb={3}>
          <Grid size={{ xs: 12, sm: 6, md: 3 }}>
            <Typography variant="body2" color="text.secondary">Código interno</Typography>
            <Typography variant="body1">{property.codeInternal}</Typography>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, md: 3 }}>
            <Typography variant="body2" color="text.secondary">Año de construcción</Typography>
            <Typography variant="body1">{property.year}</Typography>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, md: 3 }}>
            <Typography variant="body2" color="text.secondary">Precio</Typography>
            <Typography variant="body1" fontWeight="bold">
              ${property.price.toLocaleString()}
            </Typography>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, md: 3 }}>
            <Typography variant="body2" color="text.secondary">Propietario</Typography>
            <Typography variant="body1">{property.idOwner}</Typography>
          </Grid>
        </Grid>

        <Typography variant="h6" gutterBottom>Imágenes</Typography>
        {property.images && property.images.length > 0 && (
          <ImageList cols={2} gap={12}>
            {property.images.map((img, index) => (
              <ImageListItem key={index} onClick={() => setSelectedImage(img.file)}>
                <img
                  src={img.file}
                  alt={`Imagen ${index + 1}`}
                  loading="lazy"
                  style={{ borderRadius: 8 }}
                />
              </ImageListItem>
            ))}
          </ImageList>
        )}
      </Paper>

      <Box
        sx={{
          position: 'fixed',
          bottom: 24,
          right: 24,
          zIndex: 1000,
        }}
      >
        <Button
          variant="contained"
          color="primary"
          size="large"
          startIcon={<ContactMailIcon />}
          onClick={() => setOpen(true)}
        >
          Contactar propietario
        </Button>
      </Box>

      <ContactModal
        open={open}
        onClose={() => setOpen(false)}
        propertyName={property.name}
        ownerId={property.idOwner}
      />

      <ImagePreviewModal
        open={!!selectedImage}
        imageUrl={selectedImage ?? ''}
        onClose={() => setSelectedImage(null)}
      />
    </Box>
  );
};
