import { Component } from '@angular/core';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { CategoriesService } from '../../../../core/services/categories-service';
import { CategoryList } from '../category-list/category-list';
import { CategoryFormDialog } from '../category-form-dialog/category-form-dialog';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { NotificationService } from '../../../../core/services/notification-service';
import { TagModule } from 'primeng/tag';

@Component({
  selector: 'app-category-panel',
  imports: [CategoryList, CategoryFormDialog, ButtonModule, CardModule, TagModule],
  templateUrl: './category-panel.html',
  styleUrl: '../../globalAdminDesign.scss',
})
export class CategoryPanel {
  categories: CategoryResponseDto[] = [];
  showDialog = false;
  selectedCategory?: CategoryResponseDto;

  constructor(private categoryService: CategoriesService,private notificationService: NotificationService ) {}
  
  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe(res => {
      this.categories = res;
    });
  }

  openCreate(): void {
    this.selectedCategory = undefined;
    this.showDialog = true;
  }

  openEdit(category: CategoryResponseDto): void {
    this.selectedCategory = category;
    this.showDialog = true;
  }

delete(id: number): void {
  this.notificationService.confirmDelete(() => {
  this.categoryService.delete(id).subscribe({
    next: () => {
      this.loadCategories();
      this.notificationService.showSuccess('Category deleted successfully');
    },
    error: (err) => {
      console.error('Delete category failed', err);
  const message =
          err?.error?.detail ||
          err?.error?.message ||
          'Unauthorized or unexpected error';

        this.notificationService.showError('Delete category failed: ' + message);
    }
  });
});
}


  onSaved(): void {
    this.showDialog = false;
    this.loadCategories();
  }
}
