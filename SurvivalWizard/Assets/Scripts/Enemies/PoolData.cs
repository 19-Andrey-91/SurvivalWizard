using System;
using UnityEngine;

[Serializable]
public class PoolData<T> where T : MonoBehaviour
{
    public T Prefab;
    public int PoolCount;
    public int PoolMaxCount;
    public Transform Container;
}
