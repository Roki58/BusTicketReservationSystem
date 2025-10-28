import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SeatPlan } from '../models/seat-plan.model';
import { BookSeatInput, BookSeatResult } from '../models/booking.model';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  private apiUrl = 'https://localhost:7032/api/Booking'; // আপনার backend port

  constructor(private http: HttpClient) { }

  getSeatPlan(busScheduleId: string): Observable<SeatPlan> {
    return this.http.get<SeatPlan>(`${this.apiUrl}/seat-plan/${busScheduleId}`);
  }

  bookSeat(input: BookSeatInput): Observable<BookSeatResult> {
    return this.http.post<BookSeatResult>(`${this.apiUrl}/book-seat`, input);
  }
}