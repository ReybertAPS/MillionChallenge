export interface PropertyImage {
  file: string;
  isMain: boolean;
}

export interface Property {
  id: string;
  idOwner: string;
  name: string;
  address: string;
  price: number;
  codeInternal: string;
  year: number;
  imageUrl?: string | null;
  images?: PropertyImage[];
}
