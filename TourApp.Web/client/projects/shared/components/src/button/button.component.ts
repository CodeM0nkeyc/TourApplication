import {ChangeDetectionStrategy, Component, ElementRef, inject} from '@angular/core';

@Component({
    selector: 'a[app-button], button[app-button]',
    standalone: true,
    imports: [],
    templateUrl: './button.component.html',
    styleUrl: './button.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ButtonComponent {
    private readonly _elementRef = inject(ElementRef);

    public constructor() {
        (this._elementRef.nativeElement as HTMLButtonElement)
            .addEventListener("click", this.onMouseLeave);
    }

    public onMouseLeave(event: MouseEvent) {
        (event.target as HTMLElement)?.blur();
    }
}
