using BankingSystem;
using System;

namespace BankingSystem
{
    public class BankAccount
    {
        private int accoNumber;
        private string accoName;
        private double balance;
        private string accoType;

        public BankAccount(int accoNum, string name, double initialBal, string type)
        {
            accoNumber = accoNum;
            accoName = name;
            balance = initialBal;
            accoType = type;
        }

        public int AccountNumber
        {
            get
            {
                return accoNumber;
            }
        }

        public string AccountName
        {
            get
            {
                return accoName;
            }
        }

        public double Balance
        {
            get
            {
                return balance;
            }
        }

        public string AccountType
        {
            get
            {
                return accoType;
            }
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;

                Console.WriteLine($"\n*****************************************");
                Console.WriteLine($"Successfully deposited Php {amount:F2}");
                Console.WriteLine($"New balance: Php {balance:F2}");
                Console.WriteLine($"*****************************************");
            }
            else
            {
                Console.WriteLine("*** Invalid deposit amount! ***");
            }
        }

        public bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("*** Invalid withdrawal amount! ***");
                return false;
            }
            else if (amount > balance)
            {
                Console.WriteLine($"\n*****************************************");
                Console.WriteLine("Insufficient funds!");
                Console.WriteLine($"Current balance: Php {balance:F2}");
                Console.WriteLine($"*****************************************");
                return false;
            }
            else
            {
                balance -= amount;
                Console.WriteLine($"\n*****************************************");
                Console.WriteLine($"Successfully withdrew Php {amount:F2}");
                Console.WriteLine($"New balance: Php {balance:F2}");
                Console.WriteLine($"*****************************************");
                return true;
            }
        }

        public void DisplayAccountDetails()
        {
            Console.WriteLine($"\n*****************************************");
            Console.WriteLine("============ ACCOUNT DETAILS ============");
            Console.WriteLine($"Account Number: {accoNumber}");
            Console.WriteLine($"Account Holder: {accoName}");
            Console.WriteLine($"Account Type: {accoType}");
            Console.WriteLine($"Current Balance: Php {balance:F2}");
            Console.WriteLine($"*****************************************");
        }
    }
}

class SimpleApp
{
    static BankAccount[] accounts = new BankAccount[5];
    static int accountCount = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("=================================");
        Console.WriteLine("  WELCOME TO BANK SYSTEM ");
        Console.WriteLine("=================================");

        while (true)
        {
            DisplayMenu();
            int choice = GetUserChoice();

            switch (choice)
            {
                case 1:
                    CreateAccount();
                    break;
                case 2:
                    DepositMoney();
                    break;
                case 3:
                    WithdrawMoney();
                    break;
                case 4:
                    CheckBalance();
                    break;
                case 5:
                    ViewAccountDetails();
                    break;
                case 6:
                    ListAllAccounts();
                    break;
                case 7:
                    Console.WriteLine("\nThank you for using Bank System!");
                    return;
                default:
                    Console.WriteLine("\nInvalid choice! Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n===== BANK MENU =====");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. View Account Details");
            Console.WriteLine("6. List of All Accounts");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice (1-7): ");
        }

        static int GetUserChoice()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            return -1;
        }

        static BankAccount FindAccount(int accountNumber)
        {
            for (int i = 0; i < accountCount; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        static void CreateAccount()
        {
            if (accountCount >= accounts.Length)
            {
                Console.WriteLine("\nMaximum number of accounts reached!");
                return;
            }

            Console.WriteLine("\n===== CREATE NEW ACCOUNT =====");

            Console.Write("Enter account holder name: ");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\nInvalid name! Account creation failed.\n");
                Console.Write("Enter account holder name: ");
                name = Console.ReadLine();
            }

            Console.Write("Enter account type (Savings/Checking): ");
            string type = Console.ReadLine();

            while (!type.Equals("Savings", StringComparison.OrdinalIgnoreCase) &&
                   !type.Equals("Checking", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nInvalid account type. Please enter Savings or Checking.\n");
                Console.Write("Enter account type (Savings/Checking): ");
                type = Console.ReadLine();
            }

            Console.Write("Enter initial deposit amount: Php ");
            string balInput = Console.ReadLine();
            double balance;

            while (!double.TryParse(balInput, out balance) || balance <= 0)
            {
                Console.WriteLine("\nInvalid amount! Please enter a valid positive number.\n");
                Console.Write("Enter initial deposit amount: Php ");
                balInput = Console.ReadLine();
            }

            int accountNumber = 1000 + accountCount + 1;

            BankAccount newAccount = new BankAccount(accountNumber, name, balance, type);

            accounts[accountCount] = newAccount;
            accountCount++;

            Console.WriteLine($"\n*****************************************");
            Console.WriteLine($"Account created successfully!");
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Holder: {name}");
            Console.WriteLine($"Account Type: {type}");
            Console.WriteLine($"Initial Balance: Php {balance:F2}");
            Console.WriteLine($"*****************************************");
        }

        static void DepositMoney()
        {
            Console.WriteLine("\n===== DEPOSIT MONEY =====");
            Console.Write("Enter account number: ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.Write("Enter deposit amount: Php ");
                    string amtInput = Console.ReadLine();
                    if (double.TryParse(amtInput, out double amount) && amount > 0)
                    {
                        account.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid amount entered!");
                    }
                }
                else
                {
                    Console.WriteLine("\nAccount not found!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid account number!");
            }

        }

        static void WithdrawMoney()
        {
            Console.WriteLine("\n===== WITHDRAW MONEY =====");
            Console.Write("Enter account number: ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.Write("Enter withdraw amount: Php ");
                    string amtInput = Console.ReadLine();
                    if (double.TryParse(amtInput, out double amount) && amount > 0)
                    {
                        account.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid amount entered!");
                    }
                }
                else
                {
                    Console.WriteLine("\nAccount not found!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid account number!");
            }
        }

        static void CheckBalance()
        {
            Console.WriteLine("\n===== CHECK BALANCE =====");
            Console.Write("Enter account number: ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.WriteLine($"\n*****************************************");
                    Console.WriteLine($"Current Balance: Php {account.Balance:F2}");
                    Console.WriteLine($"*****************************************");
                }
                else
                {
                    Console.WriteLine("\nAccount not found!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid account number!");
            }
        }

        static void ViewAccountDetails()
        {
            Console.WriteLine("\n===== VIEW ACCOUNT DETAILS =====");
            Console.Write("Enter account number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    account.DisplayAccountDetails();
                }
                else
                {
                    Console.WriteLine("\nAccount not found!");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid account number!");
            }
        }

        static void ListAllAccounts()
        {
            Console.WriteLine("\n===== LIST OF ALL ACCOUNTS =====");

            if (accountCount == 0)
            {
                Console.WriteLine("\nNo accounts found!");
                return;
            }

            Console.WriteLine($"{"Account#",-10} {"Name",-20} {"Type",-10} {"Balance",-10}");
            Console.WriteLine(new string('-', 50));

            for (int i = 0; i < accountCount; i++)
            {
                BankAccount account = accounts[i];
                Console.WriteLine($"{account.AccountNumber,-10} {account.AccountName,-20} {account.AccountType,-10} Php {account.Balance,-10:F2}");
            }

        }
    }
}