import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';

@Injectable({
    providedIn: 'root'
})
export class XlService {

    exportToExcel(data: any[], fileName: string) {
        const worksheet = XLSX.utils.json_to_sheet(data);
        const workbook = {
            Sheets: { Sheet1: worksheet },
            SheetNames: ['Sheet1']
        };

        const buffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const blob = new Blob([buffer], { type: 'application/octet-stream' });

        saveAs(blob, `${fileName}.xlsx`);
    }

    exportToExcelNumber(value: number, fileName: string) {
        const worksheet = XLSX.utils.json_to_sheet([
            { 'Total Income': value }
        ]);

        const workbook = {
            Sheets: { Sheet1: worksheet },
            SheetNames: ['Sheet1']
        };

        const buffer = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
        const blob = new Blob([buffer], { type: 'application/octet-stream' });

        saveAs(blob, `${fileName}.xlsx`);
    }

}
