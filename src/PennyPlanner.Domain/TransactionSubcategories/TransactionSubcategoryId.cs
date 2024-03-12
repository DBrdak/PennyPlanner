using CommonAbstractions.DB.Entities;

namespace PennyPlanner.Domain.TransactionSubcategories;

public record TransactionSubcategoryId : EntityId
{
    public TransactionSubcategoryId(Guid value) : base(value)
    {

    }

    public TransactionSubcategoryId() : base(Guid.NewGuid())
    {

    }
}