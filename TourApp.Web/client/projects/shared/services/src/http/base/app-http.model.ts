export type ApiFailure = {
    code: string,
    description: string
}

export type ApiResult<TResult = never> = {
    isSuccess: boolean;
    data: TResult;
    errors: ApiFailure[] | null;
}
