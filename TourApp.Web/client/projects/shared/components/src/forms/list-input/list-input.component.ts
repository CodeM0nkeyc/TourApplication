import {
    ChangeDetectionStrategy, Component, effect, ElementRef,
    forwardRef, input, signal, viewChild
} from '@angular/core';
import {ControlValueAccessor, NG_VALUE_ACCESSOR, ReactiveFormsModule} from "@angular/forms";
import {CommonModule, KeyValue} from "@angular/common";
import TrieSearch from "trie-search";
import {BaseDataInput} from "../common/base-data-input";
import {v4 as uuid4} from "uuid";
import type {ListInputValue} from "./list-input.model";

@Component({
    selector: 'app-list-input',
    standalone: true,
    imports: [
        ReactiveFormsModule,
        CommonModule
    ],
    templateUrl: './list-input.component.html',
    styleUrls: [
      './list-input.component.scss',
      '../data-input/data-input.component.scss'
    ],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => ListInputComponent),
            multi: true
        }
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListInputComponent extends BaseDataInput
    implements ControlValueAccessor {

    private readonly _searchTree = new TrieSearch<KeyValue<string, {}>>(
        "key", { ignoreCase: true }
    );

    private readonly _dataInputRef =
        viewChild.required<ElementRef<HTMLInputElement>>("dataInput");
    private readonly _dataListRef =
        viewChild.required<ElementRef<HTMLUListElement>>("dataList");

    private change: (value: Map<string, {}> | ListInputValue) => void = () => {};
    private touch: () => void = () => {};

    public readonly initOptions = input.required<Map<string, {}>>();
    public readonly currentOptions = signal<Map<string, {}>>(new Map());
    public readonly selectedValues = signal<Map<string, {}>>(new Map());

    public readonly multipleSelection = input<boolean>(false);
    public readonly textForEmptySearchResult = input<string>("");

    constructor() {
        super(uuid4());

        effect(async () => {
            this.resetCurrentOptions();

            const optionsArr = Array.from(this.initOptions().entries());
            const searchTreeFeed = optionsArr.map<KeyValue<string, {}>>(
                    entry => {
                        return { key: entry[0], value: entry[1] }
                    }
                );

            this.selectedValues.update(oldMap => new Map<string, {}>(
                optionsArr.filter(option => oldMap.has(option[0]))));

            this._searchTree.reset();
            this._searchTree.addAll(searchTreeFeed);
        }, { allowSignalWrites: true });
    }

    private searchUpdate(value: string): void {
        if (value === "") {
            this.resetCurrentOptions();
        }
        else {
            const searchResult = this._searchTree.search(value);
            this.currentOptions.set(new Map(searchResult.map(
                item => { return [ item.key, item.value ]}
            )));
        }
    }

    private updateSelectedValues(value: ListInputValue[],
                                 preserveOldValue: boolean = false): void {
        if (preserveOldValue) {
            this.selectedValues.update(oldMap => new Map(
                    value.length === 0 ? oldMap : [...oldMap.entries(), ...value]));
        }
        else {
            this.selectedValues.set(new Map(value));
        }

        this.change(this.multipleSelection() ? this.selectedValues() : (value[0] ?? ["", null]));
    }

    private unselectItem(key: string) {
        this.selectedValues().delete(key);
        this.updateSelectedValues([], true);
    }

    private resetCurrentOptions(): void {
        this.currentOptions.set(this.initOptions());
    }

    public writeValue(value: Map<string, {}> | ListInputValue): void {
        const isMap = value instanceof Map;
        const selectedValue = isMap ? value : new Map<string, {}>([value])
        this.selectedValues.set(selectedValue);

        if (!this.multipleSelection()) {
            this._dataInputRef().nativeElement.value = isMap ? Object.values(value)[0] : value[0];
        }
    }

    public registerOnChange(fn: any): void {
        this.change = fn;
    }

    public registerOnTouched(fn: any): void {
        this.touch = fn;
    }

    public setDisabledState?(isDisabled: boolean): void {

    }

    public onInputFocus(event: Event): void {
        this._dataListRef().nativeElement.classList.add("data-input-list--visible");
    }

    public onInputBlur(event: Event): void {
        this._dataListRef().nativeElement.classList.remove("data-input-list--visible");
        this.touch();
    }

    public onInputSearch(event: Event): void {
        const key = (event.target as HTMLInputElement).value;

        this.searchUpdate(key);

        if (!this.multipleSelection()) {
            const inputOption = this.currentOptions().get(key);

            if (inputOption) {
                this.updateSelectedValues([[key, inputOption]]);
            }
            else if (this.selectedValues().size !== 0) {
                this.updateSelectedValues([]);
            }
        }
    }

    public onItemSelect(event: Event): void {
        this._dataInputRef().nativeElement.focus();

        const target = event.target as HTMLLIElement;
        const key = target.dataset["key"];
        const value = target.dataset["value"];

        if (key === undefined) {
            return;
        }

        let isRemoved = false;

        if (!this.selectedValues().has(key)) {
            this.updateSelectedValues([[key, value!]], this.multipleSelection());
        }
        else {
            this.unselectItem(key);
            isRemoved = true;
        }

        if (!this.multipleSelection()) {
            const newInputValue = isRemoved ? "" : key;

            this._dataInputRef().nativeElement.value = newInputValue;
            this.searchUpdate(newInputValue);
        }
    }

    public onItemCancel(event: Event): void {
        const target = event.target as HTMLLIElement;
        const key = target.dataset["key"];

        if (key !== undefined) {
            this.unselectItem(key);
        }
    }

    public isOptionSelected(key: string): boolean {
        return this.selectedValues().has(key);
    }
}
