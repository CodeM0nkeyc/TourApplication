import {ChangeDetectionStrategy, Component, ElementRef, forwardRef, viewChild} from '@angular/core';
import {v4 as uuid4} from "uuid";
import {ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule} from "@angular/forms";
import {BaseDataInput} from "../common/base-data-input";

@Component({
    selector: 'app-data-input',
    standalone: true,
    templateUrl: './data-input.component.html',
    styleUrl: './data-input.component.scss',
    imports: [
        ReactiveFormsModule
    ],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DataInputComponent),
            multi: true
        }
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataInputComponent extends BaseDataInput
    implements ControlValueAccessor {

    private readonly _inputRef = viewChild.required<ElementRef<HTMLInputElement>>("dataInput");

    private change: (value: string) => void = () => {};
    private touch: () => void = () => {};

    public constructor() {
        super(uuid4());
    }

    public writeValue(value: string): void {
        this._inputRef().nativeElement.value = value;
    }

    public registerOnChange(fn: any): void {
        this.change = fn;
    }

    public registerOnTouched(fn: any): void {
        this.touch = fn;
    }

    public setDisabledState?(isDisabled: boolean): void {

    }

    public onInputData(event: Event): void {
        const value = (event.target as HTMLInputElement).value;
        this.change(value);
    }

    public onInputBlur(event: Event): void {
        this.touch();
    }
}
