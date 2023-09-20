import { Property } from './property'

export interface Member {
  id: number;
  email: string;
  phone?: number;
  created: Date;
  lastActive: Date;
  gender?: string;
  city?: string;
  twitter?: string;
  youtube?: string;
  instagram?: string;
  facebook?: string;
  country?: string;
  properties: Property[];

}

