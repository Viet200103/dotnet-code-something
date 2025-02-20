namespace CSharpTryToLearn.Asynchronous;

public class SynchronizationThreadDemo
{
    
    static object lockObject = new();
    
    public static void Run()
    {
        BookMyShow bookMyShow = new BookMyShow();
        Thread t1 = new Thread(bookMyShow.TicketBooking)
        {
            Name = "Thread1"
        };

        Thread t2 = new Thread(bookMyShow.TicketBooking)
        {
            Name = "Thread2"
        };

        Thread t3 = new Thread(bookMyShow.TicketBooking)
        {
            Name = "Thread3"
        };
        
        t1.Start();
        t2.Start();
        t3.Start();

        Console.ReadKey();
    }
    
    
    public class BookMyShow
    {
        private object lockObject = new();

        private int _availableTickets = 3;
        private static int i = 1, j = 2, k = 3;

        public void BookTicket(string name, int wantedTickets)
        {
            lock (lockObject)
            {
                if (wantedTickets <= _availableTickets)
                {
                    Console.WriteLine(wantedTickets + " booked to " + name);
                    _availableTickets -= wantedTickets;
                }
                else
                {
                    Console.WriteLine("No tickets available to book");
                }
            }
        }

        public void TicketBooking()
        {
            string name = Thread.CurrentThread.Name;
            if (name.Equals("Thread1"))
            {
                BookTicket(name, i);
            }
            else if (name.Equals("Thread2"))
            {
                BookTicket(name, j);
            }
            else
            {
                BookTicket(name, k);
            }
        }
    }
}