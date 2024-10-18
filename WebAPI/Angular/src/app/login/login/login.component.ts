import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../service/login-service/login-service';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth-service/auth-service';
import { NotificationService } from '../../../shared/notification/NotificationService';
import { LoaderComponent } from "../../../shared/loader/loader/loader.component";

@Component({
  selector: 'login',
  standalone: true,
  imports: [FormsModule, LoaderComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {
  email: string;
  password: string;
  isLoading: boolean = false;

  constructor(private _loginService: LoginService,
    private router: Router,
    private _authService: AuthService,
    private _notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    if (this._authService.isLoggedIn()) {
      this.router.navigateByUrl('/Home');
    }
  }


  login() {
    this.isLoading = true;
    this._loginService.login(this.email.trim(), this.password.trim()).subscribe(
      (result) => {
        if (result) {
          this._authService.setItem("currentUser", result);
          this.router.navigateByUrl('/Home');
          this.isLoading = false;
        }
      },
      (error) => {
        this.isLoading = false;
        if (error.error && error.error.message) {
          this._notificationService.showError(error.error.message);
        } else {
          this._notificationService.showError('Login failed. Please try again.');
        }
      }
    );
  }
}
