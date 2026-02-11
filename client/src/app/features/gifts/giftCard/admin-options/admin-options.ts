import { Component, EventEmitter, Output } from '@angular/core';
import { NotificationService } from '../../../../core/services/notification-service';

@Component({
  selector: 'app-admin-options',
  imports: [],
  templateUrl: './admin-options.html',
  styleUrl: './admin-options.scss',
})
export class AdminOptions {

  @Output() toDelete = new EventEmitter<void>();
  @Output() toEdit = new EventEmitter<void>();


constructor(private notification: NotificationService) {}

  onDeleteClick() {
    this.notification.confirmDelete(()=>{
          this.toDelete.emit();
          this.notification.showSuccess('Gift card deleted successfully');
    })
  }

  onEditClick() {
    this.toEdit.emit();
    this.notification.showSuccess('Edit gift card clicked');
  }
} 
