import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../../shared/notification/NotificationService';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css'
})
export class NotificationComponent implements OnInit {
  notification: { type: string, message: string } | null = null;

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.notificationService.notification$.subscribe(notification => {
      this.notification = notification;
    });
  }

  close(): void {
    this.notification = null;
  }
}
