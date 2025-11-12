import {WritableSignal} from "@angular/core";

export function Notify(signalName: string) {
    return function(target: (...args: any[]) => Promise<any>,
                     context: ClassMethodDecoratorContext) {
        return async function (this: any, ...args: any[]): Promise<any> {
            const indicator: WritableSignal<boolean> | null = this[signalName];
            let result: Promise<any>;

            indicator?.set(true);

            try {
                result = await target.apply(this, args);
            }
            finally {
                indicator?.set(false);
            }

            return result;
        }
    }
}
