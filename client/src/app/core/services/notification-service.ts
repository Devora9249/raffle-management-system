import { Injectable } from '@angular/core';
import { MessageService,ConfirmationService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private messageService: MessageService,private confirmationService: ConfirmationService) { }

  // פונקציה להודעת הצלחה
  showSuccess(message: string) {
    this.messageService.add({ 
      severity: 'success', 
      summary: 'Success', 
      detail: message,
      life: 800
    });
  }

  // פונקציה להודעת שגיאה
  showError(message: string) {
    this.messageService.add({ 
      severity: 'error', 
      summary: 'Error', 
      detail: message,
      life: 800
    });
  }
  confirmDelete(onConfirm: () => void) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this item?',
      header: 'Delete Confirmation',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'yes',
      rejectLabel: 'no',
      acceptButtonStyleClass: 'p-button-danger p-button-text',
      rejectButtonStyleClass: 'p-button-text',
      
      accept: () => {
        onConfirm();
      }
    });
  }
}