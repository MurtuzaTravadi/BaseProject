using BaseProject.Filters;
using BaseProject.Models.Booking;
using BaseProject.Models.Common;
using BaseProject.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseProject.Controllers.Bookings
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(BaseExceptionFilter))]
    [ApiController]
    public class BookingsController : BaseController
    {

        public IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("MakeBooking")]
        public CommonResponse<Booking> MakeBooking(Booking entity)
        {
            CommonResponse<Booking> result = new CommonResponse<Booking>();
            var booking = _unitOfWork.Bookings.MakeBooking(entity);
            if (booking.BookingId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = booking;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Internal server error";
            }
            return result;
        }

        [HttpDelete]
        [Route("CancelBooking")]
        public CommonResponse<Booking> CancelBooking(int BookingId)
        {
            CommonResponse<Booking> result = new CommonResponse<Booking>();
            var IsSuccess = _unitOfWork.Bookings.CancelBooking(BookingId);
            if (IsSuccess)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = IsSuccess;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Internal server error";
            }
            return result;
        }

        [HttpGet]
        [Route("ViewBooking")]
        public CommonResponse<Booking> ViewBooking(int BookingId)
        {
            CommonResponse<Booking> result = new CommonResponse<Booking>();
            var bookings = _unitOfWork.Bookings.GetById(BookingId);
            if (bookings.BookingId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = bookings;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Record not found";
            }
            return result;
        }
    }
}
