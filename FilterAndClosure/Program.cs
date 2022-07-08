using System;
using System.Collections.Generic;

namespace FilterAndClosure
{
    //internal delegate bool FilterCommand<TSource>(TSource item);
    interface IFilterStrategy
    {
        bool Predicate(string item);
    }

    //SRP
    class CheckStringStartsWithAny : IFilterStrategy
    {
        public string StartsWith { get; set; }
        public bool Predicate(string item)
        {
            return item.StartsWith(StartsWith);
        }
    }

    internal class Program
    {
        internal static Func<string, bool> CheckStringStartwith_Any(string startsWith)
        {
            //function inside another function (Closure)
            Func<string, bool> predicate = (string item) =>
            {
                return item.StartsWith(startsWith);
            };
            return predicate;
        }

        static void Main(string[] args)
        {
            string[] names = { "Philips", "Siemens", "Cerner", "Apple", "Oracle" };
            //names.Where(s => s.StartsWith("s")); // > 3.0 

            Func<string, bool> _filterCommand = CheckStringStartwith_Any("S");
            IEnumerable<string> result = Query.Enumerable.Filter<string>(names, _filterCommand);

            foreach (string item in result)
            {
                Console.WriteLine(item);
            }

            Query.Enumerable.Filter<string>(names, CheckStringStartwith_Any("P"));
            Query.Enumerable.Filter<string>(names, (string item) => { return item.StartsWith("P"); });
            Query.Enumerable.Filter<string>(names, (string item) => { return item.StartsWith("S"); });
            Query.Enumerable.Filter<string>(names, (string item) => { return item.StartsWith("C"); });
            System.Linq.Enumerable.Where<string>(names, CheckStringStartwith_Any("PC"));
            //names.Filter<string>(Program.CheckStringStartwith_Any("C"));
        }
    }
}
