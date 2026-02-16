import { 
  Directive, 
  ComponentRef, 
  ViewContainerRef, 
  OnInit, 
  OnDestroy, 
  HostListener 
} from '@angular/core';
import { NgControl } from '@angular/forms';
import { ValidationMessagesComponent } from '../components/validation-messages/validation-messages';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[formControlName], [formControl]',
  standalone: true
})
export class ValidationErrorDirective implements OnInit, OnDestroy {
  private componentRef: ComponentRef<ValidationMessagesComponent> | null = null;
  private statusSub!: Subscription;

  constructor(
    private control: NgControl,
    private vcr: ViewContainerRef
  ) {}

  ngOnInit() {
    // מאזין לשינויים בסטטוס 
    this.statusSub = this.control.statusChanges!.subscribe(() => {
      this.updateErrorMessage();
    });
  }

  //  - תבדוק ולידציה
  @HostListener('blur')
  onBlur() {
    this.updateErrorMessage();
  }

  private updateErrorMessage() {
    const errors = this.control.errors;

    if (errors && (this.control.touched || this.control.dirty)) {
      if (!this.componentRef) {
        this.componentRef = this.vcr.createComponent(ValidationMessagesComponent);
      }
      this.componentRef.instance.control = this.control.control;
    } else {
      this.removeError();
    }
  }

  private removeError() {
    if (this.componentRef) {
      this.componentRef.destroy();
      this.componentRef = null;
    }
  }

  ngOnDestroy() {
    if (this.statusSub) {
      this.statusSub.unsubscribe();
    }
    this.removeError();
  }
}