import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {
    ButtonComponent,
    DataInputComponent,
    RadioInputComponent,
    SectionHeadingComponent
} from "shared/components";

@Component({
    selector: 'home-section-book',
    standalone: true,
    imports: [
        SectionHeadingComponent,
        DataInputComponent,
        ButtonComponent,
        FormsModule,
        ReactiveFormsModule,
        RadioInputComponent
    ],
    templateUrl: './section-book.component.html',
    styleUrl: './section-book.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionBookComponent {
    private readonly _formBuilder = inject(FormBuilder);

    public readonly form = this._formBuilder.group({
        fullName: this._formBuilder.nonNullable.control('', [Validators.required]),
        email: this._formBuilder.nonNullable.control('', [Validators.required, Validators.email]),
        groupSize: this._formBuilder.nonNullable.control('', [Validators.required]),
    });

    public onSubmit(event: Event) {
        console.log(this.form.value);
    }
}
