namespace CSharpTryToLearn.Examples.BankExample;

public interface IBankAccount
{
    bool DepositAccount(decimal amount);
    bool WithdrawAccount(decimal amount);
    decimal CheckBalance();
}