import {
  Box,
  Button,
  Modal,
  TextField,
  Typography,
} from '@mui/material';
import { useState } from 'react';

interface Props {
  open: boolean;
  onClose: () => void;
  propertyName: string;
  ownerId: string;
}

export const ContactModal = ({ open, onClose, propertyName, ownerId }: Props) => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  const [errors, setErrors] = useState<{ name?: string; email?: string }>({});

  const handleSend = () => {
    const newErrors: typeof errors = {};
    if (!name.trim()) newErrors.name = 'Este campo es obligatorio';
    if (!email.trim()) newErrors.email = 'Este campo es obligatorio';

    if (Object.keys(newErrors).length > 0) {
      setErrors(newErrors);
      return;
    }

    alert(`Mensaje enviado al propietario ${ownerId}:\n\nNombre: ${name}\nEmail: ${email}\nMensaje: ${message}`);
    onClose();
    setName('');
    setEmail('');
    setMessage('');
    setErrors({});
  };

  return (
    <Modal open={open} onClose={onClose}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 400,
          bgcolor: 'background.paper',
          borderRadius: 2,
          boxShadow: 24,
          p: 4,
          display: 'flex',
          flexDirection: 'column',
          gap: 2,
        }}
      >
        <Typography variant="h6" fontWeight="bold">Contactar propietario</Typography>

        <TextField
          label="Tu nombre"
          fullWidth
          size="small"
          value={name}
          onChange={(e) => setName(e.target.value)}
          error={!!errors.name}
          helperText={errors.name}
        />

        <TextField
          label="Tu email"
          fullWidth
          size="small"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          error={!!errors.email}
          helperText={errors.email}
        />

        <TextField
          label="Mensaje"
          fullWidth
          size="small"
          multiline
          minRows={3}
          placeholder={`Estoy interesado en la propiedad ${propertyName}...`}
          value={message}
          onChange={(e) => setMessage(e.target.value)}
        />

        <Button variant="contained" onClick={handleSend}>
          Enviar mensaje
        </Button>
      </Box>
    </Modal>
  );
};
