﻿namespace EShopMicroservice.Ordering.Domain.Abstractions;

public interface IEntityId
{
    Guid Value { get; }
}
