namespace EarliestDeadlineFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\AndréMeyer\Downloads\EdfSchedule.txt";

            Console.WriteLine("Running EDF......");
            Driver.Start(path);
        }
    }
}
