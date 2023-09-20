using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Features.Transactions.Commands;
using API.Features.Transactions.Queries;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class TransactionsController : BaseApiController
    {
        public readonly IMediator _mediator;
        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactions([FromQuery]UserParams userParams)
        {
            var transactions = await _mediator.Send(new GetTransactions.Query(userParams));

            Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount, transactions.TotalPages);    
            return Ok(transactions);
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        public async Task<ActionResult<TransactionDto>> GetTransaction(int id)
        {
            var transaction = await _mediator.Send(new GetTransaction.Query(id));
            if(transaction != null) {
                return transaction;
            }
            return NotFound();
        }

        [HttpPost("search-transactions")]
        public async Task<ActionResult<TransactionDto>> Search([FromQuery]UserParams userParams,SearchTransactionDto searchTransactionDto)
        {

            var transactions = await _mediator.Send(new SearchTransactions.Query(searchTransactionDto, userParams));

            Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount, transactions.TotalPages);    
            return Ok(transactions);

        }

        // [Authorize(Policy = "RequireAdminAgentRole")]
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> CreateTransaction(TransactionDto transactionDto)
        {
            var transaction = await _mediator.Send(new CreateTransaction.Command(transactionDto));

            if(transaction != null){
                return Ok(transaction);
            }
            return BadRequest("Failed to create transaction");

        }
    }
}