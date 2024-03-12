using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.TransactionCategories;

public sealed record TransactionCategoryId : EntityId
{
    public TransactionCategoryId(Guid value) : base(value)
    {
        
    }

    public TransactionCategoryId() : base(Guid.NewGuid()) 
    {
        
    }
}