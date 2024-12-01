using System;

public class Contractor
{
    public string ContractorName { get; set; }
    public string ContractorNumber { get; set; }
    public DateTime StartDate { get; set; }

    // Constructor
    public Contractor(string name, string number, DateTime startDate)
    {
        ContractorName = name;
        ContractorNumber = number;
        StartDate = startDate;
    }

    // Default Constructor
    public Contractor() { }

    // Display Contractor Information
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Contractor Name: {ContractorName}");
        Console.WriteLine($"Contractor Number: {ContractorNumber}");
        Console.WriteLine($"Start Date: {StartDate.ToShortDateString()}");
    }
}




  Subcontractor Class  


public class Subcontractor : Contractor
{
    public int Shift { get; set; } // 1 for Day Shift, 2 for Night Shift
    public double HourlyPayRate { get; set; }

    // Constructor
    public Subcontractor(string name, string number, DateTime startDate, int shift, double hourlyPayRate)
        : base(name, number, startDate)
    {
        Shift = shift;
        HourlyPayRate = hourlyPayRate;
    }

    // Default Constructor
    public Subcontractor() { }

    // Calculate Pay with Shift Differential
    public float CalculatePay(int hoursWorked)
    {
        double basePay = HourlyPayRate * hoursWorked;

        // Apply 3% shift differential for night shift (Shift == 2)
        if (Shift == 2)
        {
            basePay += basePay * 0.03;
        }

        return (float)basePay;
    }

    // Display Subcontractor Information
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        string shiftType = Shift == 1 ? "Day Shift" : "Night Shift";
        Console.WriteLine($"Shift: {shiftType}");
        Console.WriteLine($"Hourly Pay Rate: {HourlyPayRate:C}");
    }
}




  Program Class  

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<Subcontractor> subcontractors = new List<Subcontractor>();
        bool continueAdding = true;

        Console.WriteLine("=== Subcontractor Management System ===");

        while (continueAdding)
        {
            // Input Subcontractor Details
            Console.Write("\nEnter Contractor Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Contractor Number: ");
            string number = Console.ReadLine();

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("Invalid date. Please enter again (yyyy-mm-dd): ");
            }

            Console.Write("Enter Shift (1 for Day, 2 for Night): ");
            int shift;
            while (!int.TryParse(Console.ReadLine(), out shift) || (shift != 1 && shift != 2))
            {
                Console.Write("Invalid shift. Please enter 1 for Day or 2 for Night: ");
            }

            Console.Write("Enter Hourly Pay Rate: ");
            double hourlyPayRate;
            while (!double.TryParse(Console.ReadLine(), out hourlyPayRate) || hourlyPayRate <= 0)
            {
                Console.Write("Invalid pay rate. Please enter a positive value: ");
            }

            // Create Subcontractor Object
            Subcontractor sub = new Subcontractor(name, number, startDate, shift, hourlyPayRate);
            subcontractors.Add(sub);

            Console.Write("Do you want to add another subcontractor? (yes/no): ");
            string response = Console.ReadLine().ToLower();
            continueAdding = response == "yes";
        }

        // Display Subcontractor Details
        Console.WriteLine("\n=== Subcontractor Details ===");
        foreach (var sub in subcontractors)
        {
            sub.DisplayInfo();
            Console.WriteLine($"Weekly Pay (40 hours): {sub.CalculatePay(40):C}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
