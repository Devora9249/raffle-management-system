import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoriesService } from '../../../../core/services/categories-service';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { NotificationService } from '../../../../core/services/notification-service';
import { ValidationErrorDirective } from '../../../../shared/directives/validation-error';
@Component({
  selector: 'app-category-form-dialog',
  imports: [DialogModule,ReactiveFormsModule, ButtonModule,ValidationErrorDirective],
  templateUrl: './category-form-dialog.html',
  styleUrl: './category-form-dialog.scss',
})
export class CategoryFormDialog {

  @Input() visible = false;
  @Input() category?: CategoryResponseDto;

  @Output() saved = new EventEmitter<void>();
  @Output() closed = new EventEmitter<void>();

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoriesService,
    private notificationService: NotificationService
  ) {}

  ngOnChanges(): void {
    this.form = this.fb.group({
      name: [this.category?.name ?? '', Validators.required],
      
    });
    
  }

  submit(): void {
    if (this.category) {
      this.categoryService.update(this.category.id, this.form.value)
        .subscribe(() =>{
       this.saved.emit()
      this.notificationService.showSuccess('Category updated successfully');
      } );
    } else {
      this.categoryService.create(this.form.value)
        .subscribe(() => {
          this.saved.emit();
          this.notificationService.showSuccess('Category created successfully');
        });
    }
  }
}
