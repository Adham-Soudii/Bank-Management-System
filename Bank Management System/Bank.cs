namespace Bank_Management_System
{
    public class Account
    {
        protected string name;
        protected double balance;

        public Account(string name = "default Account", double balance = 0.0)
        {
            this.name = name;
            this.balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            balance += amount;
            return true;
        }

        public virtual bool Withdraw(double amount)
        {
            if (balance - amount >= 0)
            {
                balance -= amount;
                return true;
            }
            return false;
        }

        public double GetBalance() => balance;

        public override string ToString()
        {
            return $"[Account: {name}, Balance: {balance}]";
        }
    }

    public class SavingsAccount : Account
    {
        private double interestRate;

        public SavingsAccount(string name = "default Savings", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            this.interestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            amount += (amount * interestRate / 100);
            return base.Deposit(amount);
        }

        public override string ToString()
        {
            return $"[SavingsAccount: {name}, Balance: {balance}, InterestRate: {interestRate}%]";
        }
    }

    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(string name = "default Checking", double balance = 0.0)
            : base(name, balance)
        {
        }

        public override bool Withdraw(double amount)
        {
            amount += WithdrawalFee;
            return base.Withdraw(amount);
        }

        public override string ToString()
        {
            return $"[CheckingAccount: {name}, Balance: {balance}]";
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int withdrawalsThisYear = 0;
        private const int MaxWithdrawals = 3;

        public TrustAccount(string name = "default Trust", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance, interestRate)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
                amount += 50;
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawalsThisYear >= MaxWithdrawals)
                return false;

            if (amount > balance * 0.2) //Withdraw More Than 20% From Balance Not Allow
                return false;

            if (base.Withdraw(amount))
            {
                withdrawalsThisYear++;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[TrustAccount: {name}, Balance: {balance}, Withdrawals: {withdrawalsThisYear}/{MaxWithdrawals}]";
        }
    }

    public static class AccountUtil
    {
        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n==== Accounts =======================================");
            foreach (var acc in accounts)
                Console.WriteLine(acc);
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n==== Depositing to Accounts ============================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n==== Withdrawing from Accounts =========================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }

    class Bank
    {
        static void Main()
        {
            var accounts = new List<Account>
        {
            new Account(),
            new Account("Adham"),
            new Account("Ahmed", 2000),
            new Account("Mohamed", 5000)
        };

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            var savAccounts = new List<Account>
        {
            new SavingsAccount(),
            new SavingsAccount("Ali"),
            new SavingsAccount("Salah", 2000),
            new SavingsAccount("Lara", 5000, 5.0)
        };

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            var checAccounts = new List<Account>
        {
            new CheckingAccount(),
            new CheckingAccount("Mona"),
            new CheckingAccount("Zizo", 2000),
            new CheckingAccount("Ziad", 5000)
        };

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            var trustAccounts = new List<Account>
        {
            new TrustAccount(),
            new TrustAccount("Marwan"), // 20% = 1410 Marwan at the end in trust
            new TrustAccount("Menna", 2000), // 20% = 1810 Menna at the end in trust
            new TrustAccount("Youmna", 5000, 5.0) // 20% = 2480.5 Youmna at the end in trust
        };

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

        }
    }

}


