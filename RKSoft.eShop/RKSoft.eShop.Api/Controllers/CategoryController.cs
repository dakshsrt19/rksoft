using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RKSoft.eShop.Api.Models;
using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Net;

namespace RKSoft.eShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private ApiResponse _apiResponse;
        public CategoryController(ICategoryService CategoryService, IMapper mapper)
        {
            _categoryService = CategoryService;
            _mapper = mapper;
            _apiResponse = new();
        }

        [HttpGet]
        [Route("all", Name = "GetAllCategorys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ApiResponse>>> GetAllCategorys()
        {
            try
            {
                var Categorys = await _categoryService.GetAllCategoryAsync();
                _apiResponse.Data = _mapper.Map<List<CategoryDto>>(Categorys);
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
        [Route("{id:int}", Name = "GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var Category = await _categoryService.GetCategoryByIdAsync(Category => Category.Id == id);
                if (Category == null) return NotFound($"The Category with id {id} not found");

                _apiResponse.Data = _mapper.Map<CategoryDto>(Category);
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
        [Route("add", Name = "CreateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateCategory([FromBody] CategoryDto dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                Category _category = _mapper.Map<Category>(dto);
                var CategoryAfterCreation = await _categoryService.CreateCategoryAsync(_category);

                dto.Id = CategoryAfterCreation.Id;
                _apiResponse.Data = dto;
                _apiResponse.Success = true;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(GetCategoryById), new { id = dto.Id }, _apiResponse);
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
        [Route("update", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdateCategory(CategoryDto dto)
        {
            try
            {
                if (dto == null) return BadRequest();

                var newRecord = _mapper.Map<Category>(dto);
                _apiResponse.Data = await _categoryService.UpdateCategoryAsync(newRecord);
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
        [Route("{id:int}/delete", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }
                var Category = await _categoryService.GetCategoryByIdAsync(Category => Category.Id == id);
                if (Category == null)
                    return BadRequest($"The Category with Id {id} not found");
                await _categoryService.DeleteCategoryAsync(Category);
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
