import {TourDifficulty} from "./common";

export type TourCardVM = {
    imageSrc: string,
    heading: string,
    description: string,
    difficulty: TourDifficulty,
    durationInDays: number,
    country: string,
    startDate: Date,
    remainingPlaces: number,
    price: number,
    stopCount: number,
    rating?: number,
}
