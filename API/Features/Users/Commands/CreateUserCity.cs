using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Transactions.Commands
{
    public class CreateUserCity
    {
        public class Command : IRequest<CityDto> {
            public Command(CityDto ciransactionDto)
            {
                CityDto = ciransactionDto;
            }
            public CityDto CityDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, CityDto>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<CityDto> Handle(Command command, CancellationToken cancellationToken)
            {
                
                if(command.CityDto == null)
                {
                    throw new ArgumentNullException(nameof(command.CityDto));
                }

                if (await CityExists(command.CityDto.Name)) 
                                {
                    throw new Exception("City Already Added");
                }

                var city = _mapper.Map<City>(command.CityDto);
                _context.Cities.Add(city);

                await _context.SaveChangesAsync();
                           
                var transactionDtoRet = _mapper.Map<CityDto>(city);

                return transactionDtoRet;
            }

            private async Task<bool> CityExists(string cityName)
            {
                return await _context.Cities.AnyAsync(x => x.Name.ToLower() == cityName.ToLower());
            }
        }
    }
}