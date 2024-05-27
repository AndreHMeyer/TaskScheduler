using RoundRobin;

class Program
{
    static void Main()
    {

        string filePath = @"C:\Users\AndréMeyer\Downloads\RrSchedule.txt";

        Console.WriteLine("Running RRP......");
        Driver.Start(filePath);
    }
}
