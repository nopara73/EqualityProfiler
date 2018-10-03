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
            var hashIndexList = new List<HashIndex>();
            var txList = new List<Transaction>();
            for (int i = 0; i < 10000; i++)
            {
                int index = new Random().Next(0, 3);
                var tx = Transaction.Create(Network.Main);
                tx.AddOutput(Money.Coins(1m), new Key());
                txList.Add(tx);
                uint256 hash = tx.GetHash();
                HashIndex hashIndex = new HashIndex(hash, index);
                hashIndexList.Add(hashIndex);
            }
            Console.WriteLine("Start measurements...");

            var sw = new Stopwatch();
            sw.Start();
            foreach (var elem1 in hashIndexList)
            {
                foreach (var elem2 in hashIndexList)
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
            foreach (var elem1 in hashIndexList)
            {
                foreach (var elem2 in hashIndexList)
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
            foreach (var elem1 in hashIndexList)
            {
                foreach (var elem2 in hashIndexList)
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
            foreach (var elem1 in hashIndexList)
            {
                foreach (var elem2 in hashIndexList)
                {
                    if (elem1?.Index == elem2?.Index && elem1?.Hash == elem2?.Hash)
                    {
                        ;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine("index FIRST, nullcheck=\t\t{0}", sw.Elapsed);
            Console.WriteLine();

            var randomTx = Transaction.Create(Network.Main);

            sw.Reset();
            sw.Start();
            var found = txList.Contains(randomTx);
            sw.Stop();
            Console.WriteLine($"Contains:\t {sw.Elapsed}");

            sw.Reset();
            sw.Start();
            found = txList.FirstOrDefault(x => x == randomTx) != default(Transaction);
            sw.Stop();
            Console.WriteLine($"FirstOrDefault:\t {sw.Elapsed}");

            sw.Reset();
            sw.Start();
            found = txList.Any(x => x == randomTx);
            sw.Stop();
            Console.WriteLine($"Any:\t\t {sw.Elapsed}");

            Console.ReadKey();
        }
    }
}
