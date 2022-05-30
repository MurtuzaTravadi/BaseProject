using BaseProject.Filters;
using BaseProject.Models.Common;
using BaseProject.Models.Hotel;
using BaseProject.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseProject.Controllers.Hotels
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(BaseExceptionFilter))]
    [ApiController]
    public class HotelController : BaseController
    {
        public IUnitOfWork _unitOfWork;

        public HotelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET api/<HotelController>/5
        [HttpGet]
        [Route("GetHotelInformationById")]
        public CommonResponse<Hotel> GetHotelInformationById(int id)
        {
            CommonResponse<Hotel> result = new CommonResponse<Hotel>();
            var Hotel = _unitOfWork.Hotels.GetById(id);
            if (Hotel.HotelId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = Hotel;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Record not found";
            }
            return result;
        }


        [HttpGet]
        [Route("SeachByName")]
        public CommonResponse<Hotel> Search(string HotelName)
        {
            CommonResponse<Hotel> result = new CommonResponse<Hotel>();
            var Hotel = _unitOfWork.Hotels.Search(HotelName);
            if (Hotel.HotelId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = Hotel;
            }
            else
            {
                result.StatusCode = 201;
                //TODO make constant message.
                result.Message = "Data not found";
            }
            return result;
        }

        // POST api/<HotelController>
        [HttpPost]
        [Route("RegisterUpdateHotel")]
        public CommonResponse<Hotel> Post([FromBody] Hotel entity)
        {
            CommonResponse<Hotel> result = new CommonResponse<Hotel>();
            var Hotel = _unitOfWork.Hotels.Upsert(entity);
            if (Hotel.HotelId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Sucess";
                result.Result = Hotel;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Internal server error";
            }
            return result;
        }
    }
}
