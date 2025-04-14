using System.Runtime.Intrinsics.X86;

namespace CSharpTryToLearn.DesignPattern.StructuralPattern.Adapter;

public class LegacyPayment
{
    private interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, string currency);
        bool RefundPayment(decimal amount, string currency, string transactionId);
    }
    
    public class OldPaymentGateway
    {
        public bool MakeTransaction(int amountInCents, string currencyCode)
        {
            Console.WriteLine($"Processing payment of {amountInCents} cents in {currencyCode}");
            return true; // Simulate success
        }

        public bool RefundTransaction(decimal amountInCents, string currencyCode, string transactionId)
        {
            Console.WriteLine($"Refunding {amountInCents} cents in {currencyCode} for transaction {transactionId}");
            return true;
        }
    }

    public class PaymentAdapter: IPaymentProcessor
    {
        private readonly OldPaymentGateway _oldPaymentGateway;

        public PaymentAdapter(OldPaymentGateway oldPaymentGateway)
        {
            _oldPaymentGateway = oldPaymentGateway;
        }
        
        public bool ProcessPayment(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount. Payment aborted.");
                return false;
            }
            
            int amountInCents = (int)(amount * 100);
            
            string currencyCode = currency.ToUpper() switch
            {
                "USD" => "US",
                "EUR" => "EU",
                _ => throw new ArgumentException("Unknown currency code.")
            };
            
            return _oldPaymentGateway.MakeTransaction(amountInCents, currencyCode);
        }

        public bool RefundPayment(decimal amount, string currency, string transactionId)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid amount. Refund aborted.");
                return false;
            }

            if (String.IsNullOrEmpty(transactionId))
            {
                Console.WriteLine("Invalid transaction ID. Payment aborted.");
                return false;
            }
            
            int amountInCents = (int)Math.Round(amount * 100);
            string currencyCode = currency.ToUpper() switch
            {
                "USD" => "US",
                "EUR" => "EU",
                _ => throw new ArgumentException($"Unsupported currency: {currency}")
            };
            
            return _oldPaymentGateway.RefundTransaction(amount, currency, transactionId);
        }
    }
    
    public class PaymentProgram
    {
        public static void Run()
        {
            OldPaymentGateway oldGateway = new OldPaymentGateway();
            IPaymentProcessor processor = new PaymentAdapter(oldGateway);

            try
            {
                // Test payment
                processor.ProcessPayment(50.49m, "USD");

                // Test refund
                processor.RefundPayment(50.49m, "USD", "TX123");

                // Test invalid refund
                processor.RefundPayment(50.49m, "USD", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}