import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from './service/auth-service/auth-service';
import { CommonModule } from '@angular/common';
import { NotificationComponent } from './Notification/notification/notification.component';
import { AppConsts } from '../shared/AppConsts';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HttpClientModule, CommonModule, NotificationComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'WarehouseSystem';
  adminRole: string = AppConsts.adminRole;
  constructor(private router: Router, public _authService: AuthService) { }

  navigateTo(route: string): void {
    this.router.navigate([route]);
  }

  logout() {
    this._authService.logout();
  }
}
