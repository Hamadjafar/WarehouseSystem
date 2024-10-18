import { Component, OnInit } from '@angular/core';
import { WareHouseDto } from '../../service/warehouse-service/wareHouseDto';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { WareHouseService } from '../../service/warehouse-service/warehouse-service';
import { Router } from '@angular/router';
import { CountryService } from '../../service/country-service/country-service';
import { CountryDto } from '../../service/country-service/countryDto';
import { LoaderComponent } from "../../../shared/loader/loader/loader.component";
import { NotificationService } from '../../../shared/notification/NotificationService';

@Component({
  selector: 'create-warehouse',
  standalone: true,
  imports: [FormsModule, CommonModule, LoaderComponent],
  templateUrl: './create-warehouse.component.html',
  styleUrl: './create-warehouse.component.css'
})
export class CreateWarehouseComponent implements OnInit {
  warehouse: WareHouseDto = new WareHouseDto();
  countries: CountryDto[];
  collapseStates: boolean[] = [];
  isLoading: boolean = false;

  constructor(private _wareHouseService: WareHouseService,
    private router: Router,
    private _countryService: CountryService,
    private _notificationService: NotificationService) { }


  ngOnInit(): void {
    this.getCountries();
  }

  onSubmit() {
    this.isLoading = true;
    this._wareHouseService.create(this.warehouse).subscribe(
      (result) => {
        setTimeout(() => {
          this.router.navigate(['/warehouse-list']);
          this.isLoading = false; 
          this._notificationService.showSuccess('Saved Successfully');
        }, 2000);
      },
      (error) => {
        this.isLoading = false; 
  
        if (error.error.errors) {
          const validationErrors = error.error.errors;
          const errorMessages = [];
  
          for (let field in validationErrors) {
            errorMessages.push(`${validationErrors[field].join(', ')}`);
          }
          this._notificationService.showError(errorMessages.join(' | '));
        } else {
          this._notificationService.showError(error.error.message);
        }
      }
    );
  }

  toggleCollapse(index: number) {
    this.collapseStates[index] = !this.collapseStates[index];
}

  addItem() {
    this.warehouse.wareHouseItemsDto.push({
      id: 0,
      itemName: '',
      skuCode: '',
      qty: 1,
      costPrice: 0,
      msrpPrice: null,
      wareHouseId: this.warehouse.id,
    });
  }

  removeItem(index: number) {
    this.warehouse.wareHouseItemsDto.splice(index, 1);
  }

  getCountries() {
    this._countryService.getCountries().subscribe(result => {
      this.countries = result;
    })
  }
}
