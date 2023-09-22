import { Member } from './member';
import { Status } from './status';

export interface Transaction {
    id: number;
    appUserId?: number;
    sentAmount: number;
    profit: number;
    totalAmount: number;
    senderName: string;
    senderCityId: number,
    recipientName: string;
    recipientCityId: number,
    code: string;
    statusId: number,
    status: Status;
    appUser: Member;
}