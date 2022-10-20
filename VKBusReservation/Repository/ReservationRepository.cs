using VKBusReservation.Models;
using VKBusReservation.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace VKBusReservation.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly VKBusReservationDbContext db;
        public ReservationRepository(VKBusReservationDbContext db)
        {
            this.db = db;
        }


        public Messages BookTicket(Reservation reservation)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var busExist = GetBus(reservation.BusId);
            if (busExist == null)
            {
                messages.Message = "Bus id not available";
                return messages;
            }
            var customerExist = GetCustomer(reservation.CustomerId);
            if (customerExist == null)
            {
                messages.Message = "Customer id not available";
                return messages;
            }
            DateTime date = DateTime.Now;
            TimeSpan time = new TimeSpan(0, busExist.TripStartTime.Hour, busExist.TripStartTime.Minute, busExist.TripStartTime.Second);
            var ticketDate = date.Date + time;

            if (reservation.ReservationTime < ticketDate)
            {
                var busList = db.Reservations.Where(x => x.BusId == reservation.BusId).ToList();
                var timeExist = busList.LastOrDefault(x => x.Reservationdate == reservation.Reservationdate);
                if (timeExist == null)
                {
                    var busSeats = busExist.TotalSeats;
                    var availableSeats = busSeats - reservation.NumberOfSeats;
                    if (availableSeats < 0)
                    {
                        messages.Message = "Reserved seat is greater than the Bus Total seats";
                        return messages;
                    }
                    return SaveReservation(reservation, reservation.NumberOfSeats, availableSeats);
                }
                if (timeExist != null)
                {
                    var getAvailable = timeExist.AvailableSeats;
                    var newAvailableSeat = getAvailable - reservation.NumberOfSeats;
                    if (newAvailableSeat < 0)
                    {
                        messages.Message = "Reserved seat is greater than the Bus Total seats";
                        return messages;
                    }
                    return SaveReservation(reservation, (timeExist.ReservedSeats + reservation.NumberOfSeats), newAvailableSeat);
                }

            }
            if (reservation.Reservationdate < ticketDate)
            {
                messages.Message = "The Bus is already in travel";
                return messages;
            }
            var newBusList = db.Reservations.Where(x => x.BusId == reservation.BusId).ToList();
            var newTimeExist = newBusList.LastOrDefault(x => x.Reservationdate == reservation.Reservationdate);
            if (newTimeExist == null)
            {
                var busSeats = busExist.TotalSeats;
                var availableSeats = busSeats - reservation.NumberOfSeats;
                if (availableSeats < 0)
                {
                    messages.Message = "Reserved seat is greater than the Bus Total seats";
                    return messages;
                }
                return SaveReservation(reservation, reservation.NumberOfSeats, availableSeats);
            }
            if (newTimeExist != null)
            {
                var getAvailable = newTimeExist.AvailableSeats;
                var newAvailableSeat = getAvailable - reservation.NumberOfSeats;
                if (newAvailableSeat < 0)
                {
                    messages.Message = "Reserved seat is greater than the Bus Total seats";
                    return messages;
                }
                return SaveReservation(reservation, (newTimeExist.ReservedSeats + reservation.NumberOfSeats), newAvailableSeat);
            }
            return messages;
        }



        public Messages CancelTicket(int reservationId)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var reserved = ReservationDetailsById(reservationId);
            if (reserved == null)
            {
                messages.Message = "Reservation Id is not registered";
                return messages;
            }
            var busExist = GetBus(reserved.BusId);
            var time = reserved.Reservationdate;
            TimeSpan timing = new TimeSpan(0, busExist.TripStartTime.Hour, busExist.TripStartTime.Minute, busExist.TripStartTime.Second);
            var ticketDate = time.Date + timing;
            var dateTime = DateTime.Now.Date + timing;
            if (dateTime > ticketDate && time < ticketDate)
            {
                messages.Message = "The Bus is already in travel so ticket cannot Cancel";
                return messages;
            }
            db.Reservations.Remove(reserved);
            db.SaveChanges();
            messages.Success = true;
            messages.Message = "Ticket Canceled Successfully";
            return messages;
        }


        public List<Reservation> GetAll()
        {
            return db.Reservations.ToList();
        }
        


        public Messages UpdateTicket(Reservation reservation)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var exist = ReservationDetailsById(reservation.ReservationId);
            if (exist == null)
            {
                messages.Message = "Reservation Id is not registered";
                return messages;
            }
            var busExist = GetBus(reservation.BusId);
            if (busExist == null)
            {
                messages.Message = "Bus id not available";
                return messages;
            }
            var customerExist = GetCustomer(reservation.CustomerId);
            if (customerExist == null)
            {
                messages.Message = "Customer id not available";
                return messages;
            }
            var time = reservation.Reservationdate;
            TimeSpan timing = new TimeSpan(0, busExist.TripStartTime.Hour, busExist.TripStartTime.Minute, busExist.TripStartTime.Second);
            var ticketDate = time.Date + timing;
            var dateTime = DateTime.Now.Date + timing;
            if (dateTime > ticketDate && time < ticketDate)
            {
                messages.Message = "The Bus is not available cannot Update";
                return messages;
            }
            var lastBusList = db.Reservations.Where(x => x.BusId == reservation.BusId).ToList();
            var lastBus = lastBusList.LastOrDefault(x => x.Reservationdate == reservation.Reservationdate);
            if (lastBus == null)
            {
                exist.AvailableSeats = busExist.TotalSeats - reservation.NumberOfSeats;
                if (exist.AvailableSeats < 0)
                {
                    messages.Message = "Reserved seat is greater than the Bus Total seats";
                    return messages;
                }
                exist.ReservedSeats = reservation.ReservedSeats;
                exist = SetReservationValue(reservation, exist);
                db.SaveChanges();
                messages.Success = true;
                messages.Message = "Ticket succssfully updated";
                return messages;

            }
            var lastBusAvailable = lastBus.AvailableSeats;
            var lastBusReserved = lastBus.ReservedSeats;
            var newAvailable = lastBusAvailable - reservation.NumberOfSeats;
            if (newAvailable < 0)
            {
                messages.Message = "Reserved seat is greater than the Bus Total seats";
                return messages;
            }
            var newreserved = lastBusReserved + reservation.NumberOfSeats;
            exist = SetReservationValue(reservation, exist);
            exist.AvailableSeats = newAvailable;
            exist.ReservedSeats = newreserved;
            messages.Success = true;
            messages.Message = "Ticket succssfully updated";
            return messages;
        }


        public Reservation ReservationDetailsById(int id)
        {
            return db.Reservations.FirstOrDefault(x => x.ReservationId == id);
        }

        public Bus GetBus(int id)
        {
            return db.Buses.FirstOrDefault(x => x.BusId == id);
        }

        public Customer GetCustomer(int id)
        {
            return db.Customers.FirstOrDefault(x => x.CustomerId == id);
        }

        public IEnumerable<CustomerDetails> ReservationDetailsByCustomerId(int customerId)
        {
            var customerDetail = (from reservation in db.Reservations
                                  join customer in db.Customers on reservation.CustomerId equals customer.CustomerId
                                  join bus in db.Buses on reservation.BusId equals bus.BusId
                                  where reservation.CustomerId == customerId
                                  select new CustomerDetails()
                                  {
                                      BusName = bus.BusName,
                                      BusNumber = bus.BusNumber,
                                      Reservationdate = reservation.Reservationdate,
                                      CustomerName = customer.CustomerName,
                                      ReservedSeats = reservation.NumberOfSeats,

                                  }).ToList();



            return customerDetail;
        }

        public IEnumerable<AddReservationDTO> ReserveList()
        {
            var reserveList = (from reservation in db.Reservations
                                  join customer in db.Customers on reservation.CustomerId equals customer.CustomerId
                                  join bus in db.Buses on reservation.BusId equals bus.BusId
                                  
                                  select new AddReservationDTO()
                                  {
                                      ReservationId = reservation.ReservationId,
                                      CustomerName = customer.CustomerName,
                                      BusNumber=bus.BusNumber,
                                      NumberOfSeats = reservation.NumberOfSeats,
                                      
                                      ReservationTime = reservation.ReservationTime,
                                      Reservationdate=reservation.Reservationdate,  


                                  }).ToList();



            return reserveList;
        }

        #region Private Methods
        private Reservation SetReservationValue(Reservation newReservation, Reservation existReservation)
        {
            existReservation.Reservationdate = newReservation.Reservationdate;
            existReservation.BusId = newReservation.BusId;
            existReservation.NumberOfSeats = newReservation.NumberOfSeats;
            existReservation.CustomerId = newReservation.CustomerId;
            return existReservation;
        }

        private Messages SaveReservation(Reservation reservation, int? reservedSeats, int? availableSeats)
        {
            Messages messages = new Messages();
            reservation.ReservedSeats = reservedSeats;
            reservation.AvailableSeats = availableSeats;
            db.Reservations.Add(reservation);
            db.SaveChanges();
            messages.Success = true;
            messages.Message = "Ticket successfully reserved";
            return messages;
        }
        #endregion
    }
}

