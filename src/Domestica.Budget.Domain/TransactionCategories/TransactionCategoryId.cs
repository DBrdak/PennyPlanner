using CommonAbstractions.DB.Entities;

namespace Domestica.Budget.Domain.TransactionCategories;

public sealed record TransactionCategoryId : EntityId
{
    public TransactionCategoryId(Guid value) : base(value)
    {
        
    }

    public TransactionCategoryId() : base(Guid.NewGuid()) 
    {
        
    }
}