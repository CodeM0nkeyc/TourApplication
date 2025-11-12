import {Address} from "shared/models";

export type RegistrationData = {
    email: string,
    phoneNumber?: string,

    firstName: string,
    lastName: string,
    middleName?: string,

    address: Address

    password: string
}

export type Credentials = {
    email: string,
    password: string
}

export type ConfirmationCode = {
    email: string,
    code: string
}
