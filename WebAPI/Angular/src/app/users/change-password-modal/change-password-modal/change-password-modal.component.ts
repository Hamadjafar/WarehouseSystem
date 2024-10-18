import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../../service/users-service/user-service';
import { ChangePasswordDto } from '../../../service/users-service/changePasswordDto';
import { NotificationService } from '../../../../shared/notification/NotificationService';

@Component({
  selector: 'change-password-modal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './change-password-modal.component.html',
  styleUrl: './change-password-modal.component.css'
})
export class ChangePasswordModalComponent {

  newPassword: string;
  confirmPassword: string;
  @ViewChild('myModal') modal: ElementRef;
  changePasswordDto: ChangePasswordDto = new ChangePasswordDto();
  constructor(private _userService: UserService, private _notificationService: NotificationService) {

  }



  changePassword() {
    if (this.newPassword !== this.confirmPassword) {
      this._notificationService.showError("Passwords do not match!");
      return;
    }
    this.changePasswordDto.newPassword = this.newPassword;
    this._userService.changePassword(this.changePasswordDto).subscribe({
      next: (result) => {
        this.closeModal();
        this._notificationService.showSuccess('Password changed successfully');
      },
      error: (error) => {
        if (error.error.errors) {
            const newPasswordErrors = error.error.errors.NewPassword;
            if (newPasswordErrors && newPasswordErrors.length > 0) {
                this._notificationService.showError(newPasswordErrors[0]);
            } else {
                this._notificationService.showError("An error occurred. Please try again.");
            }
        } else {
            this._notificationService.showError("An unexpected error occurred.");
        }
    }
    });
  }

  openModal(userId: number) {
    this.changePasswordDto = new ChangePasswordDto();
    this.newPassword = undefined;
    this.confirmPassword = undefined;
    this.changePasswordDto.userId = userId;
    this.modal.nativeElement.classList.add('show');
    this.modal.nativeElement.style.display = 'block';
    document.body.classList.add('modal-open');
  }


  closeModal() {
    this.modal.nativeElement.classList.add('show');
    this.modal.nativeElement.style.display = 'none';
    document.body.classList.add('modal-open');
    const modalBackdrop = document.querySelector('.modal-backdrop');
    if (modalBackdrop) {
      modalBackdrop.remove(); 
    }
  }

}
