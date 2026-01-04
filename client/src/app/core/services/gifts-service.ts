import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { GiftResponseDto, GiftCreateDto, GiftUpdateDto, PriceSort } from '../models/gift-model'

@Injectable({ providedIn: 'root' })
export class GiftsService {

  private readonly baseUrl = 'http://localhost:5071/api/Gift';

  constructor(private http: HttpClient) {}

getAll(sort: PriceSort): Observable<GiftResponseDto[]> {
  return this.http
    .get<GiftResponseDto[]>(this.baseUrl, {
      params: { sort }
    })
}

  getById(id: number): Observable<GiftResponseDto> {
    return this.http
      .get<GiftResponseDto>(`${this.baseUrl}/${id}`)
  }

  create(dto: GiftCreateDto): Observable<GiftResponseDto> {
    return this.http
      .post<GiftResponseDto>(this.baseUrl, dto)
  }

  update(id: number, dto: GiftUpdateDto): Observable<GiftResponseDto> {
    return this.http
      .put<GiftResponseDto>(`${this.baseUrl}/${id}`, dto)
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }


}
