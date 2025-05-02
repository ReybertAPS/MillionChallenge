import { Modal, Box, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close';

interface Props {
  open: boolean;
  imageUrl: string;
  onClose: () => void;
}

export const ImagePreviewModal = ({ open, imageUrl, onClose }: Props) => {
  return (
    <Modal open={open} onClose={onClose}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          maxWidth: '90vw',
          maxHeight: '90vh',
          bgcolor: 'transparent',
          outline: 'none',
        }}
      >
        <IconButton
          onClick={onClose}
          sx={{
            position: 'absolute',
            top: 8,
            right: 8,
            zIndex: 1,
            bgcolor: 'rgba(0,0,0,0.5)',
            color: 'white',
            '&:hover': { bgcolor: 'rgba(0,0,0,0.7)' },
          }}
        >
          <CloseIcon />
        </IconButton>

        <Box
          component="img"
          src={imageUrl}
          alt="Vista ampliada"
          sx={{
            width: '100%',
            height: 'auto',
            maxHeight: '90vh',
            borderRadius: 2,
            boxShadow: 6,
          }}
        />
      </Box>
    </Modal>
  );
};
