using System;

namespace ConsoleAppForQuickTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            String name = "=lowercase";
            Foo(ref name);
            Console.WriteLine(name);
            Console.WriteLine(name.Substring(1));
            Console.ReadKey();
        }

        public static void Foo (ref string name)
        {
            SubFoo(ref name);
        }

        public static void SubFoo (ref string name)
        {
            name = name.ToUpper();
        }
    }
}
