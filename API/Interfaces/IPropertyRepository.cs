using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IPropertyRepository
    {
        Task<PagedList<PropertyDto>> SearchAsync(UserParams userParams, SearchPropertyDto searchPropertyDto);
        void Update(Property property);
        void Create(Property property);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Property>> GetPropertiesAsync();
        Task<PagedList<PropertyDto>> GetPropertiesAsync(UserParams userParams);
        Task<PagedList<PropertyDto>> GetRecentPropertiesAsync(UserParams userParams);
        Task<PropertyDto> GetPropertyDtoAsync(int id);
        Task<Property> GetPropertyAsync(int id);


    }
}