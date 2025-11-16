export class FriendlyError extends Error {
    public readonly innerError?: Error;

    public constructor(message: string, innerError?: Error) {
        super(message);
        this.name = "FriendlyError";
        this.innerError = innerError;
        this.cause = this.innerError?.cause;
    }
}
