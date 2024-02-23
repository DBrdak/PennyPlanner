using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionSubcategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionSubcategories.UpdateTransactionSubcategory
{
    internal sealed class UpdateTransactionSubcategoryCommandHandler : ICommandHandler<UpdateTransactionSubcategoryCommand, TransactionSubcategory>
    {
        private readonly ITransactionSubcategoryRepository _transactionSubcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTransactionSubcategoryCommandHandler(ITransactionSubcategoryRepository transactionSubcategoryRepository, IUnitOfWork unitOfWork)
        {
            _transactionSubcategoryRepository = transactionSubcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TransactionSubcategory>> Handle(UpdateTransactionSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var transactionSubcategory = await _transactionSubcategoryRepository.GetByIdAsync(
                new(Guid.Parse((ReadOnlySpan<char>)request.TransactionSubcategoryId)),
                cancellationToken);

            if (transactionSubcategory is null)
            {
                return Result.Failure<TransactionSubcategory>(
                    Error.NotFound($"Transaction subcategory with ID: {request.TransactionSubcategoryId} mot found"));
            }

            transactionSubcategory.UpdateValue(new (request.NewValue));

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            return isSuccessful ?
                transactionSubcategory :
                Result.Failure<TransactionSubcategory>(
                    Error.TaskFailed(
                        $"Problem while updating transaction subcategory with ID: {request.TransactionSubcategoryId}"));
        }
    }
}
