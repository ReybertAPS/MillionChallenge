import { createBrowserRouter } from 'react-router-dom';
import { PropertiesListPage } from '../features/properties/pages/PropertiesListPage';
import { PropertyDetailPage } from '../features/properties/pages/PropertyDetailPage';
import { MainLayout } from '../layouts/MainLayout';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <MainLayout />,
    children: [
      { path: '/', element: <PropertiesListPage /> },
      { path: 'property/:id', element: <PropertyDetailPage /> },
    ],
  },
]);
