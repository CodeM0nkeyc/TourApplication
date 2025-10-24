import {ChangeDetectionStrategy, Component, input} from '@angular/core';

@Component({
    selector: 'home-review',
    standalone: true,
    imports: [],
    templateUrl: './review.component.html',
    styleUrl: './review.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ReviewComponent {
    public readonly id = input.required();
}
