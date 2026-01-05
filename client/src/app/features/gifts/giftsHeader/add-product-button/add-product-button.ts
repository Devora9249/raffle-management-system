import { Component, EventEmitter, Output } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { GiftsService } from '../../../../core/services/gifts-service';
import { GiftCreateDto } from '../../../../core/models/gift-model';
@Component({
  selector: 'app-add-product-button',
  imports: [DialogModule, ButtonModule, ReactiveFormsModule],
  templateUrl: './add-product-button.html',
  styleUrl: './add-product-button.scss',
})
export class AddProductButton {

  @Output() created = new EventEmitter<any>();

  form!: FormGroup;
  // selectedFile: File | null = null;
  // previewUrl: string | null = null;

  constructor(
    private fb: FormBuilder,
    private giftsService: GiftsService
  ) {
    this.form = this.fb.group({
      description: [''],
      price: [null, [Validators.required, Validators.min(1)]],
      categoryId: [null, Validators.required],
      donorId: [null, Validators.required],
      // image: [null, Validators.required],
    });
  }

  // כשבוחרים תמונה
  // onFileChange(event: Event) {
  //   const input = event.target as HTMLInputElement;
  //   const file = input.files?.[0] ?? null;

  //   if (!file) return;

  //   this.selectedFile = file;
  //   this.form.patchValue({ image: file });
  //   this.form.get('image')?.markAsTouched();

  //   // תצוגה מקדימה (preview)
  //   this.previewUrl = URL.createObjectURL(file);
  // }

  // שמירה
  save() {
    if (this.form.invalid) {   //|| !this.selectedFile
      this.form.markAllAsTouched();
      return;
    }

    // DTO (חלק הנתונים הטקסטואליים)
    const model: GiftCreateDto = {
      description: this.form.value.description || '',
      price: Number(this.form.value.price),
      categoryId: Number(this.form.value.categoryId),
      donorId: Number(this.form.value.donorId),
      // imageUrl: this.form.value.imageUrl || ''
    };

    // FormData עבור קובץ + שדות
    // const formData = new FormData();
    // formData.append('price', model.price.toString());
    // formData.append('description', model.description ?? '');
    // formData.append('categoryId', model.categoryId.toString());
    // formData.append('donorId', model.donorId.toString());
    // formData.append('image', this.selectedFile);


    this.giftsService.create(model).subscribe({
      next: (createdProduct) => {
        this.created.emit(createdProduct);
        this.resetForm();
        alert('Gift created successfully!');
        this.close();
        this.form.reset();
      },
      error: (err) => {
        console.error('Create gift failed', err);
        alert('Create gift failed. Please check your input.');
      }
    });
  }

  // איפוס
  resetForm() {
    this.form.reset();
    // this.selectedFile = null;

    // if (this.previewUrl) {
    //   URL.revokeObjectURL(this.previewUrl);
    // }
    // this.previewUrl = null;
  }


  //פתיחה וסגירה של הדיאלוג

  showDialog = false;

  open() {
    this.showDialog = true;
  }

  close() {
    this.showDialog = false;
  }
}