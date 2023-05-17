
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// T1 - type Key,
/// T2 - type Value
/// </summary>
[Serializable]
public class CustomDictionary<T1, T2>
{
    [SerializeField] private List<KeyAndValue<T1, T2>> _dictionary;

    public T2 GetValueDictionary(T1 dictionaryKey)
    {
        T1 _key = dictionaryKey;
        T2 value = default;
        int count = 0;
        foreach (var key in _dictionary)
        {
            if (key.Key.Equals(dictionaryKey))
            {
                count++;
                value = key.Value;
                if (value.Equals(default(T2)))
                {
                    throw new UnityException($"Value not set for key: \"{_key}\"");
                }
            }
            if(count > 1)
            {
                throw new UnityException($"Multiple keys found for: \"{_key}\"");
            }
        }
        if (count <= 0)
        {
            throw new UnityException($"Key: \"{_key}\",  not found");
        }
        
        return value;
    }
}
