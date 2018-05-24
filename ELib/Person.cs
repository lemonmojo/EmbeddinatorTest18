using System;

namespace ELib
{   
    public class Person
    {
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

        public string FullName
		{
			get {
				return FirstName + " " + LastName;
			}
		}

		public int Age { get; set; }

        public Person() { }

		public Person(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

        public void ThrowException()
        {
            ThrowException("A nice Exception Message");
        }

        public void ThrowException(string message)
        {
            throw new Exception(message);
        }

        public void DoIntesiveWork()
        {
            int i = 0;

            do {
                Print("DoIntesiveWork " + i);

                i++;
            } while (true);
        }

        public void Print(string str)
        {
            Console.WriteLine(str);
        }      
    }
}