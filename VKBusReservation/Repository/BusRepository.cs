using VKBusReservation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VKBusReservation.Repository
{
    public class BusRepository : IBusRepository
    {

        private readonly VKBusReservationDbContext db;
        public BusRepository(VKBusReservationDbContext db)
        {
            this.db = db;
        }


        public Messages CreateBus(Bus bus)
        {
            Messages messages = new Messages();
            var busExist = GetByBusNumber(bus.BusNumber);
            if (busExist == null)
            {
                TimeSpan time = new TimeSpan(0, busExist.TripStartTime.Hour, busExist.TripStartTime.Minute, busExist.TripStartTime.Second);

                //bus.TripStartTime = time;
                db.Buses.Add(bus);
                db.SaveChanges();
                messages.Success = true;
                messages.Message = "Bus succssfully added";
                return messages;
            }
            else
            {
                messages.Success = false;
                messages.Message = "BusNumber already registered.";
                return messages;
            }

        }


        public Messages DeleteBus(int id)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var bus = GetByBusId(id);
            if (bus == null)
            {
                messages.Message = "The Bus Id is not Registered";
                return messages;
            };
            var reserved = db.Reservations.Where(x => x.BusId == id).ToList();
            if (reserved.Count() > 0)
            {
                messages.Message = "Bus already reserved,You must cancel all the reservations to delete the Bus";
                return messages;
            }
            else
            {
                db.Buses.Remove(bus);
                db.SaveChanges();
                messages.Success = true;
                messages.Message = "Bus succssfully deleted";
                return messages;
            }

        }


        public List<Bus> GetAll()
        {
            return db.Buses.ToList();
        }


        public Bus GetByBusId(int busId)
        {
            var busExist = db.Buses.FirstOrDefault(x => x.BusId == busId);
            return busExist;
        }


        public Bus GetByBusNumber(string busNumber)
        {
            var busExist = db.Buses.FirstOrDefault(x => x.BusNumber == busNumber);
            return busExist;
        }

        public List<Bus> GetByFromTo(string from,string to)
        {
            var busFrom = db.Buses.Where(x => x.From == from).ToList();
            var busTo = busFrom.Where(x => x.To == to).ToList();
            return busTo;
        }



        public Messages Update(Bus bus)
        {
            Messages messages = new Messages();
            messages.Success = false;
            var busExist = GetByBusId(bus.BusId);
            var busNumber = GetByBusNumber(bus.BusNumber);
            if (busNumber != null && busExist.BusId != busNumber.BusId)
            {
                messages.Message = "Bus number already exist";
                return messages;
            }
            if (busExist == null)
            {
                messages.Message = "Bus id Not registered.";
                return messages;
            }

            busExist.BusNumber = bus.BusNumber;
            busExist.BusName = bus.BusName;
            busExist.From = bus.From;
            busExist.To = bus.To;
            busExist.TripStartTime = bus.TripStartTime;
            busExist.TripEndTime = bus.TripEndTime;
            busExist.TicketPrice = bus.TicketPrice;
            db.SaveChanges();
            messages.Success = true;
            messages.Message = "Bus succssfully updated";
            return messages;

        }

        public IEnumerable<BusDetails> ReservationDetailsByBusId(int busId)
        {
            var busDetail = (from reservation in db.Reservations

                             join bus in db.Buses on reservation.BusId equals bus.BusId
                             where reservation.BusId == busId
                             select new BusDetails()
                             {
                                 BusName = bus.BusName,
                                 BusNumber = bus.BusNumber,
                                 TotalSeats = bus.TotalSeats,
                                 ReservedSeats = reservation.NumberOfSeats,
                                 ReservedTicketPrice = bus.TicketPrice,
                                 From = bus.From,
                                 To = bus.To,
                                 Reservationdate = reservation.Reservationdate
                             }).ToList();
            return busDetail;
        }
    }

}
