import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConsts } from "../../../shared/AppConsts";
import { WareHouseDto } from "./wareHouseDto";
import { Observable } from "rxjs";
import { WareHouseItemsDto } from "./wareHouseItemsDto";
import { AuthService } from "../auth-service/auth-service";
import { WareHouseOutputDto } from "./wareHouseOutputDto";
import { WareHouseItemsOutputDto } from "./WareHouseItemsOutputDto";

@Injectable({
    providedIn: 'root'
})

export class WareHouseService {
    private apiUrl = AppConsts.baseUrl + 'WareHouse/Create';
    private apiUrlGetAll = AppConsts.baseUrl + 'WareHouse/GetAllWarehouses';
    private getItemsByIdUrl = AppConsts.baseUrl + 'WareHouse/GetItemsById';

    private headers: {
        headers:HttpHeaders;
    };

    constructor(private _http: HttpClient, private _authService: AuthService) {
        this.headers = _authService.getHeader();
    }

    create(wareHouseDto: WareHouseDto): Observable<any> {
        return this._http.post<any>(this.apiUrl, wareHouseDto, this.headers);

    }

    getAllWareHouses(currentPage: number, pageSize: number): Observable<WareHouseOutputDto> {
        const params = new HttpParams()
          .set('pageNumber', currentPage) 
          .set('pageSize', pageSize); 
    
        return this._http.get<WareHouseOutputDto>(this.apiUrlGetAll, { headers: this.headers.headers, params });
      }

      getItemsById(warehouseId: number, currentPage: number, pageSize: number): Observable<WareHouseItemsOutputDto> {
        const params = new HttpParams()
            .set('warehouseId', warehouseId)
            .set('pageNumber', currentPage)
            .set('pageSize', pageSize);
    
        return this._http.get<WareHouseItemsOutputDto>(this.getItemsByIdUrl, { headers: this.headers.headers, params });
    }
}