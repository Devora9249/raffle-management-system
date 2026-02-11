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
  // הדירקטיבה תעבוד על כל שדה שיש לו formControlName או formControl
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
    // מאזין לשינויים בסטטוס (למשל כשהשדה הופך מקין ללא תקין תוך כדי הקלדה)
    this.statusSub = this.control.statusChanges!.subscribe(() => {
      this.updateErrorMessage();
    });
  }

  // האירוע שחיפשת: ברגע שהמשתמש יוצא מהשדה (Blur) - תבדוק ולידציה
  @HostListener('blur')
  onBlur() {
    this.updateErrorMessage();
  }

  private updateErrorMessage() {
    const errors = this.control.errors;

    // התנאי המעודכן: אם יש שגיאה וגם (נגעו בשדה או שינו אותו)
    if (errors && (this.control.touched || this.control.dirty)) {
      if (!this.componentRef) {
        // יוצר את קומפוננטת הודעת השגיאה מתחת לאינפוט
        this.componentRef = this.vcr.createComponent(ValidationMessagesComponent);
      }
      // מעביר לקומפוננטה את ה-control כדי שתשלוף את הטקסט הנכון מהמילון
      this.componentRef.instance.control = this.control.control;
    } else {
      // אם אין שגיאה או שהמשתמש עוד לא נגע בשדה - תסיר את ההודעה
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