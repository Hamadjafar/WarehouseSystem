import { Injectable } from "@angular/core";
import { AppConsts } from "../../../shared/AppConsts";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { AuthService } from "../auth-service/auth-service";
import { WareHouseStatusDto } from "./WareHouseStatusDto";
import { TopHighAndLowItemsDto } from "./TopHighAndLowItemsDto";

@Injectable({
    providedIn: 'root'
})


export class DashboadService{
    private wareHouseStatusUrl = AppConsts.baseUrl + 'Dashboard/GetWareHouseStatus';
    private topHighAndLowItemsUrl = AppConsts.baseUrl + 'Dashboard/GetTopHighAndLowItemsByQuantity';

    private headers: {
        headers:HttpHeaders;
    };

    constructor(private _http: HttpClient,private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }


    getWareHouseStatus() {
        return this._http.get<WareHouseStatusDto[]>(this.wareHouseStatusUrl, this.headers);
    }        

    getTopHighAndLowItemsByQuantity(){
        return this._http.get<TopHighAndLowItemsDto>(this.topHighAndLowItemsUrl, this.headers);
    }
}