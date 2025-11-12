import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {AppValidators, fromApiErrors} from "shared/validators";
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {UsersService} from "shared/services";
import {enterTrigger} from "../../auth.animations";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: 'auth-confirmation',
    standalone: true,
    imports: [
        ButtonComponent,
        DataInputComponent,
        ReactiveFormsModule,
        ValidationMessageComponent
    ],
    templateUrl: './confirmation.component.html',
    styleUrl: './confirmation.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    animations: [
        enterTrigger
    ]
})
export class ConfirmationComponent extends StepComponentBase {
    private readonly _formBuilder = inject(FormBuilder);
    private readonly _usersService = inject(UsersService);
    private readonly _route = inject(ActivatedRoute);

    public override readonly form = this._formBuilder.group({
        code: this._formBuilder.nonNullable.control("", [
            AppValidators.length(7, "Code")]),
    });

    public constructor() {
        super("signup/completed");
    }

    protected override saveData(): void {

    }

    protected override async submit(): Promise<boolean> {
        const confirmationCode = {
            email: this._route.snapshot.queryParams["email"] ?? "",
            code: this.form.value.code!
        }

        const confirmResult = await this._usersService.confirmEmailAsync(confirmationCode);

        if (confirmResult.isSuccess) {
            sessionStorage.setItem("registered", "true");
            return super.submit();
        }

        const errorObject = fromApiErrors(confirmResult.errors);

        this.form.controls.code.setErrors(errorObject);

        return false;
    }
}
