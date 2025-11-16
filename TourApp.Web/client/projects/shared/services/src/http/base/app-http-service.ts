import {HttpClient} from "@angular/common/http";
import {environment} from "shared";

export abstract class AppHttpService {
    protected readonly httpClient: HttpClient;
    protected readonly apiUrl = environment.apiUrl;

    protected constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }
}
