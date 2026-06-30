using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Base class representing a person
public abstract class Person
{
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    // Constructor to initialise common attributes
    public Person(string name, DateTime dateOfBirth, string email, string phoneNumber)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    // Abstract method to be implemented by derived classes
    public abstract void ShowDetails();

    // Validates the format of an email address
    public static bool ValidateEmail(string email)
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return emailRegex.IsMatch(email);
    }

    // Validates the format of a phone number
    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        var phoneRegex = new Regex(@"^\+?[0-9]{10,15}$");
        return phoneRegex.IsMatch(phoneNumber);
    }
}

// Class representing a teacher, derived from person
public class Teacher : Person
{
    public string Subject1 { get; set; }
    public string Subject2 { get; set; }
    public decimal Salary { get; set; }

    // Constructor to initialise teacher specific attributes
    public Teacher(string name, DateTime dateOfBirth, string email, string phoneNumber, string subject1, string subject2, decimal salary)
        : base(name, dateOfBirth, email, phoneNumber)
    {
        Subject1 = subject1;
        Subject2 = subject2;
        Salary = salary;
    }

    // Displays the teachers details
    public override void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}\nDate of Birth: {DateOfBirth.ToShortDateString()}\nEmail: {Email}\nPhone Number: {PhoneNumber}\nSubject 1: {Subject1}\nSubject 2: {Subject2}\nSalary: {Salary:C}");
    }
}

// Class representing admin, derived from person
public class Admin : Person
{
    public string Role { get; set; }

    // Constructor to initialise admin specific attributes
    public Admin(string name, DateTime dateOfBirth, string email, string phoneNumber, string role)
        : base(name, dateOfBirth, email, phoneNumber)
    {
        Role = role;
    }

    // Displays the admins details
    public override void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}\nDate of Birth: {DateOfBirth.ToShortDateString()}\nEmail: {Email}\nPhone Number: {PhoneNumber}\nRole: {Role}");
    }
}

// Class representing a student, derived from person
public class Student : Person
{
    // Constructor to initialise student attributes
    public Student(string name, DateTime dateOfBirth, string email, string phoneNumber)
        : base(name, dateOfBirth, email, phoneNumber)
    {
    }

    // Displays the student details 
    public override void ShowDetails()
    {
        Console.WriteLine($"Name: {Name}\nDate of Birth: {DateOfBirth.ToShortDateString()}\nEmail: {Email}\nPhone Number: {PhoneNumber}");
    }
}

// Main Program class
class Program
{
    static List<Person> people = new List<Person>(); // List to hold all persons (Teacher, Admin, Student)

    static void Main()
    {
        // Continuous loop for main menu
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Desktop Information System");
            Console.WriteLine("1. Add Person");
            Console.WriteLine("2. View All Persons");
            Console.WriteLine("3. View Persons by Role");
            Console.WriteLine("4. Edit Person");
            Console.WriteLine("5. Delete Person");
            Console.WriteLine("6. Exit");
            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            // Handle user choice
            switch (choice)
            {
                case "1":
                    AddPerson();
                    break;
                case "2":
                    ViewAllPersons();
                    break;
                case "3":
                    ViewPersonsByRole();
                    break;
                case "4":
                    EditPerson();
                    break;
                case "5":
                    DeletePerson();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Adds a new person to the system
    static void AddPerson()
    {
        Console.Clear();
        Console.WriteLine("Add Person");
        Console.WriteLine("Choose role: (1) Teacher, (2) Admin, (3) Student");
        string roleChoice = Console.ReadLine();

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
        DateTime dateOfBirth;
        while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
        {
            Console.Write("Invalid date. Please enter a valid Date of Birth (yyyy-mm-dd): ");
        }

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();
        while (!Person.ValidateEmail(email))
        {
            Console.Write("Invalid email. Please enter a valid Email: ");
            email = Console.ReadLine();
        }

        Console.Write("Enter Phone Number: ");
        string phoneNumber = Console.ReadLine();
        while (!Person.ValidatePhoneNumber(phoneNumber))
        {
            Console.Write("Invalid phone number. Please enter a valid Phone Number: ");
            phoneNumber = Console.ReadLine();
        }

        // Add person based on selected role
        if (roleChoice == "1") // Teacher
        {
            Console.Write("Enter Subject 1: ");
            string subject1 = Console.ReadLine();

            Console.Write("Enter Subject 2: ");
            string subject2 = Console.ReadLine();

            Console.Write("Enter Salary: ");
            decimal salary;
            while (!decimal.TryParse(Console.ReadLine(), out salary))
            {
                Console.Write("Invalid salary. Please enter a valid Salary: ");
            }

            people.Add(new Teacher(name, dateOfBirth, email, phoneNumber, subject1, subject2, salary));
        }
        else if (roleChoice == "2") // Admin
        {
            Console.Write("Enter Role: ");
            string role = Console.ReadLine();

            people.Add(new Admin(name, dateOfBirth, email, phoneNumber, role));
        }
        else if (roleChoice == "3") // Student
        {
            people.Add(new Student(name, dateOfBirth, email, phoneNumber));
        }

        Console.WriteLine("Person added successfully. Press any key to continue.");
        Console.ReadKey();
    }

    // Displays all persons stored in the system
    static void ViewAllPersons()
    {
        Console.Clear();
        Console.WriteLine("All Persons:");
        foreach (var person in people)
        {
            person.ShowDetails();
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    // Filters and displays persons by their role
    static void ViewPersonsByRole()
    {
        Console.Clear();
        Console.WriteLine("Choose role: (1) Teacher, (2) Admin, (3) Student");
        string roleChoice = Console.ReadLine();

        Console.WriteLine($"{(roleChoice == "1" ? "Teachers" : roleChoice == "2" ? "Admins" : "Students")}:");

        foreach (var person in people)
        {
            if ((roleChoice == "1" && person is Teacher) ||
                (roleChoice == "2" && person is Admin) ||
                (roleChoice == "3" && person is Student))
            {
                person.ShowDetails();
                Console.WriteLine();
            }
        }
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    // Allows editing of a selected persons details
    static void EditPerson()
    {
        Console.Clear();
        Console.WriteLine("Enter the index of the person to edit (0 to cancel): ");
        for (int i = 0; i < people.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {people[i].Name}");
        }

        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= people.Count)
        {
            Person person = people[index - 1];

            Console.WriteLine("Edit details for: " + person.Name);
            Console.Write("Enter new Name (leave blank to keep unchanged): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) person.Name = name;

            Console.Write("Enter new Email (leave blank to keep unchanged): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email) && Person.ValidateEmail(email)) person.Email = email;

            Console.Write("Enter new Phone Number (leave blank to keep unchanged): ");
            string phoneNumber = Console.ReadLine();
            if (!string.IsNullOrEmpty(phoneNumber) && Person.ValidatePhoneNumber(phoneNumber)) person.PhoneNumber = phoneNumber;

            Console.WriteLine("Details updated successfully. Press any key to continue.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Invalid index. Press any key to try again.");
            Console.ReadKey();
        }
    }

    // Deletes a person from the list
    static void DeletePerson()
    {
        Console.Clear();
        Console.WriteLine("Enter the index of the person to delete (0 to cancel): ");
        for (int i = 0; i < people.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {people[i].Name}");
        }

        int index;
        if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= people.Count)
        {
            people.RemoveAt(index - 1);
            Console.WriteLine("Person deleted successfully. Press any key to continue.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Invalid index. Press any key to try again.");
            Console.ReadKey();
        }
    }
}