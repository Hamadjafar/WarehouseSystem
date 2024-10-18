import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConsts } from "../../../shared/AppConsts";
import { AuthService } from "../auth-service/auth-service";
import { LogsDto } from "./LogsDto";
import { LogsOutputDto } from "./LogsOutputDto";

@Injectable({
    providedIn: 'root'
})


export class LogsService {
    private headers: {
        headers: HttpHeaders;
    };

    constructor(private _http: HttpClient, private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }

    private apiUrl = AppConsts.baseUrl + 'Logs/GetLogs';


    getAllLogs(currentPage: number, pageSize: number) {
        const params = new HttpParams()
            .set('pageNumber', currentPage)
            .set('pageSize', pageSize);
            
        return this._http.get<LogsOutputDto>(this.apiUrl, { headers: this.headers.headers, params });
    }

}