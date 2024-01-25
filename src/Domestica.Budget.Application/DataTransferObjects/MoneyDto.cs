﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record MoneyDto
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        private MoneyDto(decimal Amount, string Currency)
        {
            this.Amount = Amount;
            this.Currency = Currency;
        }

        private MoneyDto()
        {
            
        }

        internal static MoneyDto FromDomainObject(Money.DB.Money domainObject)
        {
            return new (domainObject.Amount, domainObject.Currency.Code);
        }

        internal Money.DB.Money ToDomainObject()
        {
            return new (Amount, Money.DB.Currency.FromCode(Currency));
        }
    }
}
