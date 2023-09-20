using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class PropertiesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;
        public PropertiesController(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        [HttpPost("search-properties")]
        public async Task<ActionResult<PropertyDto>> Search([FromQuery]UserParams userParams,SearchPropertyDto searchPropertyDto)
        {

            if(searchPropertyDto.City == null) return BadRequest("Location is required");
            
            if(searchPropertyDto.PropertyType == null) return BadRequest("Property Type is required");

            var properties = await _propertyRepository.SearchAsync(userParams, searchPropertyDto);

            Response.AddPaginationHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);    

            return Ok(properties);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetProperties([FromQuery]UserParams userParams)
        {
            var properties = await _propertyRepository.GetPropertiesAsync(userParams);

            Response.AddPaginationHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);    

            return Ok(properties);
        }

        [HttpGet("recent")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetRecentProperties([FromQuery]UserParams userParams)
        {
            var properties = await _propertyRepository.GetPropertiesAsync(userParams);

            Response.AddPaginationHeader(properties.CurrentPage, properties.PageSize, properties.TotalCount, properties.TotalPages);    

            return Ok(properties);
        }

        [HttpGet("{id}", Name = "GetProperty")]
        public async Task<ActionResult<PropertyDto>> GetProperty(int id)
        {
            var property = await _propertyRepository.GetPropertyDtoAsync(id);
            if(property != null) {
                return property;
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProperty(int id, PropertyUpdateDto propertyUpdateDto)
        {
            var property = await _propertyRepository.GetPropertyAsync(id);

            _mapper.Map(propertyUpdateDto, property);

            _propertyRepository.Update(property);

            if (await _propertyRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update property");
        }

        [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<PropertyDto>> CreateProperty(PropertyDto propertyDto)
        {

            var property = _mapper.Map<Property>(propertyDto);
            
            _propertyRepository.Create(property);

            if (await _propertyRepository.SaveAllAsync()) {
            var propertyDtoRet = _mapper.Map<PropertyDto>(property);

            return propertyDtoRet;

            }

            return BadRequest("Failed to create property");

        }
    }
}