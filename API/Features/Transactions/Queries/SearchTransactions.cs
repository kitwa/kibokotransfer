using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Transactions.Queries
{
    public abstract class SearchTransactions
    {
        public class Query : IRequest<PagedList<SearchTransactionDto>> 
        {
            public Query(SearchTransactionDto searchTransactionDto, UserParams userParams)
            {
                UserParams = userParams;
                SearchTransactionDto = searchTransactionDto;
            }
            public UserParams UserParams {get; set;}
            public SearchTransactionDto SearchTransactionDto { get; set; }
        }

        public class Handler: IRequestHandler<Query, PagedList<SearchTransactionDto>> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PagedList<SearchTransactionDto>> Handle(Query query, CancellationToken cancellationToken)
            {
                var transactions = _context.Transactions
                .ProjectTo<SearchTransactionDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .Where(x => x.SenderCityId == query.SearchTransactionDto.SenderCityId 
                        || x.RecipientCityId == query.SearchTransactionDto.RecipientCityId
                        || x.StatusId == query.SearchTransactionDto.StatusId 
                        || x.RecipientName == query.SearchTransactionDto.RecipientName
                        || x.Code == query.SearchTransactionDto.Code)
                .OrderBy(x => x.Created);
                            
                return await PagedList<SearchTransactionDto>.CreateAsync(transactions, query.UserParams.PageNumber, query.UserParams.PageSize);
            }
        }
        
    }
}