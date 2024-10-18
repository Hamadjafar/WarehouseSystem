import { Injectable } from "@angular/core";
import { AppConsts } from "../../../shared/AppConsts";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { CountryDto } from "./countryDto";
import { AuthService } from "../auth-service/auth-service";

@Injectable({
    providedIn: 'root'
})

export class CountryService {
    private apiUrl = AppConsts.baseUrl + 'Country/GetCountries';
    private headers: {
        headers:HttpHeaders;
    };

    constructor(private _http: HttpClient,private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }


    getCountries() {
        return this._http.get<CountryDto[]>(this.apiUrl, this.headers);
    }

}