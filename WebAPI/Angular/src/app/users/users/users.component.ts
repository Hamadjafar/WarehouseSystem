import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../service/users-service/user-service';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from '../../../shared/loader/loader/loader.component';
import { FormsModule } from '@angular/forms';
import { UsersOutputDto } from '../../service/users-service/UsersOutputDto';
import { ChangePasswordModalComponent } from '../change-password-modal/change-password-modal/change-password-modal.component';
import { NotificationService } from '../../../shared/notification/NotificationService';
import { Router } from '@angular/router';
import { AppConsts } from '../../../shared/AppConsts';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, LoaderComponent, FormsModule,ChangePasswordModalComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  @ViewChild('changePasswordModalComponent') changePasswordModalComponent:ChangePasswordModalComponent;
  currentPage: number = 1;
  totalItems: number = 0;
  pageSize: number = 10;
  isLoading: boolean = false;
  usersOutputDto: UsersOutputDto;
  adminRole: string = AppConsts.adminRole;
  constructor(private _userService: UserService,
    private _notificationService: NotificationService,
    private router: Router) {

  }

  ngOnInit(): void {
    this.getAllUsers();
  }




  getAllUsers() {
    this.isLoading = true;
    this._userService.getAllUsers(this.currentPage, this.pageSize).subscribe(result => {
      this.usersOutputDto = result;
      this.totalItems = result.totalItems;
      setTimeout(() => {
        this.isLoading = false;
      }, 1000)
    });
  }

  openChangePasswordModal(userId: number) {
    this.changePasswordModalComponent.openModal(userId)
  }

  


  editUser(userId: number) {
    this.router.navigate(['/create-or-edit-user',userId])
  }

  deleteUser(userId: number) {
    const isConfirmed = window.confirm('Are you sure you want to delete this user?');
    
    if (isConfirmed) {
        this.isLoading = true; 
        this._userService.deactivateUser(userId).subscribe(
            (result) => {
                this.isLoading = false; 
                this._notificationService.showSuccess('User deleted successfully.'); 
                this.getAllUsers(); 
            },
            (error) => {
                this.isLoading = false; 
                this._notificationService.showError('Failed to delete the user. Please try again later.'); 
                console.error('Delete user error:', error); 
            }
        );
    }
}


  get totalPages(): number {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  changePage(page: number): void {
    if (page < 1 || page > this.totalPages) {
      return;
    }
    this.currentPage = page;
    this.getAllUsers();
  }


  navigateToCreateUserPage(){
    this.router.navigate(['/create-or-edit-user'])
  }
  

}
