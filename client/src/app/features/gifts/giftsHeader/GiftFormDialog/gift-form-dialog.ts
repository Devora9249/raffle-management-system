// import { Component, EventEmitter, Output, Input, OnChanges, SimpleChanges } from '@angular/core'; 
// import { DialogModule } from 'primeng/dialog';
// import { ButtonModule } from 'primeng/button';
// import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
// import { GiftsService } from '../../../../core/services/gifts-service';
// import { GiftResponseDto } from '../../../../core/models/gift-model'; 
// import { CommonModule } from '@angular/common';
// import { CategoryResponseDto } from '../../../../core/models/category-model';
// import { DonorListItem } from '../../../../core/models/donor-model';
// import { SelectModule } from 'primeng/select';

// @Component({
//   selector: 'app-gift-form-dialog',
//   standalone: true,
//   imports: [DialogModule, ButtonModule, ReactiveFormsModule, CommonModule, FormsModule, SelectModule],
//   templateUrl: './gift-form-dialog.html',
//   styleUrl: './gift-form-dialog.scss',
// })

// export class GiftFormDialog implements OnChanges { 

//   editMode: boolean = false;
//   @Output() created = new EventEmitter<boolean>();
//   @Output() closed = new EventEmitter<void>(); 


//   @Input() giftToEdit: GiftResponseDto | null = null;
//   @Input() categories: CategoryResponseDto[] = [];
//   @Input() donors: DonorListItem[] = [];

//   form!: FormGroup;
//   selectedFile: File | null = null;
//   previewUrl: string | null = null;

//   showDialog = false;

//   constructor(
//     private fb: FormBuilder,
//     private giftsService: GiftsService
//   ) {
//     this.form = this.fb.group({
//       description: [''],
//       price: [null],
//       categoryId: [null],
//       donorId: [null],
//       image: [null],
//     });
//     console.log(this.donors, "donors");

//     this.form.valueChanges.subscribe(value => {
//       console.log('FORM VALUE:', value);
//     });
//   }


//   private setCreateValidators() {
//     this.editMode = false;
//     console.log("editMode", this.editMode);

//     this.form.get('price')?.setValidators([Validators.required, Validators.min(1)]);
//     this.form.get('categoryId')?.setValidators([Validators.required]);
//     this.form.get('donorId')?.setValidators([Validators.required]);
//     this.form.get('image')?.setValidators([Validators.required]);
//     this.form.updateValueAndValidity();
//   }

//   private setEditValidators() {
//     this.editMode = true;
//     console.log("editMode", this.editMode);
//     this.form.get('price')?.setValidators([Validators.min(1)]);
//     this.form.get('categoryId')?.clearValidators();
//     this.form.get('donorId')?.clearValidators();
//     this.form.get('image')?.clearValidators();
//     this.form.updateValueAndValidity();
//   }

//   onCategoryChange(value: number | null): void {
//     this.form.patchValue({ categoryId: value });
//     console.log(value);
//   }


//   ngOnChanges(changes:SimpleChanges): void {
//     console.log("change!", changes);

//     if (this.giftToEdit) {
//       this.setEditValidators();

//       this.showDialog = true;
//       this.form.patchValue({
//         description: this.giftToEdit.description,
//         price: this.giftToEdit.price,
//         categoryId: this.giftToEdit.categoryId,
//         donorId: this.giftToEdit.donorId,
//       });

//       if (this.giftToEdit.imageUrl) {
//         this.previewUrl = `http://localhost:5071${this.giftToEdit.imageUrl}`;
//       }
//     }
//   }


//   get isEditMode(): boolean {
//     return !!this.giftToEdit;
//   }

//   onFileChange(event: Event) {
//     const input = event.target as HTMLInputElement;
//     const file = input.files?.[0] ?? null;
//     if (!file) return;

//     this.selectedFile = file;
//     this.form.patchValue({ image: file });
//     this.previewUrl = URL.createObjectURL(file);
//   }

//   save() {
//     if (this.form.invalid) {
//       this.form.markAllAsTouched();
//       return;
//     }

//     const formData = new FormData();
//     formData.append('description', this.form.value.description || '');
//     formData.append('price', this.form.value.price.toString());
//     formData.append('categoryId', this.form.value.categoryId.toString());
//     formData.append('donorId', this.form.value.donorId.toString());

//     if (this.selectedFile) {
//       formData.append('image', this.selectedFile);
//     }

//     const request$ = this.isEditMode
//       ? this.giftsService.update(this.giftToEdit!.id, formData)
//       : this.giftsService.create(formData);

//     request$.subscribe({
//       next: (result) => {
//         this.created.emit(true);
//         this.close();
//         this.resetForm();
//         alert('Gift saved successfully!');
//       },
//       error: () => alert('שגיאה בשמירה')
//     });
//   }

//   resetForm() {
//     this.form.reset();
//     this.selectedFile = null;
//     this.previewUrl = null;
//     this.giftToEdit = null;
//     this.setCreateValidators();

//   }

//   open() {
//     this.form.reset();
//     this.setCreateValidators(); 
//     this.showDialog = true;
//   }

//   close() {
//     this.showDialog = false;
//     this.closed.emit();
//   }
// }

// import { Component, EventEmitter, Output, Input, OnChanges, SimpleChanges } from '@angular/core';
// import { DialogModule } from 'primeng/dialog';
// import { ButtonModule } from 'primeng/button';
// import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
// import { GiftsService } from '../../../../core/services/gifts-service';
// import { GiftResponseDto } from '../../../../core/models/gift-model';
// import { CommonModule } from '@angular/common';
// import { CategoryResponseDto } from '../../../../core/models/category-model';
// import { DonorListItem } from '../../../../core/models/donor-model';
// import { SelectModule } from 'primeng/select';

// @Component({
//   selector: 'app-gift-form-dialog',
//   standalone: true,
//   imports: [ DialogModule, ButtonModule, ReactiveFormsModule, CommonModule, FormsModule, SelectModule,],
//   templateUrl: './gift-form-dialog.html',
//   styleUrl: './gift-form-dialog.scss',
// })
// export class GiftFormDialog implements OnChanges {

//   @Input() giftToEdit: GiftResponseDto | null = null;
//   @Input() categories: CategoryResponseDto[] = [];
//   @Input() donors: DonorListItem[] = [];

//   @Output() created = new EventEmitter<boolean>();
//   @Output() closed = new EventEmitter<void>();

//   form: FormGroup;
//   showDialog = false;
//   editMode = false;

//   selectedFile: File | null = null;
//   previewUrl: string | null = null;

//   constructor(
//     private fb: FormBuilder,
//     private giftsService: GiftsService
//   ) {
//     this.form = this.fb.group({
//       description: [''],
//       price: [null],
//       categoryId: [null],
//       donorId: [null],
//       image: [null],
//     });
//   }

//   /* =======================
//      MODE PREPARATION
//      ======================= */

//   private prepareCreateMode(): void {
//     this.editMode = false;
//     this.form.reset();
//     this.selectedFile = null;
//     this.previewUrl = null;

//     this.form.get('price')?.setValidators([Validators.required, Validators.min(1)]);
//     this.form.get('categoryId')?.setValidators([Validators.required]);
//     this.form.get('donorId')?.setValidators([Validators.required]);
//     this.form.get('image')?.setValidators([Validators.required]);

//       console.log('gifttoedit in CREATE MODE', this.form.value);

//     this.form.updateValueAndValidity();
//   }

//   private prepareEditMode(gift: GiftResponseDto): void {
//     this.editMode = true;
//     this.form.reset();

//     this.selectedFile = null;  
//     this.previewUrl = null;

//     this.form.get('price')?.setValidators([Validators.min(1)]);
//     this.form.get('categoryId')?.clearValidators();
//     this.form.get('donorId')?.clearValidators();
//     this.form.get('image')?.clearValidators();

//     this.form.patchValue({
//       description: gift.description,
//       price: gift.price,
//       categoryId: gift.categoryId,
//       donorId: gift.donorId,
      
//     });

//     this.previewUrl = gift.imageUrl
//       ? `http://localhost:5071${gift.imageUrl}`
//       : null;


//     this.form.updateValueAndValidity();
//           console.log('gifttoedit in EDIT MODE', this.form.value);
//       console.log('imageurl in EDIT MODE', this.previewUrl);
//   }

//   /* =======================
//      LIFECYCLE
//      ======================= */

//   ngOnChanges(changes: SimpleChanges): void {
//     if (changes['giftToEdit'] && this.giftToEdit) {
//       this.prepareEditMode(this.giftToEdit);
//       this.showDialog = true;
//     }
//   }

//   /* =======================
//      PUBLIC API
//      ======================= */

//   open(): void {
//           console.log('gifttoedit in OPEN', this.form.value);

//     this.prepareCreateMode();
//     this.showDialog = true;
//   }

//   close(): void {
//           console.log('gifttoedit in CLOSE', this.form.value);
    
//     this.showDialog = false;
//     this.closed.emit();
//   }

//   /* =======================
//      FORM ACTIONS
//      ======================= */

//   onFileChange(event: Event): void {
//     const input = event.target as HTMLInputElement;
//     const file = input.files?.[0] ?? null;
//     if (!file) return;

//     this.selectedFile = file;
//     this.form.patchValue({ image: file });
//     this.previewUrl = URL.createObjectURL(file);
//   }

//   save(): void {
//     if (this.form.invalid) {
//       this.form.markAllAsTouched();
//       return;
//     }

//     const formData = new FormData();
//     formData.append('description', this.form.value.description || '');
//     formData.append('price', this.form.value.price.toString());
//     formData.append('categoryId', this.form.value.categoryId.toString());
//     formData.append('donorId', this.form.value.donorId.toString());

//     if (this.selectedFile) {
//       formData.append('image', this.selectedFile);
//     }

//     const request$ = this.editMode
//       ? this.giftsService.update(this.giftToEdit!.id, formData)
//       : this.giftsService.create(formData);

//     request$.subscribe({
//       next: () => {
//         this.created.emit(true);
//         this.close();
//       },
//       error: () => alert('שגיאה בשמירה'),
//     });
//               console.log('gifttoedit in SAVE', this.form.value);

//   }
// }




import { Component, EventEmitter, Output, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { GiftsService } from '../../../../core/services/gifts-service';
import { GiftResponseDto } from '../../../../core/models/gift-model';
import { CommonModule } from '@angular/common';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { DonorListItem } from '../../../../core/models/donor-model';
import { SelectModule } from 'primeng/select';

@Component({
  selector: 'app-gift-form-dialog',
  standalone: true,
  imports: [DialogModule, ButtonModule, ReactiveFormsModule, CommonModule, FormsModule, SelectModule],
  templateUrl: './gift-form-dialog.html',
  styleUrl: './gift-form-dialog.scss',
})
export class GiftFormDialog implements OnChanges {

  @Input() giftToEdit: GiftResponseDto | null = null;
  @Input() categories: CategoryResponseDto[] = [];
  @Input() donors: DonorListItem[] = [];

  @Output() created = new EventEmitter<boolean>();
  @Output() closed = new EventEmitter<void>();

  form: FormGroup;
  showDialog = false;
  editMode = false;

  selectedFile: File | null = null;  
  previewUrl: string | null = null;    

  constructor(private fb: FormBuilder, private giftsService: GiftsService) {
    this.form = this.fb.group({
      description: [''],
      price: [null, [Validators.required, Validators.min(1)]],
      categoryId: [null, Validators.required],
      donorId: [null, Validators.required],
    });
  }

  /* =======================
     LIFECYCLE
     ======================= */

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['giftToEdit'] && this.giftToEdit) {
      this.prepareEditMode(this.giftToEdit);
      this.showDialog = true;
    }
  }

  /* =======================
     MODE PREPARATION
     ======================= */

  private prepareCreateMode(): void {
    this.editMode = false;
    this.form.reset();
    this.selectedFile = null;
    this.previewUrl = null; 
  }

  private prepareEditMode(gift: GiftResponseDto): void {
    this.editMode = true;
    this.form.reset();

    this.selectedFile = null; 
    console.log(gift.imageUrl, 'giftimageurl1');
        console.log(this.previewUrl, 'previewurl1');

    this.previewUrl = gift.imageUrl ? `http://localhost:5071${gift.imageUrl}` : null;
    console.log(gift.imageUrl, 'giftimageurl2');

    console.log(this.previewUrl, 'previewurl2');
    

    this.form.patchValue({
      description: gift.description,
      price: gift.price,
      categoryId: gift.categoryId,
      donorId: gift.donorId,
    });
  }

  /* =======================
     PUBLIC API
     ======================= */

  open(): void {
    this.prepareCreateMode();
    this.showDialog = true;
  }

  close(): void {
    this.showDialog = false;
    this.closed.emit();
  }

  /* =======================
     FORM ACTIONS
     ======================= */

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0] ?? null;
    if (!file) return;

    this.selectedFile = file;
    this.previewUrl = URL.createObjectURL(file);
  }

  save(): void {
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

    const request$ = this.editMode
      ? this.giftsService.update(this.giftToEdit!.id, formData)
      : this.giftsService.create(formData);

    request$.subscribe({
      next: () => {
        this.created.emit(true);
        this.close();
      },
      error: () => alert('שגיאה בשמירה'),
    });
  }
}
