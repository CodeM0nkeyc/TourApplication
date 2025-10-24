import {AfterViewInit, Directive, ElementRef, inject, input, OnInit} from '@angular/core';
import {BindThis} from "shared/utilities";
import {validationRegexes} from "../common/validation-regexes";
import type {ValidationType} from "../common/types";

@Directive({
    selector: 'app-data-input[fieldCoercion], app-list-input[fieldCoercion]',
    standalone: true,
})
export class CoercionDirective implements OnInit, AfterViewInit {
    private readonly _elementRef = inject(ElementRef);

    private _validateChange?: (event: InputEvent) => boolean;
    private _coerceValue?: (value: string) => string;

    public readonly fieldCoercion = input.required<ValidationType>();

    public ngOnInit(): void {
        let validationType = this.fieldCoercion();

        if (validationType === "phoneNumber") {
            validationType = "positiveNumbersWithSpaces";

            this._coerceValue = value => {
                value = value.replaceAll(" ", "").slice(0, 10);
                const parts = [];

                if (value.length > 0) {
                    parts.push(value.slice(0, 3));
                }
                if (value.length > 3) {
                    parts.push(value.slice(3, 6));
                }
                if (value.length > 6) {
                    parts.push(value.slice(6, 8));
                }
                if (value.length > 8) {
                    parts.push(value.slice(8, 10));
                }

                return parts.join(' ');
            }

            this._validateChange = (event: InputEvent): boolean => {
                if (!event.data) {
                    return true;
                }

                const valueOverflow = (event.target as HTMLInputElement).value.length > 12;
                return !valueOverflow && validationRegexes[validationType].test(event.data);
            }
        }
        else {
            this._validateChange = (event: InputEvent): boolean => {
                return event.data === null || validationRegexes[validationType].test(event.data);
            }
        }
    }

    public ngAfterViewInit(): void {
        const inputElem = (this._elementRef.nativeElement as HTMLElement)
            .querySelector("input") as HTMLInputElement;

        inputElem?.addEventListener(
            "beforeinput", this.onInputValidate
        )

        if (this._coerceValue) {
            inputElem?.addEventListener(
                "input", this.onInputCoerce
            );
        }
    }

    @BindThis
    private onInputValidate(event: InputEvent): void {
        if(!this._validateChange?.(event)) {
            event.preventDefault();
        }
    }

    @BindThis
    private onInputCoerce(event: Event): void {
        const target = event.target as HTMLInputElement;
        let cursorPosition = target.selectionStart;
        let previousValueLength = target.value.length;

        target.value = this._coerceValue!(target.value);

        if (cursorPosition && previousValueLength < target.value.length) {
            cursorPosition += target.value.length - previousValueLength;
        }

        target.setSelectionRange(cursorPosition, cursorPosition);
    }
}
