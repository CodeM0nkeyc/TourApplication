import {ChangeDetectionStrategy, Component, DestroyRef, inject, input, OnInit, signal} from '@angular/core';
import {AbstractControl} from "@angular/forms";

@Component({
    selector: 'app-validation-message',
    standalone: true,
    imports: [],
    templateUrl: './validation-message.component.html',
    styleUrl: './validation-message.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ValidationMessageComponent implements OnInit {
    private readonly _destroyRef = inject(DestroyRef);

    public readonly control = input.required<AbstractControl>();

    public readonly validationMessages = signal<string[]>([]);

    public ngOnInit(): void {
        const subscription = this.control().valueChanges.subscribe(value => {
            this.validationMessages.set(Object.values(this.control().errors ?? {}));
        });

        this._destroyRef.onDestroy(() => {
            subscription.unsubscribe();
        });
    }
}
