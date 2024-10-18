import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AppConsts } from "../../../shared/AppConsts";
import { AuthModel } from "../auth-service/auth-model";

@Injectable({
    providedIn: 'root'
})

export class LoginService {
    constructor(private http: HttpClient) { }

    private apiUrl = AppConsts.baseUrl + 'Auth/Login';

    login(email: string, password: string): Observable<AuthModel> {
        const requestBody = {
            email: email,
            password: password
        }
        return this.http.post<AuthModel>(this.apiUrl, requestBody);
    }
}