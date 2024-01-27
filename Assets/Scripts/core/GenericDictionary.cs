using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDictionary<TKey, TValue>
{
    private Dictionary<TKey, TValue> internalDictionary = new Dictionary<TKey, TValue>();
    private TValue error;

    public void Setup(TValue _error)
    {
        error = _error;
    }

    public void Add(TKey key, TValue value)
    {
        internalDictionary.Add(key, value);
    }

    public TValue Get(TKey key)
    {
        TValue foundValue;
        if(internalDictionary.TryGetValue(key, out foundValue)){
            return foundValue;
        }
        return error;
    }
}