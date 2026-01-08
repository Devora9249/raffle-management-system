import { Component, EventEmitter, Output, Input, OnChanges, SimpleChanges } from '@angular/core'; 
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { GiftsService } from '../../../../core/services/gifts-service';
import { GiftResponseDto } from '../../../../core/models/gift-model'; 
import { CommonModule } from '@angular/common';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { DonorListItem } from '../../../../core/models/donor-model';

@Component({
  selector: 'app-gift-form-dialog',
  standalone: true,
  imports: [DialogModule, ButtonModule, ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './gift-form-dialog.html',
  styleUrl: './gift-form-dialog.scss',
})

export class GiftFormDialog implements OnChanges { 

  editMode: boolean = false;
  @Output() created = new EventEmitter<boolean>();
  @Output() closed = new EventEmitter<void>();


  @Input() giftToEdit: GiftResponseDto | null = null;
  @Input() categories: CategoryResponseDto[] = [];
  @Input() donors: DonorListItem[] = [];

  form!: FormGroup;
  selectedFile: File | null = null;
  previewUrl: string | null = null;

  showDialog = false;

  constructor(
    private fb: FormBuilder,
    private giftsService: GiftsService
  ) {
    this.form = this.fb.group({
      description: [''],
      price: [null],
      categoryId: [null],
      donorId: [null],
      image: [null],
    });
    console.log(this.donors, "donors");
    
    this.form.valueChanges.subscribe(value => {
      console.log('FORM VALUE:', value);
    });
  }


  private setCreateValidators() {
    this.editMode = false;
    console.log("editMode", this.editMode);

    this.form.get('price')?.setValidators([Validators.required, Validators.min(1)]);
    this.form.get('categoryId')?.setValidators([Validators.required]);
    this.form.get('donorId')?.setValidators([Validators.required]);
    this.form.get('image')?.setValidators([Validators.required]);
    this.form.updateValueAndValidity();
  }

  private setEditValidators() {
    this.editMode = true;
    console.log("editMode", this.editMode);
    this.form.get('price')?.setValidators([Validators.min(1)]);
    this.form.get('categoryId')?.clearValidators();
    this.form.get('donorId')?.clearValidators();
    this.form.get('image')?.clearValidators();
    this.form.updateValueAndValidity();
  }

  onCategoryChange(value: number | null): void {
    this.form.patchValue({ categoryId: value });
    console.log(value);
  }


  ngOnChanges(changes:SimpleChanges): void {
    console.log("change!", changes);
    
    if (this.giftToEdit) {
      this.setEditValidators();

      this.showDialog = true;
      this.form.patchValue({
        description: this.giftToEdit.description,
        price: this.giftToEdit.price,
        categoryId: this.giftToEdit.categoryId,
        donorId: this.giftToEdit.donorId,
      });

      if (this.giftToEdit.imageUrl) {
        this.previewUrl = `http://localhost:5071${this.giftToEdit.imageUrl}`;
      }
    }
  }


  get isEditMode(): boolean {
    return !!this.giftToEdit;
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0] ?? null;
    if (!file) return;

    this.selectedFile = file;
    this.form.patchValue({ image: file });
    this.previewUrl = URL.createObjectURL(file);
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formData = new FormData();
    formData.append('description', this.form.value.description || '');
    formData.append('price', this.form.value.price.toString());
    formData.append('categoryId', this.form.value.categoryId.toString());
    formData.append('donorId', this.form.value.donorId.toString());

    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    const request$ = this.isEditMode
      ? this.giftsService.update(this.giftToEdit!.id, formData)
      : this.giftsService.create(formData);

    request$.subscribe({
      next: (result) => {
        this.created.emit(true);
        this.close();
        this.resetForm();
        alert('Gift saved successfully!');
      },
      error: () => alert('שגיאה בשמירה')
    });
  }

  resetForm() {
    this.form.reset();
    this.selectedFile = null;
    this.previewUrl = null;
    this.giftToEdit = null; this.setCreateValidators();
  
  }

  open() {
    this.setCreateValidators(); 
    this.showDialog = true;
  }

  close() {
    this.showDialog = false;
    this.closed.emit();
  }
}
