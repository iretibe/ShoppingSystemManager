﻿using ShoppingSystem.Order.Core.Exceptions;

namespace ShoppingSystem.Order.Core.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }

        private ProductId(Guid value) => Value = value;

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new OrderDomainException("ProductId cannot be empty!");
            }

            return new ProductId(value);
        }
    }
}
