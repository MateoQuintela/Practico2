using Service;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
           // TestServices.TestConnection();
            var orderService = new InvoiceService();
            var result = orderService.GetAll();
            Console.Read();
        }
    }
}