import {Directive, input} from "@angular/core";
import {v4 as uuid4} from "uuid";
import {InputType} from "./base-data-input.model";


@Directive()
export abstract class BaseDataInput {
    public readonly name = input.required<string>();

    public readonly placeholder = input<string>("");

    public readonly type = input<InputType>("text");

    public readonly inputId: string;

    protected constructor(inputId: string = uuid4()) {
        this.inputId = inputId;
    }
}
