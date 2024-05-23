using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2._OOP.Abstract
{
    public class Abstract
    {
        public Abstract()
        {
            // khai báo myDob kiểu Animal
            Animal myDog = new Dog(); // or Dog myDog = new Dog(); áp dụng tính đa hình

            myDog.Name = "Milu";

            Console.WriteLine(myDog.Name);

            myDog.Run();

            myDog.Speak();

            myDog.Sleep();
        }
        public abstract class Animal
        {
            // Thuộc tính chung
            public string Name { get; set; }

            // Phương thức trừu tượng, lớp con phải triển khai

            public abstract void Speak();

            // đánh dấu virtual để class con có thể override lại
            public virtual void Run()
            {
                Console.WriteLine("Animal Run");
            }

            // Phương thức có thân, lớp con có thể kế thừa
            public void Sleep()
            {
                Console.WriteLine("Animal sleep");
            }
        }

        public class Dog : Animal
        {
            // khi 1 phương thức abstract chưa được định nghĩa ở lớp cha thì lớp con phải implement
            public override void Speak()
            {
                Console.WriteLine("Dob barks");
            }

            public override void Run()
            {
                Console.WriteLine("Dog run");
            }
        }       
    }
}
