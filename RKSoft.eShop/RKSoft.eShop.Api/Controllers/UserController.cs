using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RKSoft.eShop.Api.Models;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace RKSoft.eShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     // Applies to all actions unless overridden
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _UserService;
        private ApiResponse _apiResponse;
        public UserController(IMapper mapper, IUserService UserService)
        {
            _mapper = mapper;
            _UserService = UserService;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("all", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetAllUsers()
        {
            try
            {
                var Users = await _UserService.GetAllUserAsync();
                var res = _mapper.Map<List<UserDto>>(Users);
                _apiResponse.Data = res;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors = new List<string> { ex.Message };
                return StatusCode((int)_apiResponse.StatusCode, _apiResponse);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetUserById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var User = await _UserService.GetUserByIdAsync(User => User.Id == id);
                if (User == null) return NotFound($"The User with id {id} not found");
                var res = _mapper.Map<UserDto>(User);
                _apiResponse.Data = res;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors = new List<string> { ex.Message };
                return StatusCode((int)_apiResponse.StatusCode, _apiResponse);
            }
        }

        [HttpPost]
        [Route("add", Name = "CreatUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] UserDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }

                User User = _mapper.Map<User>(dto);
                User.IsDeleted = false;
                User.CreatedAt = DateTime.UtcNow;
                User.UpdatedAt = DateTime.UtcNow;

                var result = await _UserService.CreateUserAsync(User);
                dto.Id = result.Id;
                _apiResponse.Data = dto;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return CreatedAtRoute(nameof(GetUserById), new { id = dto.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors = new List<string> { ex.Message };
                return StatusCode((int)_apiResponse.StatusCode, _apiResponse);
            }
        }

        [HttpPut]
        [Route("update", Name = "UpdatUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdatUser(UserDto dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var newRecord = _mapper.Map<User>(dto);
                var res = await _UserService.UpdateUserAsync(newRecord);
                _apiResponse.Data = res;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors = new List<string> { ex.Message };
                return StatusCode((int)_apiResponse.StatusCode, _apiResponse);
            }
        }

        [HttpDelete]
        [Route("{id:int}/delete", Name = "DeletUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> DeletUser(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var User = await _UserService.GetUserByIdAsync(User => User.Id == id);
                if (User == null)
                    return BadRequest($"The User with Id {id} not found");
                await _UserService.DeleteUserAsync(User);
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.Data = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Errors = new List<string> { ex.Message };
                return StatusCode((int)_apiResponse.StatusCode, _apiResponse);
            }
        }
    }
}
