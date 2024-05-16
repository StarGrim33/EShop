﻿namespace BasketAPI.Data;

public class CachedBaskerRepository(IBasketRepository repository)
    : IBasketRepository
{
    public async Task<ShoppingCart?> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await repository.GetBasket(userName, cancellationToken);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        return await repository.StoreBasket(cart, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await repository.DeleteBasket(userName, cancellationToken);
    }
}