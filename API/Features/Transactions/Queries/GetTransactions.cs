using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Transactions.Queries
{
    public abstract class GetTransactions
    {
        public class Query : IRequest<PagedList<TransactionDto>> 
        {
            public Query(UserParams userParams)
            {
                UserParams = userParams;
            }
            public UserParams UserParams {get; set;}
        }

        public class Handler: IRequestHandler<Query, PagedList<TransactionDto>> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<TransactionDto>> Handle(Query query, CancellationToken cancellationToken)
            {
                var transactions = _context.Transactions.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .OrderBy(x => x.Id);
                            
                return await PagedList<TransactionDto>.CreateAsync(transactions, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
        
    }
}