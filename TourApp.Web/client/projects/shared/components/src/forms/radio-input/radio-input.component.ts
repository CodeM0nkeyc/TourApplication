import {ChangeDetectionStrategy, Component, ElementRef, forwardRef, input, viewChild} from '@angular/core';
import {v4 as uuid4} from "uuid"
import {ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule} from "@angular/forms";

@Component({
    selector: 'app-radio-input',
    standalone: true,
    imports: [
        ReactiveFormsModule
    ],
    templateUrl: './radio-input.component.html',
    styleUrl: './radio-input.component.scss',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => RadioInputComponent),
            multi: true
        }
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class RadioInputComponent implements ControlValueAccessor {
    private readonly radioInputRef = viewChild.required<ElementRef<HTMLInputElement>>("radioInput");

    public readonly radioGroupName = input.required<string>();
    public readonly value = input<string>("");

    public readonly inputId: string = uuid4();

    private change: (value: string) => void = () => {};
    private touch: () => void = () => {};

    constructor() {

    }

    public writeValue(value: string): void {
        this.radioInputRef().nativeElement.value = value;
    }

    public registerOnChange(fn: any): void {
        this.change = fn;
    }

    public registerOnTouched(fn: any): void {
        this.touch = fn;
    }

    public setDisabledState?(isDisabled: boolean): void {

    }

    public onRadioClick(event: Event): void {
        const value = (event.target as HTMLInputElement).value;

        this.change(value);
        this.touch();
    }
}
