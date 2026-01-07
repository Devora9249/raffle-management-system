import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { LoginDto, RegisterDto, LoginResponseDto, UserResponseDto } from '../models/auth-model';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  _id: string;
  name: string;
  email: string;
  phone?: string;
  city?: string;
  address?: string;
  roles: string[];
}

const roleMap: Record<string, 'User' | 'Admin' | 'Donor'> = {
  User: 'User',
  Admin: 'Admin',
  Donor: 'Donor'
};

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

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }

  getCurrentUser(): Observable<UserResponseDto | null> {
    const token = this.getToken();
    if (!token) return of(null);

    try {
      const decoded = jwtDecode<JwtPayload>(token);
      const user: UserResponseDto = {
        id: Number(decoded._id),
        name: decoded.name,
        email: decoded.email,
        phone: decoded.phone || '',
        city: decoded.city || '',
        address: decoded.address || '',
        role: roleMap[decoded.roles[0]] || 'User'
      };
      return of(user);
    } catch {
      return of(null);
    }
  }

  getCurrentUserId(): Observable<number | null> {
    return new Observable<number | null>(observer => {
      this.getCurrentUser().subscribe(user => {
        observer.next(user ? user.id : null);
        observer.complete();
      });
    });
  }
}
