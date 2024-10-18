import { Component, OnInit } from '@angular/core';
import { RoleService } from '../../../service/role-service/role-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../../../shared/loader/loader/loader.component';
import { RoleDto } from '../../../service/users-service/roleDto';
import { UserService } from '../../../service/users-service/user-service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserDto } from '../../../service/users-service/userDto';
import { NotificationService } from '../../../../shared/notification/NotificationService';

@Component({
  selector: 'create-or-edit-user',
  standalone: true,
  imports: [FormsModule, CommonModule, LoaderComponent],
  templateUrl: './create-or-edit-user.component.html',
  styleUrl: './create-or-edit-user.component.css'
})
export class CreateOrEditUserComponent implements OnInit {
  roles: RoleDto[];
  id: number;
  userDto: UserDto = new UserDto();
  isEditMode: boolean;
  isLoading: boolean = false;
  constructor(private _roleService: RoleService,
    private _userService: UserService,
    private _route: ActivatedRoute,
    private router: Router,
    private _notificationService: NotificationService) {

  }


  ngOnInit(): void {
    this.getRoles();
    this._route.params.subscribe(params => {
      this.id = params['id'];
      if (this.id) {
        this.isEditMode = true;
        this.getUserById(this.id);
      } else {
        this.isEditMode = false;
      }
    })
  }

  createOrUpdateUser() {
    this.isLoading = true;
    this._userService.createOrUpdateUser(this.userDto).subscribe(
      result => {
        this.isLoading = false;
        this._notificationService.showSuccess(this.isEditMode ? 'User updated successfully' : 'User created successfully.');
        this.router.navigate(['/users']);
      },
      error => {
        this.isLoading = false;
        if (error.error?.errors) {
          const errorMessages = [];
          for (const key in error.error.errors) {
            if (error.error.errors.hasOwnProperty(key)) {
              const messages = error.error.errors[key];
              errorMessages.push(...messages);
            }
          }
          const combinedMessage = errorMessages.join('\n');
          this._notificationService.showError(combinedMessage);
        } else {
          const errorMessage = error.error?.message || 'An error occurred. Please try again.';
          this._notificationService.showError(errorMessage);
        }
      }
    );
  }

  onActiveChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    this.userDto.isActive = selectElement.value === 'true'; 
  }

  getUserById(userId: number) {
    this._userService.getUserById(userId).subscribe(result => {
      this.userDto = result;
    })
  }

  getRoles() {
    this._roleService.getRoles().subscribe(result => {
      this.roles = result;
    })
  }
}
