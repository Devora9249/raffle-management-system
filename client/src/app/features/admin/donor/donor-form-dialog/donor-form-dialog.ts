import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DonorListItem, DonorWithGiftsDto } from '../../../../core/models/donor-model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DonorService } from '../../../../core/services/donor-service';
import { AuthService } from '../../../../core/services/auth-service';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { NotificationService } from '../../../../core/services/notification-service';
@Component({
  selector: 'app-donor-form-dialog',
  imports: [ButtonModule, DialogModule,ReactiveFormsModule,],
  templateUrl: './donor-form-dialog.html',
  styleUrl: './donor-form-dialog.scss',
})
export class DonorFormDialog {


  @Input() visible = false;
  @Input() donor?: DonorWithGiftsDto;

  @Output() saved = new EventEmitter<void>();
  @Output() closed = new EventEmitter<void>();

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private donorsService: DonorService,
    private notificationService: NotificationService,
    // private authService: AuthService
  ) {}

  ngOnChanges(): void {
    this.form = this.fb.group({
      name: [this.donor?.name ?? '', Validators.required],
      email: [this.donor?.email ?? '', [Validators.required, Validators.email]],
      password: ['', this.donor ? [] : [Validators.required, Validators.minLength(6)]],
      phone: [this.donor?.phone ?? ''],
      city: [this.donor?.city ?? ''],
      address: [this.donor?.address ?? ''],
    });
  }

  submit(): void {
    // if (this.category) {
    //   this.authService.update()
    //     .subscribe(() => this.saved.emit());
    // } else {
      this.donorsService.addDonor(this.form.value)
        .subscribe(() => {
          this.saved.emit();
          this.notificationService.showSuccess('Donor added successfully');
        });
    // }
  }
}

