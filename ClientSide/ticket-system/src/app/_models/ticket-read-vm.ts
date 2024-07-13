export interface TicketReadVm {
        id: number;
        creationDateTime: string;
        phoneNumber: string;
        governorate: string;
        city: string;
        district: string;
        status: string;
        isHandled : boolean;
}
