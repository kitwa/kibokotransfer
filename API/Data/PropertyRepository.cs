using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using API.Helpers;
using System;

namespace API.Data
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PropertyRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<PagedList<PropertyDto>> SearchAsync(UserParams userParams, SearchPropertyDto searchPropertyDto)
        {
            var query = _context.Properties
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .Where(x => x.City == searchPropertyDto.City 
                    && x.PropertyType == searchPropertyDto.PropertyType
                    && x.Price >= searchPropertyDto.MinPrice 
                    && x.Price <= searchPropertyDto.MaxPrice
                    && x.BedRooms >= searchPropertyDto.BedRooms 
                    && x.BathRooms >= searchPropertyDto.BathRooms)
            .OrderBy(x => x.Created);

            return await PagedList<PropertyDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync()
        {
            return await _context.Properties
            // .ThenInclude(p => p.Photos)
            .ToListAsync();
        }

        public async Task<PagedList<PropertyDto>> GetPropertiesAsync(UserParams userParams)
        {
            var query = _context.Properties
            .OrderByDescending(p => p.Created)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();
            
            return await PagedList<PropertyDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<PropertyDto>> GetRecentPropertiesAsync(UserParams userParams)
        {
            var query = _context.Properties
            .OrderByDescending(p => p.Created)
            .Take(3)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();
            
            return await PagedList<PropertyDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }
        public async Task<Property> GetPropertyAsync(int id)
        {
            return await _context.Properties
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        }

        public async Task<PropertyDto> GetPropertyDtoAsync(int id)
        {
            return await _context.Properties
            .Where(x => x.Id == id)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
             var save =  await _context.SaveChangesAsync();
             return save >= 0;
        }

        public void Update(Property property)
        {
            _context.Entry(property).State = EntityState.Modified;
        }

        public void Create(Property property)
        {
            if(property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            _context.Properties.Add(property);
        }

    }
}