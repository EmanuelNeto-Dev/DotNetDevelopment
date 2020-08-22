using System;
using System.Collections.Generic;
using System.Linq;

namespace NumericQuery
{
    class Program
    {
        /*
         * Type Inference.
         *  Ex.: var list = ...
         *  
         *  - Eliminates the need to guess the type of data returned by the LINQ query;
         *  - Eliminates verbose and repetitive code in some circunstances;
         *  - Makes possible the use of the feature Anonymouos Types when it's writing query expressions;
         *  - Allows to easily use a powerful feature LINQ called composability;
         */
        public static void Main(string[] args)
        {
            /* 
               Declaring and initializing the collection 
               It's just a short-hand to: 
               
                List<int> list = new List<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
             */
            List<int> list = new List<int> { 1, 2, 3 };
            
            var query = from number         //This clause came first to the compiler know immediately the type of data you wanted to query;
                        in list
                        where number < 3    //This clause instructs the compiler to filter the items of list;
                        select number;      //This clause came last because the IDE wont immediately know the type of data you wanted to query;

            foreach (var number in query)
            {
                Console.WriteLine(number);
            }
        }
    }
}
