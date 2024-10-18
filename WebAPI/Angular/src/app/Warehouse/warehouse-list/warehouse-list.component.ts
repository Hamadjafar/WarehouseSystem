import { Component, OnInit } from '@angular/core';
import { WareHouseService } from '../../service/warehouse-service/warehouse-service';
import { WareHouseDto } from '../../service/warehouse-service/wareHouseDto';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { WareHouseOutputDto } from '../../service/warehouse-service/wareHouseOutputDto';
import { LoaderComponent } from '../../../shared/loader/loader/loader.component';

@Component({
  selector: 'warehouse-list',
  standalone: true,
  imports: [CommonModule,LoaderComponent],
  templateUrl: './warehouse-list.component.html',
  styleUrl: './warehouse-list.component.css'
})
export class WarehouseListComponent implements OnInit {
  isLoading: boolean = false;
  constructor(private _wareHouseService: WareHouseService,private router: Router) { }
  currentPage: number = 1;
  totalItems: number = 0;
  pageSize: number = 10;
  wareHousedata: WareHouseOutputDto;
  ngOnInit(): void {
    this.getAllWareHouses();
  }

  getAllWareHouses() {
    this.isLoading = true;
    this._wareHouseService.getAllWareHouses(this.currentPage, this.pageSize).subscribe(response => {
      this.wareHousedata = response;
      this.totalItems = response.totalItems;
      setTimeout(() => {
        this.isLoading = false;
    }, 1000)
  });
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize); 
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return; 
    }
    this.currentPage = page;
    this.getAllWareHouses(); 
  }

  viewWarehouse(id: number): void {
    this.router.navigate(['/view-warehouse', id]); 
  }

  navigateToCreateNewWarehousePage() {
    this.router.navigate(['/create-warehouse']);

  }
}
