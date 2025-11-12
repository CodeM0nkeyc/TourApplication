import {Route} from "@angular/router";
import {SigninComponent} from "./signin/signin.component";
import {FirstStepComponent} from "./signup/first-step/first-step.component";
import {SecondStepComponent} from "./signup/second-step/second-step.component";
import {ThirdStepComponent} from "./signup/third-step/third-step.component";
import {FourthStepComponent} from "./signup/fourth-step/fourth-step.component";
import {SignupCompletedComponent} from "./signup/signup-completed/signup-completed.component";
import {ConfirmationComponent} from "./signup/confirmation/confirmation.component";
import {createStepComponentGuard} from "./signup/step-component-guard";

export const routes: Route[] = [
    {
        path: "",
        children: [
            { path: "", redirectTo: "signin", pathMatch: "full" },
            { path: "signin", component: SigninComponent, title: "Sign in" },
            {
                path: "signup", title: "Sign up",
                children: [
                    { path: "", redirectTo: "step1", pathMatch: "full" },
                    { path: "step1", component: FirstStepComponent },
                    { path: "step2", component: SecondStepComponent,
                        canActivate: [createStepComponentGuard("/signup/step1", "email")] },
                    { path: "step3", component: ThirdStepComponent,
                        canActivate: [createStepComponentGuard("/signup/step2", "firstName", "lastName")] },
                    { path: "step4", component: FourthStepComponent,
                        canActivate: [createStepComponentGuard("/signup/step3", "country")] },
                    { path: "confirmation", component: ConfirmationComponent },
                    { path: "completed", component: SignupCompletedComponent,
                        canActivate: [createStepComponentGuard("/signup/step1", "registered")] }
                ]
            },
        ]
    }
];
