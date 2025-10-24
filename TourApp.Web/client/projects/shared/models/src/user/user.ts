import {Address} from "./common";

export type User = {
    id: string;
    email: string;
    phoneNumber: string;
    password: string;

    firstName: string;
    lastName: string;
    middleName: string;
    address: Address;
    image: Blob
}
