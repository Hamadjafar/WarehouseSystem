import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DashboadService } from '../../service/dashboad-service/dashboad-service';
import { WareHouseStatusDto } from '../../service/dashboad-service/WareHouseStatusDto';
import { Chart, registerables } from 'chart.js';
import { LoaderComponent } from "../../../shared/loader/loader/loader.component";
import { TopHighAndLowItemsDto } from '../../service/dashboad-service/TopHighAndLowItemsDto';

@Component({
  selector: 'dashboard',
  standalone: true,
  imports: [LoaderComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  wareHouseStatusDto: WareHouseStatusDto[];
  topHighAndLowItemsDto: TopHighAndLowItemsDto;
  chart: any;
  isLoading: boolean = false;
  @ViewChild('lineChart', { static: true }) lineChartRef!: ElementRef<HTMLCanvasElement>;
  lineChart: any;

  constructor(private _dashboadService: DashboadService) {

  }
  ngOnInit(): void {
    this.getWareHouseStatus();
    this.getTopHighAndLowItemsByQuantity();
    Chart.register(...registerables); 
  }


  getWareHouseStatus() {
    this.isLoading = true;
    this._dashboadService.getWareHouseStatus().subscribe(result => {
      this.wareHouseStatusDto = result;
      this.createChart();
      setTimeout(() => {
        this.isLoading = false;
      }, 100)
    })
  }

  getTopHighAndLowItemsByQuantity() {
    this.isLoading = true;
    this._dashboadService.getTopHighAndLowItemsByQuantity().subscribe(result => {
      this.topHighAndLowItemsDto = result;
      this.createLineChart();
      setTimeout(() => {
        this.isLoading = false;
      }, 100)
    })
  }

  createChart(): void {
    const labels = this.wareHouseStatusDto.map(item => item.name);
    const data = this.wareHouseStatusDto.map(item => item.count);
    const ctx = document.getElementById('warehouseChart') as HTMLCanvasElement;
    this.chart = new Chart(ctx, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Count',
          data: data,
          backgroundColor: ['#36A2EB', '#FF6384', '#FFCE56'],
          hoverBackgroundColor: ['#36A2EB', '#FF6384', '#FFCE56']
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
      }
    });
  }

  createLineChart(): void {
    const ctx = this.lineChartRef.nativeElement.getContext('2d');  // Get canvas context

    this.lineChart = new Chart(ctx, {
      type: 'line',  
      data: {
        labels: [...this.topHighAndLowItemsDto.topHighItems.map(item => item.name), ...this.topHighAndLowItemsDto.topLowItems.map(item => item.name)],
        datasets: [
          {
            label: 'Count High Quntity',
            data: this.topHighAndLowItemsDto.topHighItems.map(item => item.quantity),
            borderColor: '#4e61d6',
            fill: false,
            tension: 0.4
          },
          {
            label: 'Count Low Quntity',
            data: this.topHighAndLowItemsDto.topLowItems.map(item => item.quantity),
            borderColor: '#d64e4e',
            fill: false,
            tension: 0.4
          }
        ]
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: true,
            position: 'top'
          }
        },
        scales: {
          x: {
            title: {
              display: true,
              text: 'Items'
            }
          },
          y: {
            title: {
              display: true,
              text: 'Quantity'
            },
            beginAtZero: true
          }
        }
      }
    });
  }
}
