import {Failure, Result} from "../../entity-services/base/result";

export type ApiFailure = Failure;

export type ApiResult<TResult = never> = Result<TResult>;
