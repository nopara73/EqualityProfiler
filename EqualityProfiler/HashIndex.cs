using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;

namespace EqualityProfiler
{
    public class HashIndex
    {
        public uint256 Hash { get; set; }
        public int Index { get; set; }

        public HashIndex(uint256 hash, int index)
        {
            Hash = hash;
            Index = index;
        }
    }
}
