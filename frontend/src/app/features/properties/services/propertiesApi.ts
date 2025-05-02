import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { Property } from '../types/Property';

interface PagedResponse<T> {
  data: T[];
  pageIndex: number;
  pageSize: number;
  totalRecords: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export const propertiesApi = createApi({
  reducerPath: 'propertiesApi',
  baseQuery: fetchBaseQuery({
    baseUrl: import.meta.env.VITE_API_BASE_URL,
  }),
  endpoints: (builder) => ({
    getAllProperties: builder.query<PagedResponse<Property>, { pageIndex: number; pageSize: number }>({
      query: ({ pageIndex, pageSize }) =>
        `properties?pageIndex=${pageIndex}&pageSize=${pageSize}`,
    }),
    getPropertyById: builder.query<Property, string>({
      query: (id) => `properties/${id}`,
    }),
    getFilteredProperties: builder.query<PagedResponse<Property>, {
      name?: string;
      address?: string;
      minPrice?: number;
      maxPrice?: number;
      pageIndex: number;
      pageSize: number;
    }>({
      query: (params) => {
        const searchParams = new URLSearchParams();
        if (params.name) searchParams.append('name', params.name);
        if (params.address) searchParams.append('address', params.address);
        if (params.minPrice !== undefined) searchParams.append('minPrice', String(params.minPrice));
        if (params.maxPrice !== undefined) searchParams.append('maxPrice', String(params.maxPrice));
        searchParams.append('pageIndex', String(params.pageIndex));
        searchParams.append('pageSize', String(params.pageSize));
        return `properties/filter?${searchParams.toString()}`;
      },
    }),
  }),
});

export const {
  useGetAllPropertiesQuery,
  useGetPropertyByIdQuery,
  useGetFilteredPropertiesQuery,
} = propertiesApi;
