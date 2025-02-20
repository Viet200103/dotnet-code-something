namespace CSharpTryToLearn.Examples.BankExample;

public class CurrentAccount : IBankAccount
{
    private decimal _balance = 0;
    
    public bool DepositAccount(decimal amount)
    {
        _balance += amount;
        Console.WriteLine($"Deposited {amount}");
        Console.WriteLine($"Current Balance: {_balance}");
        return true;
    }

    public bool WithdrawAccount(decimal amount)
    {
        if (_balance < amount)
        {
            Console.WriteLine("Not enough money");
            return false;
        }
        else
        {
            _balance -= amount;
            Console.WriteLine($"You have successfully withdraw: {amount}");
            Console.WriteLine($"Your Account Balance: {_balance}");
            return true;
        }
    }

    public decimal CheckBalance()
    {
        return _balance;
    }
}