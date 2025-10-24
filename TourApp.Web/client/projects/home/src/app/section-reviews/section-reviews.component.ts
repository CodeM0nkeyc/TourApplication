import {ChangeDetectionStrategy, Component} from '@angular/core';
import {ReviewComponent} from "./review/review.component";
import {SectionHeadingComponent, ButtonComponent} from "shared/components"

@Component({
    selector: 'home-section-reviews',
    standalone: true,
    imports: [
        ReviewComponent,
        SectionHeadingComponent,
        ButtonComponent
    ],
    templateUrl: './section-reviews.component.html',
    styleUrl: './section-reviews.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionReviewsComponent {

}
