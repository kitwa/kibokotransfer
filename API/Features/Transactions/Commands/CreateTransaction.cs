using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using MediatR;

namespace API.Features.Transactions.Commands
{
    public class CreateTransaction
    {
        public class Command : IRequest<TransactionDto> {
            public Command(TransactionDto transactionDto)
            {
                TransactionDto = transactionDto;
            }
            public TransactionDto TransactionDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, TransactionDto>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public  Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<TransactionDto> Handle(Command command, CancellationToken cancellationToken)
            {
                
                if(command.TransactionDto == null)
                {
                    throw new ArgumentNullException(nameof(command.TransactionDto));
                }

                var transaction = _mapper.Map<Transaction>(command.TransactionDto);
                _context.Transactions.Add(transaction);

                await _context.SaveChangesAsync();
                           
                var transactionDtoRet = _mapper.Map<TransactionDto>(transaction);

                return transactionDtoRet;
            }
        }
    }
}