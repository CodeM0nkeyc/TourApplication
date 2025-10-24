import type {TourDifficulty} from "shared/models";

export type TourCardFrontVM = {
    imageSrc: string,
    heading: string,
    durationInDays: number,
    remainingPlaces: number,
    startDate: Date,
    stopCount: number,
    difficulty: TourDifficulty
}
