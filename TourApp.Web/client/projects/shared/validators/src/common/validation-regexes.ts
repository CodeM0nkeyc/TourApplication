import type {ValidationType} from "./types";

export const validationRegexes: Record<ValidationType, RegExp> = {
    positiveNumbers: /^\d+$/,
    positiveNumbersWithSpaces: /^[\d ]+$/,
    numbers: /^[+-]?\d+$/,
    numbersWithSpaces: /^(?=.*[+-]?\d)[\d ]+$/,
    letters: /^[A-Za-z]+$/,
    lettersWithSpaces: /^[A-Za-z\s]+$/,
    phoneNumber: /\d{3}\s?\d{3}\s?\d{2}\s?\d{2}]/,
    phoneNumberWithCoercion: /^(?:\D*\d){10,}\D*$/,
    password: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_])[A-Za-z\d@$!%*?&_]{8,}$/
}
