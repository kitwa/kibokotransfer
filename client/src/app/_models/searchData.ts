import {Photo} from './photo';

export interface SearchData {

    minPrice: number;
    maxPrice: number;
    bathRooms: number;
    bedRooms: number;
    propertyType?: string;
    city: string;

}