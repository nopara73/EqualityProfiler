using NBitcoin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EqualityProfiler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var list = new List<HashIndex>();
            for (int i = 0; i < 10000; i++)
            {
                int index = new Random().Next(0, 3);
                var tx = Transaction.Create(Network.Main);
                tx.AddOutput(Money.Coins(1m), new Key());
                uint256 hash = tx.GetHash();
                HashIndex hashIndex = new HashIndex(hash, index);
                list.Add(hashIndex);
            }
            Console.WriteLine("Start measurements...");

            var sw = new Stopwatch();
            sw.Start();
            foreach (var elem1 in list)
            {
                foreach (var elem2 in list)
                {
                    if (elem1.Hash == elem2.Hash && elem1.Index == elem2.Index)
                    {
                        ;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("index SECOND, NO nullcheck=\t{0}", sw.Elapsed);

            sw.Reset();
            sw.Start();
            foreach (var elem1 in list)
            {
                foreach (var elem2 in list)
                {
                    if (elem1?.Hash == elem2?.Hash && elem1?.Index == elem2?.Index)
                    {
                        ;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("index SECOND, nullcheck=\t{0}", sw.Elapsed);

            sw.Reset();
            sw.Start();
            foreach (var elem1 in list)
            {
                foreach (var elem2 in list)
                {
                    if (elem1.Index == elem2.Index && elem1.Hash == elem2.Hash)
                    {
                        ;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("index FIRST, NO nullcheck=\t{0}", sw.Elapsed);

            sw.Reset();
            sw.Start();
            foreach (var elem1 in list)
            {
                foreach (var elem2 in list)
                {
                    if (elem1?.Index == elem2?.Index && elem1?.Hash == elem2?.Hash)
                    {
                        ;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("index FIRST, nullcheck=\t\t{0}", sw.Elapsed);

            Console.ReadKey();
        }
    }
}
