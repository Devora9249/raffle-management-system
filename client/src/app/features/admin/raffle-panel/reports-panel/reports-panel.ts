import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { WinningService } from '../../../../core/services/winning-service';
import { XlService } from '../../../../core/services/xl-service';

@Component({
  selector: 'app-reports-panel',
  imports: [ButtonModule],
  templateUrl: './reports-panel.html',
  styleUrl: '../../globalAdminDesign.scss',
})
export class ReportsPanel {

  constructor(private winningsService: WinningService, private xlService: XlService) { }

  exportIncomeReport() {
    this.winningsService.getTotalIncome().subscribe({
      next: res => {
        this.xlService.exportToExcelNumber(res, 'Income_Report');
      },
    });
  }

  exportWinningsReport() {
    this.winningsService.getAll().subscribe({
      next: res => {
        this.xlService.exportToExcel(res, 'Winnings_Report');
      },
    });
  }
}
