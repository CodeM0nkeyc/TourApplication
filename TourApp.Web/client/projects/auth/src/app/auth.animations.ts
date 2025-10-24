import {animate, style, transition, trigger} from "@angular/animations";

export const enterTrigger = trigger("enter", [
    transition(":enter", [
        style({ opacity: 0 }),
        animate("1s ease-in", style({ opacity: 1 }))
    ])
])
