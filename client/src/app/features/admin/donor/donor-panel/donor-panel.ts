import { Component } from '@angular/core';
import { DonorListItem } from '../../../../core/models/donor-model';
import { DonorService } from '../../../../core/services/donor-service';
import { DonorList } from '../donor-list/donor-list';
import { DonorFormDialog } from '../donor-form-dialog/donor-form-dialog';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-donor-panel',
  imports: [DonorList, DonorFormDialog, ButtonModule],
  templateUrl: './donor-panel.html',
  styleUrl: './donor-panel.scss',
})
export class DonorPanel {

donors: DonorListItem[] = [];
  showDialog = false;
  selectedDonor?: DonorListItem;

  constructor(private donorService: DonorService) {}

  ngOnInit(): void {
    this.loadDonors();
  }

  loadDonors(): void {
    this.donorService.getDonors().subscribe(res => {
      this.donors = res;
    });
  }

  openCreate(): void {
    this.selectedDonor = undefined;
    this.showDialog = true;
  }

  // openEdit(donor: DonorListItem): void {
  //   this.selectedDonor = donor;
  //   this.showDialog = true;
  // }

// delete(id: number): void {
//   this.authService.delete(id).subscribe({
//     next: () => {
//       this.loadDonors();
//     },
//     error: (err) => {
//       console.error('Delete donor failed', err);
//   const message =
//           err?.error?.detail ||
//           err?.error?.message ||
//           'Unauthorized or unexpected error';

//         alert('Delete donor failed: ' + message);
//     }
//   });
// }


  onSaved(): void {
    this.showDialog = false;
    this.loadDonors();
  }
}