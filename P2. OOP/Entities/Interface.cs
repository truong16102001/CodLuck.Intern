using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2._OOP.Abstract
{
    public class Interface
    {
        public Interface()
        {
            IAnimal myDog = new Dog();
            myDog.Name = "Buddy";
            Console.WriteLine(myDog.Name);  // Output: Buddy
            myDog.Speak();                  // Output: Dog barks
            myDog.Sleep();                  // Output: Dog sleeps
        }
        public interface IAnimal
        {
            // Khả năng của lớp
            string Name { get; set; }
            void Speak();
            void Sleep();
        }

        public class Dog : IAnimal
        {
            public string Name { get; set; }

            public void Sleep()
            {
                Console.WriteLine("Dog sleeps");
            }

            public void Speak()
            {
                Console.WriteLine("Dog barks");
            }
        }
    }
   
   
}
