using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationSingleton
{
    class Program
    {
        static void Main(string[] args)
        {
            //Can't call constructor it's private
            //Singleton s1 = new Singleton();
            //Need to Instantiate with static methods
            Singleton s2 = Singleton.Instance;  //Instance will always be the same instance
            


            //Console.WriteLine(s1.GetHashCode());
            Console.WriteLine(s2.GetHashCode());
            Console.ReadKey();
        }
    }
}
