import { Component, Input } from '@angular/core';
import { DonorListItem, DonorWithGiftsDto } from '../../../../core/models/donor-model';
import { DonorService } from '../../../../core/services/donor-service';
import { DonorList } from '../donor-list/donor-list';
import { DonorFormDialog } from '../donor-form-dialog/donor-form-dialog';
import { ButtonModule } from 'primeng/button';
import { DonorFiltersBar } from '../donor-filters-bar/donor-filters-bar';
import { DonorGiftsDialog } from '../donor-gifts-dialog/donor-gifts-dialog';

@Component({
  selector: 'app-donor-panel',
  imports: [DonorList, DonorFormDialog, ButtonModule, DonorFiltersBar, DonorGiftsDialog],
  templateUrl: './donor-panel.html',
  styleUrl: './donor-panel.scss',
})
export class DonorPanel {

  donors: DonorWithGiftsDto[] = [];
  allDonors: DonorWithGiftsDto[] = [];
  showCreateDialog = false;
  @Input() selectedDonor?: DonorWithGiftsDto;
  searchTermGifts: string = '';
  searchTermDonors: string = '';

  showGiftsDialog = false;

  constructor(private donorService: DonorService) {}

  ngOnInit(): void {
    this.loadDonors();
  }

  loadDonors(): void {
    this.donorService.getDonorsWithGifts().subscribe(res => {
      this.donors = res;
      this.allDonors = res;
      console.log(res, 'res');
      
    });
  }

  openCreate(): void {
    this.showCreateDialog = true;
  }

onFilterChanged(): void {
  let result = this.allDonors;
  console.log("allDonors", this.allDonors);
  

  if (this.searchTermDonors) {
    const term = this.searchTermDonors.toLowerCase();
    result = result.filter(d =>
      d.name.toLowerCase().includes(term)
    );
  }

  if (this.searchTermGifts) {
    const term = this.searchTermGifts.toLowerCase();
    result = result.filter(d =>
      d.gifts.some(g =>
        g.description.toLowerCase().includes(term)
      )
    );
    console.log(result, "result");
    
  }

  this.donors = result;
}




  // filterGifts(): void {
  //   if (!this.searchTermGifts) {
  //     this.loadDonors();
  //     return;
  //   }
  //   const term = this.searchTermGifts.toLowerCase();
  //   this.donors = this.allDonors.filter(donor =>
  //     donor.gifts.some(gift => gift.description.toLowerCase().includes(term))
      
  //   );
  //         console.log(this.donors, 'donors1');

  // }

  // filterDonors(): void {
  //   if (!this.searchTermDonors) {
  //     this.loadDonors();
  //     return;
  //   }
  //   const term = this.searchTermDonors.toLowerCase();
  //   this.donors = this.allDonors.filter(donor =>
  //     donor.name.toLowerCase().includes(term)
  //   );
  // }

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
    console.log("hi");
    
    this.showCreateDialog = false;
    this.loadDonors();
  }

  // openGiftsDialog(): void {
  //   this.showGiftsDialog = true;
  // }

  
  openGiftsDialog(donor: DonorWithGiftsDto): void {
  this.selectedDonor = donor;
  this.showGiftsDialog = true;
}

closeGiftsDialog(): void {
  this.showGiftsDialog = false;
  this.selectedDonor = undefined;
}
}