import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { LoginDto, RegisterDto, LoginResponseDto, UserResponseDto } from '../models/auth-model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5071/api/auth';

  constructor(private http: HttpClient) {}

  login(dto: LoginDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(`${this.apiUrl}/login`, dto);
  }

  register(dto: RegisterDto): Observable<LoginResponseDto> {
    return this.http.post<LoginResponseDto>(`${this.apiUrl}/register`, dto);
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  getCurrentUser(): Observable<UserResponseDto> {
    // כאן מחזירים דוגמא
    return of({
      id: 1,
      name: 'Yaeli',
      email: 'yaeli@example.com',
      phone: '',
      city: '',
      address: '',
      role: 'Donor' // חייב להיות Admin | Donor | User
    } as UserResponseDto);
  }
}
