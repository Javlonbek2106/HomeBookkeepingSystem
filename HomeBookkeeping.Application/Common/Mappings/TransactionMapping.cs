using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.CreateTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.DeleteTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Commands.UpdateTransaction;
using HomeBookkeeping.Application.UseCases.Transactions.Response;
using HomeBookkeeping.Domain.Entities;

namespace HomeBookkeeping.Application.Common.Mappings
{
    public class TransactionMapping : Profile
    {
        public TransactionMapping()
        {
            TransactionMappings();
        }
        private void TransactionMappings()
        {
            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<UpdateTransactionCommand, Transaction>();
            CreateMap<DeleteTransactionCommand, Transaction>();
            CreateMap<Transaction, TransactionResponse>();
        }
    }
}
