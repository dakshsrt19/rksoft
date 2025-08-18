using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RKSoft.eShop.Api.Models;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Net;

namespace RKSoft.eShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;
        private ApiResponse _apiResponse;
        public StoreController(IStoreService storeService, IMapper mapper)
        {
            _storeService = storeService;
            _mapper = mapper;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("all", Name = "GetAllStores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetAllStores()
        {
            try
            {
                var stores = await _storeService.GetAllStoreAsync();
                _apiResponse.Data = _mapper.Map<List<StoreDTO>>(stores);
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
        [Route("{id:int}", Name = "GetStoreById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetStoreById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var store = await _storeService.GetStoreByIdAsync(store => store.Id == id);
                if (store == null) return NotFound($"The store with id {id} not found");

                _apiResponse.Data = _mapper.Map<StoreDTO>(store);
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
        [Route("add", Name = "CreateStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateStore([FromBody] StoreDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                EStore store = _mapper.Map<EStore>(dto);
                var storeAfterCreation = await _storeService.CreateStoreAsync(store);

                dto.Id = storeAfterCreation.Id;
                _apiResponse.Data = dto;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(GetStoreById), new { id = dto.Id }, _apiResponse);
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
        [Route("update", Name = "UpdatStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdatStore(StoreDTO dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var newRecord = _mapper.Map<EStore>(dto);
                _apiResponse.Data = await _storeService.UpdateStoreAsync(newRecord);
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
        [Route("{id:int}/delete", Name = "DeleteStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> DeleteStore(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var store = await _storeService.GetStoreByIdAsync(store => store.Id == id);
                if (store == null)
                    return BadRequest($"The store with Id {id} not found");
                await _storeService.DeleteStoreAsync(store);
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
