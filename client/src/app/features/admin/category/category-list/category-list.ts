import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-category-list',
  imports: [TableModule,ButtonModule, CardModule],
  templateUrl: './category-list.html',
  styleUrl: '../../globalAdminDesign.scss',
})
export class CategoryList {
  @Input() categories: CategoryResponseDto[] = [];
  @Output() edit = new EventEmitter<CategoryResponseDto>();
  @Output() remove = new EventEmitter<number>();
}
