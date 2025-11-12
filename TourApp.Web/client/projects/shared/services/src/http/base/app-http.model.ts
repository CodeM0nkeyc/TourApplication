export type ApiError = {
    code: string,
    description: string
}

export type ApiResult<TResult = never> = {
    isSuccess: boolean;
    data: TResult;
    errors: ApiError[] | null;
}
