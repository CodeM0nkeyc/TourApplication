import {CanActivateFn, RedirectCommand, Router} from "@angular/router";
import {inject} from "@angular/core";

export function createStepComponentGuard(returnPath: string, ...keys: string[]): CanActivateFn {
    return function (route, state) {
        const router = inject(Router);
        let isDefined: string | boolean = true;

        for (const key of keys) {
            isDefined &&= sessionStorage.getItem(key) ?? false;
        }

        if (isDefined) {
            return true;
        }

        return new RedirectCommand(router.parseUrl(returnPath),
            {
                replaceUrl: true
            });
    }
}
