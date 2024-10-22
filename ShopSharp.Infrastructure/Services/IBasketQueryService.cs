﻿namespace ShopSharp.Infrastructure.Services;

/// <summary>
/// Specific query used to fetch count without running in memory
/// </summary>
public interface IBasketQueryService
{
    Task<int> CountTotalBasketItems(string username);
}

