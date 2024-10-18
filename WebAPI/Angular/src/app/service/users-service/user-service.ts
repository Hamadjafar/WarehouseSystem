import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthService } from "../auth-service/auth-service";
import { AppConsts } from "../../../shared/AppConsts";
import { Observable } from "rxjs";
import { UsersOutputDto } from "./UsersOutputDto";
import { ChangePasswordDto } from "./changePasswordDto";
import { UserDto } from "./userDto";

@Injectable({
    providedIn: 'root'
})

export class UserService {

    private apiUrl = AppConsts.baseUrl + 'User/';


    private headers: {
        headers: HttpHeaders;
    };

    constructor(private _http: HttpClient, private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }



    createOrUpdateUser(userInputDto: UserDto): Observable<any> {
        return this._http.post<any>(this.apiUrl + 'CreateOrUpdateUser', userInputDto, this.headers);

    }

    deactivateUser(userId: number) {
        return this._http.patch<any>(this.apiUrl + 'DeactivateUser', userId, this.headers);
    }

    changePassword(changePasswordDto: ChangePasswordDto) {
        return this._http.patch<any>(this.apiUrl + 'ChangePassword', changePasswordDto, this.headers);
    }


    getAllUsers(currentPage: number, pageSize: number): Observable<UsersOutputDto> {
        const params = new HttpParams()
            .set('pageNumber', currentPage)
            .set('pageSize', pageSize);

        return this._http.get<UsersOutputDto>(this.apiUrl + 'GetAllUsers', { headers: this.headers.headers, params });
    }

    getUserById(userId: number) {
        return this._http.get<UserDto>(`${this.apiUrl}GetUserById/${userId}`, {
            headers: this.headers.headers
        });
    }

}