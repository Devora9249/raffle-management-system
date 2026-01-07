import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DonorListItem, DonorDashboardResponse } from '../models/donor-model';

@Injectable({
  providedIn: 'root'
})
export class DonorService {

  private readonly apiUrl = 'http://localhost:5000/api/donor';

  constructor(private http: HttpClient) {}

  /**
   * GET /api/donor?search=&city=
   * Admin בלבד
   */
  getDonors(search?: string, city?: string): Observable<DonorListItem[]> {
    let params = new HttpParams();

    if (search) {
      params = params.set('search', search);
    }

    if (city) {
      params = params.set('city', city);
    }

    return this.http.get<DonorListItem[]>(this.apiUrl, { params });
  }

  /**
   * PATCH /api/donor/role/{userId}?role=Donor
   * Admin בלבד
   */
  setUserRole(userId: number, role: string): Observable<void> {
    const params = new HttpParams().set('role', role);

    return this.http.patch<void>(
      `${this.apiUrl}/role/${userId}`,
      null,
      { params }
    );
  }

  /**
   * GET /api/donor/{donorId}/dashboard
   * דשבורד לתורם
   */
  getDonorDashboard(donorId: number): Observable<DonorDashboardResponse> {
    return this.http.get<DonorDashboardResponse>(
      `${this.apiUrl}/${donorId}/dashboard`
    );
  }
}
