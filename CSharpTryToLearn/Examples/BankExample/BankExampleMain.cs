namespace CSharpTryToLearn.Examples.BankExample;

public class BankExampleMain
{

    public static void Run()
    {
        Console.WriteLine("Saving Account: ");
        IBankAccount savingAccount = new SavingAccount();
        savingAccount.DepositAccount(2000);
        savingAccount.DepositAccount(1000);
        savingAccount.WithdrawAccount(1500);
        savingAccount.WithdrawAccount(5000);
        Console.WriteLine($"Saving Account Balance: {savingAccount.CheckBalance()}");
        
        Console.WriteLine("\nCurrent Account: ");
        IBankAccount currentAccount = new CurrentAccount();
        currentAccount.DepositAccount(500);
        currentAccount.DepositAccount(1500);
        currentAccount.WithdrawAccount(2600);
        currentAccount.WithdrawAccount(1000);
        Console.WriteLine($"Current Account Balance: {currentAccount.CheckBalance()}");
        
        Console.ReadLine();
    }
}