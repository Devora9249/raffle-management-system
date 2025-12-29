// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginDto, RegisterDto, LoginResponseDto } from '../models/auth.models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/auth';

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
}
