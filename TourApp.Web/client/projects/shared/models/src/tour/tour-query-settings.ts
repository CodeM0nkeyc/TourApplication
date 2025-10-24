import type {OrderByTourProperty, TourDifficulty, TourState} from "./common";


export type TourQuerySettings = {
    id?: string,
    heading?: string,
    remainingPlaces?: string,
    difficulties?: TourDifficulty[],
    tourStates?: TourState[],
    countries?: string[],
    pageIndex?: number,

    "priceSettings.lowerBound"?: string,
    "priceSettings.upperBound"?: string,
    "priceSettings.withDiscount"?: boolean,
    "orderSettings.property"?: OrderByTourProperty,
    "orderSettings.descending"?: boolean
}
