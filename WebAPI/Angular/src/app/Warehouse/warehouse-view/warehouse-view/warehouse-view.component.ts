import { Component, OnInit } from '@angular/core';
import { WareHouseService } from '../../../service/warehouse-service/warehouse-service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { WareHouseItemsOutputDto } from '../../../service/warehouse-service/WareHouseItemsOutputDto';
import { LoaderComponent } from "../../../../shared/loader/loader/loader.component";

@Component({
  selector: 'warehouse-view',
  standalone: true,
  imports: [CommonModule, LoaderComponent],
  templateUrl: './warehouse-view.component.html',
  styleUrl: './warehouse-view.component.css'
})
export class WarehouseViewComponent implements OnInit {
  constructor(private _wareHouseService: WareHouseService, private route: ActivatedRoute) { }

  wareHouseitems: WareHouseItemsOutputDto = new WareHouseItemsOutputDto();
  id: number;
  currentPage: number = 1;
  totalItems: number = 0;
  pageSize: number = 10;
  isLoading: boolean = false;


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      if (this.id) {
        this.getAllWareHouses(this.id);
      }
    })

  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.getAllWareHouses(this.id); 
  }

  getAllWareHouses(id: number) {
    this.isLoading = true;
    this._wareHouseService.getItemsById(id,this.currentPage, this.pageSize).subscribe(result => {
      this.wareHouseitems = result;
      setTimeout(() => {
        this.isLoading = false;
    }, 1000)
    })
  }

 

}
