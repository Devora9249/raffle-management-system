import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { DonorWithGiftsDto } from '../../../../core/models/donor-model';
import { TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-donor-gifts-dialog',
  imports: [DialogModule, TableModule, CommonModule],
  templateUrl: './donor-gifts-dialog.html',
  styleUrl: './donor-gifts-dialog.scss',
})
export class DonorGiftsDialog {
  @Input() visible = false;
  @Input() donor?: DonorWithGiftsDto;

  @Output() closed = new EventEmitter<void>();
}
