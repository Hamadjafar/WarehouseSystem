import { Injectable } from "@angular/core";
import { AppConsts } from "../../../shared/AppConsts";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthService } from "../auth-service/auth-service";
import { RoleDto } from "../users-service/roleDto";

@Injectable({
    providedIn: 'root'
})

export class RoleService {
    private apiUrl = AppConsts.baseUrl + 'Role/GetRoles';
    private headers: {
        headers:HttpHeaders;
    };

    constructor(private _http: HttpClient,private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }


    getRoles() {
        return this._http.get<RoleDto[]>(this.apiUrl, this.headers);
    }
}