import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { BusService } from '../../services/bus.service';
import { AvailableBus } from '../../models/available-bus.model';

@Component({
  selector: 'app-bus-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './bus-search.component.html',
  styleUrls: ['./bus-search.component.css']
})
export class BusSearchComponent {
  from: string = '';
  to: string = '';
  journeyDate: string = '';
  buses: AvailableBus[] = [];
  loading: boolean = false;
  searched: boolean = false;

  cities: string[] = ['Dhaka','Rangpur', 'Chittagong', 'Sylhet', 'Rajshahi', 'Khulna', 'Barishal'];

  constructor(private busService: BusService, private router: Router) {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    this.journeyDate = tomorrow.toISOString().split('T')[0];
  }

  searchBuses() {
    if (!this.from || !this.to || !this.journeyDate) {
      alert('Please fill all fields');
      return;
    }

    if (this.from === this.to) {
      alert('From and To cities cannot be same');
      return;
    }

    this.loading = true;
    this.searched = true;

    this.busService.searchBuses(this.from, this.to, this.journeyDate).subscribe({
      next: (data) => {
        this.buses = data;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error:', error);
        alert('Failed to search buses. Make sure backend is running on https://localhost:7001');
        this.loading = false;
      }
    });
  }

  viewSeats(bus: AvailableBus) {
    this.router.navigate(['/seat-selection'], {
      queryParams: { 
        scheduleId: bus.busScheduleId,
        busName: bus.busName,
        companyName: bus.companyName,
        price: bus.price
      }
    });
  }
}