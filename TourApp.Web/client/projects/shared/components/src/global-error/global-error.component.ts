import {ChangeDetectionStrategy, Component, DestroyRef, inject, signal} from '@angular/core';
import {GlobalErrorHandler} from "shared/services";
import {skip} from "rxjs";
import {enterTrigger} from "shared/animations";

@Component({
    selector: 'app-global-error',
    standalone: true,
    templateUrl: './global-error.component.html',
    styleUrl: './global-error.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [enterTrigger]
})
export class GlobalErrorComponent {
    private readonly _destroyRef = inject(DestroyRef);

    private readonly _globalErrorService = inject(GlobalErrorHandler);

    public readonly error = signal<string | null>(null);
    public readonly opened = signal<boolean>(false);

    public constructor() {
        const subscription = this._globalErrorService.errors
            .pipe(skip(1))
            .subscribe(message => {
                if (message !== "") {
                    this.opened.set(true);
                    this.error.set(message);
                }
            });

        this._destroyRef.onDestroy(() => {
            subscription.unsubscribe();
        });
    }

    public onClose(event: Event): void {
        this.opened.set(false);
    }
}
