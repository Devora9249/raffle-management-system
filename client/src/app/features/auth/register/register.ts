import { Component, EventEmitter, Output, SimpleChanges, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RegisterDto } from '../../../core/dto/register-dto';
import { AuthService } from '../../../core/services/register-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  templateUrl: './register.html',
  styleUrls: ['./register.scss'],
})
export class RegisterComponent {
  constructor(private authService: AuthService, private router: Router) {}

  // אובייקט למילוי טופס הרישום
  model: RegisterDto = {
    name: '',
    email: '',
    password: '',
    phone: '',
    city: '',
    address: ''
  };

  // אירוע למי שרוצה לקבל עדכון אחרי רישום
  @Output() registered: EventEmitter<RegisterDto> = new EventEmitter<RegisterDto>();

  // קריאה ל-API להרשמה
  save() {
    this.authService.register(this.model).subscribe({
      next: (res) => {
        alert('Registration successful!');
        this.registered.emit(this.model );
      },
      error: (err) => {
        console.error('Registration failed', err);
        alert('Registration failed. Check your input.');
      }
    });
  }

  // איפוס הטופס
  undoChanges() {
    this.model = {
      name: '',
      email: '',
      password: '',
      phone: '',
      city: '',
      address: ''
    };
  }

  // אם תצטרכי לבצע משהו כשקלט משתנה
  ngOnChanges(changes: SimpleChanges) {
    // כאן אפשר לבדוק שינויים ב-inputs אם יהיו בעתיד
  }
}
