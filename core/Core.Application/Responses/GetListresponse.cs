﻿using Core.Persistence.Paging;
namespace Core.Application.Responses;
public class GetListresponse<T> : BasePageableModel
{
    public IList<T> Items
    {
        get => _items ??= new List<T>();
        set => _items = value;
    }
    private IList<T>? _items;
}

