// notification.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new BehaviorSubject<{ type: string, message: string } | null>(null);
  public notification$ = this.notificationSubject.asObservable();

  showSuccess(message: string): void {
    this.notificationSubject.next({ type: 'success', message });
    this.clearNotification();
  }

  showError(message: string): void {
    this.notificationSubject.next({ type: 'danger', message });
    this.clearNotification();
  }

  private clearNotification(): void {
    setTimeout(() => {
      this.notificationSubject.next(null);
    }, 3000); 
  }
}
