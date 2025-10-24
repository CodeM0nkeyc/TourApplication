export function BindThis(target: (...args: any[]) => any,
                  context: ClassMethodDecoratorContext) {
    const methodName = context.name;

    context.addInitializer(function(this: any) {
        this[methodName] = this[methodName].bind(this);
    });
}
