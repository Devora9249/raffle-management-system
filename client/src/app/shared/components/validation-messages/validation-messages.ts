import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common'; // הוספנו את זה
import { AbstractControl } from '@angular/forms';
import { getValidatorErrorMessage } from '../validation-messages';

@Component({
  selector: 'app-validation-messages',
  standalone: true, // וודאי שזה מופיע
  imports: [CommonModule], // וודאי שזה מופיע
  template: `
    <div *ngIf="errorMessage" class="error-text">
      <small style="color: grey;">{{ errorMessage }}</small>
    </div>
  `
})
export class ValidationMessagesComponent {
  @Input() control!: AbstractControl | null;

  get errorMessage() {
    if (this.control && this.control.errors && (this.control.dirty || this.control.touched)) {
      for (const propertyName in this.control.errors) {
        if (this.control.errors.hasOwnProperty(propertyName)) {
          return getValidatorErrorMessage(propertyName, this.control.errors[propertyName]);
        }
      }
    }
    return null;
  }
}