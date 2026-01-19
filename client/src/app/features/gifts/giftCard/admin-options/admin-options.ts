import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-admin-options',
  imports: [],
  templateUrl: './admin-options.html',
  styleUrl: './admin-options.scss',
})
export class AdminOptions {

  @Output() toDelete = new EventEmitter<void>();
  @Output() toEdit = new EventEmitter<void>();



  onDeleteClick() {
    this.toDelete.emit();
  }

  onEditClick() {
    this.toEdit.emit();
  }
} 
