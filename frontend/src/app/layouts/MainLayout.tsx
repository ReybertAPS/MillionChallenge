import { AppBar, Toolbar, Typography, Container } from '@mui/material';
import { Outlet, Link as RouterLink } from 'react-router-dom';

export const MainLayout = () => {
  return (
    <>
      <AppBar position="static">
        <Toolbar>
          <Typography
            variant="h6"
            component={RouterLink}
            to="/"
            sx={{
              textDecoration: 'none',
              color: 'inherit',
              fontWeight: 500,
            }}
          >
            Million Properties
          </Typography>
        </Toolbar>
      </AppBar>

      <Container maxWidth="lg">
        <Outlet />
      </Container>
    </>
  );
};
