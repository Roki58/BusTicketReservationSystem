import { Routes } from '@angular/router';
import { BusSearchComponent } from './components/bus-search/bus-search.component';
import { SeatSelectionComponent } from './components/seat-selection/seat-selection.component';

export const routes: Routes = [
  { path: '', component: BusSearchComponent },
  { path: 'seat-selection', component: SeatSelectionComponent },
  { path: '**', redirectTo: '' }
];