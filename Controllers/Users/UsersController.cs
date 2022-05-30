using Microsoft.AspNetCore.Mvc;
using BaseProject.Models;
using BaseProject.Models.Users;
using BaseProject.Models.Common;
using BaseProject.Service;
using BaseProject.Filters;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseProject.Controllers.Users
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(BaseExceptionFilter))]
    [ApiController]
    public class UsersController : BaseController
    {
        public IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<UserController>
        [HttpGet]
        [Route("GetUsers")]
        [Authorize]
        public CommonResponse<List<User>> GetUsers()
        {
            CommonResponse<List<User>> users = new CommonResponse<List<User>>();
            users.StatusCode = 200;
            users.Result = _unitOfWork.Users.GetUser();
            return users;
        }

        [HttpPost]
        [Route("Login")]
        public CommonResponse<bool> Login([FromBody]LoginRequest loginRequest)
        {
            
            CommonResponse<bool> result = new CommonResponse<bool>();
            var token = _unitOfWork.Users.Login(loginRequest);
            if (!string.IsNullOrWhiteSpace(token))
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Login Sucess";
                result.Result = token;
            }
            else
            {
                result.StatusCode = 401;
                //TODO make constant message.
                result.Message = "Authentication failed";
            }
            return result;

        }


        // POST api/<UserController>
        [HttpPost]
        [Route("RegisterUser")]
        public CommonResponse<User> RegisterUser([FromBody] User user)
        {
            CommonResponse<User> result = new CommonResponse<User>();
            var User = _unitOfWork.Users.RegisterUser(user);
            if (User.UserId != 0)
            {
                result.StatusCode = 200;
                //TODO make constant message.
                result.Message = "Login Sucess";
                result.Result = User;
            }
            else
            {
                result.StatusCode = 500;
                //TODO make constant message.
                result.Message = "Something went wrong";
            }
            return result;
        }
    }
}
