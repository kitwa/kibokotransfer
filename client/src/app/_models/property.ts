import { Member } from './member';
import {Photo} from './photo';

export interface Property {
    id: number;
    appUserId?: number;
    price: number;
    bathRooms: number;
    bedRooms: number;
    garage: number;
    propertyType: string;
    description: string;
    city: string;
    country: string;
    youtubeLink?: string;
    photos: Photo[];
    appUser: Member;
    created: Date;
}