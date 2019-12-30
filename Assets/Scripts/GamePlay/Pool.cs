using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T>
{
    List<T> _items;

    #region Methods

    public T Get()
    {
        if (_items.Count > 0)
        {
            T item = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
            return item;
        }
        else
        {
            _items.Capacity++;
            return CreateNewObject();
        }
    }


    public void Return(T item)
    {
        OnReturn(item);
        _items.Add(item);
    }

    protected abstract T CreateNewObject();
    protected abstract void OnReturn(T item);


    #endregion


    #region Constructors

    public Pool(int initialCapacity = 10)
    {
        _items = new List<T>();
        _items.Capacity = initialCapacity;
    }

    #endregion
}
