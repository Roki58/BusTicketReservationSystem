export interface BookSeatInput {
  busScheduleId: string;
  seatId: string;
  passengerName: string;
  mobileNumber: string;
  boardingPoint: string;
  droppingPoint: string;
}

export interface BookSeatResult {
  success: boolean;
  message: string;
  ticketId?: string;
  seatNumber?: string;
}