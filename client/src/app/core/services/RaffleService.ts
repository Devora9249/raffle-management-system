import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

export interface RaffleStatusResponse {
  finished: boolean;
}

@Injectable({ providedIn: "root" })
export class RaffleService {
  private readonly baseUrl = "http://localhost:5071/api/raffle"; // ✅ תתאימי לכתובת שלך

  constructor(private http: HttpClient) {}

  getStatus(): Observable<RaffleStatusResponse> {
    return this.http.get<RaffleStatusResponse>(`${this.baseUrl}/status`);
  }
}
