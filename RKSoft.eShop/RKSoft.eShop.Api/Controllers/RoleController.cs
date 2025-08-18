using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RKSoft.eShop.Api.Models;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.App.Services;
using RKSoft.eShop.Domain.Entities;
using System.Data;
using System.Net;

namespace RKSoft.eShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private ApiResponse _apiResponse;
        public RoleController(IMapper mapper, IRoleService roleService)
        {
            _mapper = mapper;
            _roleService = roleService;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("all", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoleAsync();
                var res = _mapper.Map<List<RoleDto>>(roles);
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
        [Route("{id:int}", Name = "GetRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetRoleById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var role = await _roleService.GetRoleByIdAsync(Role => Role.Id == id);
                if (role == null) return NotFound($"The Role with id {id} not found");
                var res = _mapper.Map<RoleDto>(role);
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
        [Route("add", Name = "CreatRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateRole([FromBody] RoleDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest();
                }

                Role role = _mapper.Map<Role>(dto);
                role.IsDeleted = false;
                role.CreatedAt = DateTime.UtcNow;
                role.UpdatedAt = DateTime.UtcNow;

                var result = await _roleService.CreateRoleAsync(role);
                dto.Id = result.Id;
                _apiResponse.Data = dto;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return CreatedAtRoute(nameof(GetRoleById), new { id = dto.Id }, _apiResponse);
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
        [Route("update", Name = "UpdatRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdatRole(RoleDto dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var newRecord = _mapper.Map<Role>(dto);
                var res = await _roleService.UpdatRoleAsync(newRecord);
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
        [Route("{id:int}/delete", Name = "DeletRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> DeletRole(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var Role = await _roleService.GetRoleByIdAsync(Role => Role.Id == id);
                if (Role == null)
                    return BadRequest($"The Role with Id {id} not found");
                await _roleService.DeletRoleAsync(Role);
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
