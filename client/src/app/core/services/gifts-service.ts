import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Gift } from '../models/gift.model';
import { GiftResponseDto } from '../dto/gift/gift-response.dto';
import { GiftCreateDto } from '../dto/gift/gift-create.dto';
import { GiftUpdateDto } from '../dto/gift/gift-update.dto';

@Injectable({ providedIn: 'root' })
export class GiftsService {

  private readonly baseUrl = '/api/Gift';

  constructor(private http: HttpClient) {}

  getAll(sort?: string): Observable<Gift[]> {
    return this.http
      .get<GiftResponseDto[]>(this.baseUrl, { params: { sort: sort ?? '' } })
      .pipe(map(dtos => dtos.map(dto => this.mapToModel(dto))));
  }

  getById(id: number): Observable<Gift> {
    return this.http
      .get<GiftResponseDto>(`${this.baseUrl}/${id}`)
      .pipe(map(dto => this.mapToModel(dto)));
  }

  create(dto: GiftCreateDto): Observable<Gift> {
    return this.http
      .post<GiftResponseDto>(this.baseUrl, dto)
      .pipe(map(dto => this.mapToModel(dto)));
  }

  update(id: number, dto: GiftUpdateDto): Observable<Gift> {
    return this.http
      .put<GiftResponseDto>(`${this.baseUrl}/${id}`, dto)
      .pipe(map(dto => this.mapToModel(dto)));
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  private mapToModel(dto: GiftResponseDto): Gift {
    return {
      id: dto.id,
      description: dto.description,
      price: dto.price,
      categoryId: dto.categoryId,
      donorId: dto.donorId,
      displayPrice: `${dto.price} ₪`
    };
  }
}
