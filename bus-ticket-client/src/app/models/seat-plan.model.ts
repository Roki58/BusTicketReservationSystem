export interface SeatPlan {
  busScheduleId: string;
  seats: Seat[];
}

export interface Seat {
  seatId: string;
  seatNumber: string;
  rowNumber: number;
  columnNumber: number;
  status: string;
  selected?: boolean;
}