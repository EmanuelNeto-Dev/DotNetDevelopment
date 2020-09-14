using System;

#region Notes

/*
 *  - Class definitions always contain the 'Class' keyword followed by the name of the class (Ex.: class Counter {});
 *  - public         class   Counter{}
 *    accessibility  type    name of the object 
 *      => If the class does not have an specific access modifier declared, the default value is 'Internal'. It means 
 *      that is only possible to use this class within the component that created it. (The same library);
 *      => It's possible to give visibility to Internal classes through other libraries. As the Microsoft does with 
 *      its libraries. This is made possible by annotating a component with the [assembly: InternalVisibleTo("name")]
 *      attribute.
 *  - accessibility modifiers are optional for members of the class, as they are for classes. 
 *      => If you do not have some explicit access modifier declared for the member, the default value is 'Private'. 
 *      That means that just the class where the member was created, can use it directly.
 *  - The new operator indicates a construction of a new instance of the class 
 * 
 *  By Convention:
 *      - Classes (PascalCasing) => In these conventions, the first letter of a class name is capitalized, and if the name contains multiple 
 *      words, each new word also starts with a capital letter.
 *      - Methods (CamelCasing) => Uppercase letters are used at the start of all but the first word;
 *     
 */

#endregion

namespace ASimpleClass
{
    public class Counter
    {
        private int _count;
        public int GetNextValue()
        {
            _count += 1;
            return _count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Counter();
            var c2 = new Counter();

            Console.WriteLine($"c1: {c1.GetNextValue()}");
            Console.WriteLine($"c1: {c1.GetNextValue()}");
            Console.WriteLine($"c1: {c1.GetNextValue()}");

            Console.WriteLine($"c2: {c2.GetNextValue()}");
            Console.WriteLine($"c1: {c1.GetNextValue()}");
        }
    }
}
