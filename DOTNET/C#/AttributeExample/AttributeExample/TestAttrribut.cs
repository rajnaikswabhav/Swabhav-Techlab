using System;


namespace AttributeExample
{
    class TestAttrribut
    {
        private int number = 10;
        private int number2 = 20;

        [NeedRefectoring]
        public void Sum(int value1, int value2)
        {
            var sum = value1 + value2 ;
            Console.WriteLine("Sum of {0} and {1} is: {2}",value1,value2,sum);
        }

        
        public void Subtraction(int value1, int value2)
        {
            var sub = value1 + value2;
            Console.WriteLine("Subtraction of {0} and {1} is: {2}", value1, value2, sub);
        }

        public void Multiplication(int value1, int value2)
        {
            var mul = value1 + value2;
            Console.WriteLine("Multiplication of {0} and {1} is: {2}", value1, value2, mul);
        }

        [NeedRefectoring]
        public void Division(int value1, int value2)
        {
            var div = value1 / value2;
            Console.WriteLine("Division of {0} and {1} is: {2}", value1, value2, div);
        }

    }
}
