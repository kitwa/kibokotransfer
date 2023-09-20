using API.Data;
using API.DTOs;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Transactions.Queries
{
    public abstract class GetTransaction
    {
        public class Query : IRequest<TransactionDto> 
        {
            public Query (int transactionId) 
            {
                TransactionId = transactionId;
            }

            public int TransactionId {get; set;}
        }

        public class Handler: IRequestHandler<Query, TransactionDto> 
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TransactionDto> Handle(Query query, CancellationToken cancellationToken)
            {
                var transaction = await _context.Transactions.Where(x => x.Id == query.TransactionId).SingleOrDefaultAsync();
                            
                return _mapper.Map<TransactionDto>(transaction);
            }
        }
        
    }
}