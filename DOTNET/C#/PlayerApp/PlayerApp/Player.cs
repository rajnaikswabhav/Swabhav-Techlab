using System;


namespace PlayerApp
{
    class Player
    {
        private readonly int _id;
        private readonly String _name;
        private int _age;

        public Player(int id, String name) : this(id, name, 18)
        {

        }

        public Player(int id, String name, int age)
        {
            _id = id;
            _name = name;
            _age = age;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
        }

        public Player WhoIsElder(Player player)
        {
            if (_age > player.Age)
            {
                return this;
            }
            return player;
        }

        public override bool Equals(object obj)
        {
            if (_id == ((Player)obj).Id)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public override string ToString()
        {
            return "Id: " + _id + "\n" + "Name : " + _name + "\n" + "Age : " + _age + "\n";
        }

    }
}
