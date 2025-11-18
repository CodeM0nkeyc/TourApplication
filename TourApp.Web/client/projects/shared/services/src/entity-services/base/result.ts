export type Failure = {
    code: string,
    description: string
}

export type Result<TResult = never> = {
    isSuccess: boolean;
    data: TResult;
    errors: Failure[] | null;
}
