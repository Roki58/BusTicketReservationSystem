import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingService } from '../../services/booking.service';
import { Seat, SeatPlan } from '../../models/seat-plan.model';
import { BookSeatInput } from '../../models/booking.model';

@Component({
  selector: 'app-seat-selection',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './seat-selection.component.html',
  styleUrls: ['./seat-selection.component.css']
})
export class SeatSelectionComponent implements OnInit {
  scheduleId: string = '';
  busName: string = '';
  companyName: string = '';
  price: number = 0;
  
  seatPlan?: SeatPlan;
  selectedSeat?: Seat;
  
  passengerName: string = '';
  mobileNumber: string = '';
  boardingPoint: string = '';
  droppingPoint: string = '';
  
  loading: boolean = false;
  bookingInProgress: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private bookingService: BookingService
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.scheduleId = params['scheduleId'];
      this.busName = params['busName'] || '';
      this.companyName = params['companyName'] || '';
      this.price = +params['price'] || 0;
      
      if (this.scheduleId) {
        this.loadSeatPlan();
      }
    });
  }

  loadSeatPlan() {
    this.loading = true;
    this.bookingService.getSeatPlan(this.scheduleId).subscribe({
      next: (data) => {
        this.seatPlan = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error:', error);
        alert('Failed to load seat plan. Check if backend is running.');
        this.loading = false;
        this.router.navigate(['/']);
      }
    });
  }

  selectSeat(seat: Seat) {
    if (seat.status !== 'Available') return;
    
    if (this.selectedSeat) {
      this.selectedSeat.selected = false;
    }
    
    seat.selected = true;
    this.selectedSeat = seat;
  }

  getSeatClass(seat: Seat): string {
    if (seat.selected) return 'seat selected';
    if (seat.status === 'Booked' || seat.status === 'Sold') return 'seat booked';
    return 'seat available';
  }

  bookSeat() {
    if (!this.selectedSeat) {
      alert('Please select a seat');
      return;
    }

    if (!this.passengerName || !this.mobileNumber || !this.boardingPoint || !this.droppingPoint) {
      alert('Please fill all passenger details');
      return;
    }

    const input: BookSeatInput = {
      busScheduleId: this.scheduleId,
      seatId: this.selectedSeat.seatId,
      passengerName: this.passengerName,
      mobileNumber: this.mobileNumber,
      boardingPoint: this.boardingPoint,
      droppingPoint: this.droppingPoint
    };

    this.bookingInProgress = true;
    this.bookingService.bookSeat(input).subscribe({
      next: (result) => {
        if (result.success) {
          alert(`✅ ${result.message}\n\nSeat: ${result.seatNumber}\nTicket ID: ${result.ticketId}\nPrice: ৳${this.price}`);
          this.router.navigate(['/']);
        } else {
          alert(`❌ ${result.message}`);
        }
        this.bookingInProgress = false;
      },
      error: (error) => {
        console.error('Error:', error);
        alert('Booking failed. Please try again.');
        this.bookingInProgress = false;
      }
    });
  }

  goBack() {
    this.router.navigate(['/']);
  }
}