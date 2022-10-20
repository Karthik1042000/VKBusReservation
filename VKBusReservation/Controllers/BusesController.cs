using VKBusReservation.Models;
using VKBusReservation.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusReservationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusesController : ControllerBase
    {
        private readonly IBusRepository busRepository;

        public BusesController(IBusRepository _busRepository)
        {
            busRepository = _busRepository;
        }


        [HttpGet("GetBusList")]
        public IActionResult GetBusList()
        {
            var bus = busRepository.GetAll();
            if (bus.Count() == 0)
            {
                throw new Exception("The Bus list is empty");
            }
            return Ok(bus);
        }


        [HttpGet("GetBus/{busid}")]

        public ActionResult GetBus(int busid)
        {
            var bus = busRepository.GetByBusId(busid);
            if (bus == null)
            {
                throw new Exception("The Bus Id is not found");
            }
            return Ok(bus);
        }


        [HttpPost("Add")]

        public IActionResult Add(Bus bus)
        {
            return Ok(busRepository.CreateBus(bus));
        }


        [HttpPut("Update")]
        public ActionResult Update(Bus bus)
        {
            return Ok(busRepository.Update(bus));
        }


        [HttpDelete("RemoveById/{busid}")]

        public IActionResult RemoveById(int busid)
        {
            return Ok(busRepository.DeleteBus(busid));
        }


        [HttpGet("BusReservationsDetailsById/{busid}")]

        public ActionResult BusReservationsDetailsById(int busid)
        {
            var bus = busRepository.GetByBusId(busid);
            if (bus == null)
            {
                throw new Exception("The Bus Id is not Registered");
            }
            var busexist = busRepository.ReservationDetailsByBusId(busid);
            if (busexist.Count() == 0)
            {
                throw new Exception("The Bus Id is not Reserved any seats");
            }
            return Ok(busexist);
        }
    }
}
