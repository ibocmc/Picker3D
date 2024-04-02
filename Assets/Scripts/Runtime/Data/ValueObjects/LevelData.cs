using System;
using System.Collections.Generic;
using UnityEngine;


namespace Data.ValueObjects
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> Pools;
        public Color _color;
    }
}