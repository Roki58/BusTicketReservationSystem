import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AvailableBus } from '../models/available-bus.model';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  private apiUrl = 'https://localhost:7032/api'; // আপনার backend port

  constructor(private http: HttpClient) { }

  searchBuses(from: string, to: string, journeyDate: string): Observable<AvailableBus[]> {
    const params = new HttpParams()
      .set('from', from)
      .set('to', to)
      .set('journeyDate', journeyDate);

    return this.http.get<AvailableBus[]>(`${this.apiUrl}/BusSearch`, { params });
  }
}