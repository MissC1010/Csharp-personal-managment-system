using System;
using System.ComponentModel;

namespace PersonManagementSystem
{
    public class Person
    {
        //fields
        public string Name { get; protected set; }
        public int Age { get; protected set; }

        // Constructor
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        //methods
        public string ReturnDetails()
        {
            return Name + "-" + Age;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetAge(int age)
        {
            this.Age = age;
        }
    }


    public class Manager
    {

        //fields
        public List<Person> people;

        //constructor
        public Manager()
        {
            people = new List<Person>();
            PrintMenu();
        }

        //methods
        public void PrintMenu()
        {
            Console.WriteLine($"Welcome to the management system!");
            Console.WriteLine("1. Print all users");
            Console.WriteLine("2. Add user");
            Console.WriteLine("3. Edit user");
            Console.WriteLine("4. Search user");
            Console.WriteLine("5. Remove user");
            Console.WriteLine("6. Exit");


            Console.WriteLine("What option would you like to perform?");

            if (int.TryParse(Console.ReadLine(), out int menuOption))
            {
                switch (menuOption)
                {
                    case 1:
                        PrintAll();
                        break;
                    case 2:
                        AddPerson();
                        break;
                    case 3:
                        EditPerson();
                        break;
                    case 4:
                        SearchPerson();
                        break;
                    case 5:
                        RemovePerson();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        return; // Exit the method and return to the caller
                    default:
                        OutPutMessage("Incorrect menu choice");
                        break;
                }
            }
            else
            {
                OutPutMessage("Incorrect menu choice");
            }

            PrintMenu(); // Only call PrintMenu() if the user didn't choose to exit
        }

        public void PrintAll()
        {
            StartOption("Printed all users");

            if (people.Count == 0)
            {
                Console.WriteLine("No users added");
            }
            else
            {
                PrintAllUsers();
            }

            FinishOption();
        }

        public void AddPerson()
        {
            StartOption("Add user");

            try
            {
                string nameInput = "";
                Person person = ReturnPerson();

                if (person != null)
                {
                    people.Add(person);
                    Console.WriteLine($"Successfully added {nameInput}");
                    FinishOption();
                }
                else
                {
                    OutPutMessage("Something went wrong");
                    AddPerson();
                }

            }
            catch (Exception)
            {
                OutPutMessage($"Something went wrong");
                AddPerson();
            }
        }

        public void EditPerson()
        {
            StartOption("user to edit");

            if (people.Count == 0)
            {
                Console.WriteLine($"No users to edit");
            }
            else
            {
                PrintAllUsers();

                try
                {
                    Console.Write("Enter an index\n");
                    int indexSelection = Convert.ToInt32(Console.ReadLine());
                    indexSelection -= 1;

                    if (indexSelection <= 0 && indexSelection <= people.Count - 1)
                    {
                        try
                        {
                            string nameInput = "";
                            Person person = ReturnPerson();

                            if (person != null)
                            {
                                people[indexSelection] = person;
                                Console.WriteLine($"Successfully edited {nameInput}");
                                FinishOption();
                            }
                            else
                            {
                                OutPutMessage("Something went wrong");
                                EditPerson();
                            }

                        }
                        catch (Exception)
                        {
                            OutPutMessage($"Something went wrong");
                            EditPerson();
                        }
                    }
                    else
                    {
                        OutPutMessage("Enter a valid index");
                        EditPerson();
                    }
                }
                catch (Exception)
                {
                    OutPutMessage("Something went wrong.");
                    EditPerson();
                }
            }
        }

        public void SearchPerson()
        {
            StartOption("Searching users");

            if (people.Count == 0)
            {
                Console.WriteLine($"No users in system");
            }
            else
            {
                Console.Write($"Enter name: ");
                string nameInput = Console.ReadLine();
                bool beFound = false;

                if (!string.IsNullOrEmpty(nameInput))
                {
                    for (int i = 0; i < people.Count; i++)
                    {
                        if (people[i].Name.ToLower().Contains(nameInput))
                        {
                            Console.WriteLine(people[i].ReturnDetails());
                            beFound = true;
                        }
                    }

                    if (!beFound)
                    {
                        Console.WriteLine($"No user found with that name");
                    }

                    FinishOption();
                }
                else
                {
                    OutPutMessage("Please enter a name");
                    SearchPerson();
                }

            }


        }

        public void RemovePerson()
        {
            StartOption("user Removed");

            if (people.Count == 0)
            {
                Console.WriteLine($"There are no users in system");
            }
            else
            {
                PrintAllUsers();

                Console.WriteLine($"Enter an index");
                int index = Convert.ToInt32(Console.ReadLine());
                index--;

                if (index >= 0 && index <= people.Count - 1)
                {
                    string removedName = people[index].Name;
                    people.RemoveAt(index);
                    Console.WriteLine($"removed {removedName}");

                    FinishOption();
                }
                else
                {
                    OutPutMessage("enter a valid index inside the range");
                    RemovePerson();
                }
            }
        }

        public void FinishOption()
        {
            Console.WriteLine($"\nPress <Enter> to return to menu");
            Console.ReadLine();
            Console.Clear();
        }

        public void StartOption(string message)
        {
            Console.Clear();
            Console.WriteLine($"{message}");
        }

        public void OutPutMessage(string message)
        {
            Console.WriteLine($"{message} Press <Enter> to try again");
            Console.ReadLine();
            Console.Clear();
        }

        public void PrintAllUsers()
        {
            for (int i = 0; i < people.Count; i++)
            {
                Console.WriteLine($"{i + 1}.  {people[i].ReturnDetails()}");
            }
        }

        public Person ReturnPerson()
        {
            Console.Write($"Enter a name:\n");
            string nameInput = Console.ReadLine();

            Console.Write($"Enter an age:\n");
            int ageInput = Convert.ToInt32(Console.ReadLine());

            // Check if nameInput is null or empty and then age
            if (!string.IsNullOrEmpty(nameInput))
            {
                if (ageInput >= 0 && ageInput <= 150)
                {
                    return new Person(nameInput, ageInput);
                }
                else
                {
                    OutPutMessage("Enter a sensible age");
                }
            }
            else
            {
                OutPutMessage("You didnt enter a name");
            }
            return null;
        }
        class Program
        {
            static void Main(string[] args)
            {
                Manager manager = new Manager();
            }
        }
    }
}


