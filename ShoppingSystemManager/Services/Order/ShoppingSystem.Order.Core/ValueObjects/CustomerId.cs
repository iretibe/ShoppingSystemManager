﻿using ShoppingSystem.Order.Core.Exceptions;

namespace ShoppingSystem.Order.Core.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }

        private CustomerId(Guid value) => Value = value;

        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new OrderDomainException("CustomerId cannot be empty!");
            }

            return new CustomerId(value);
        }
    }
}
