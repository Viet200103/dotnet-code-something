namespace CSharpTryToLearn.Examples.BankExample;

public class SavingAccount : IBankAccount
{

    private decimal _balance = 0;
    private readonly decimal _perDayWithdrawLimit = 10000;
    private decimal _todayWithdrawal = 0;
    
    public bool DepositAccount(decimal amount)
    {
        _balance = _balance + amount;
        Console.WriteLine($"You have Deposited: {amount}");
        Console.WriteLine($"Your Account Balance: {_balance}");
        return true;
    }

    public bool WithdrawAccount(decimal amount)
    {

        if (_balance < amount)
        {
            Console.WriteLine("You don't have enough money");
            return false;
        }
        else if (_todayWithdrawal + amount > _perDayWithdrawLimit)
        {
            Console.WriteLine("Withdrawal Limit Exceeded");
            return false;
        }
        else
        {
            _balance -= amount;
            _todayWithdrawal += amount;
            Console.WriteLine($"You have Withdrawn: {amount}");
            Console.WriteLine($"Your Account Balance: {_balance}");
            return true;
        }
    }

    public decimal CheckBalance()
    {
        return _balance;
    }
}