namespace BankApp12E
{
    public class Bank
    {
        Dictionary<string, Account> accounts = new Dictionary<string,Account>();

        public void CreateAccount(string account, string name)
        {
            accounts.Add(account, new Account(name, 0));
        }

        public int GetBalance(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[account].Balance;
        }

        public void Deposit(string account, int amount)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            } else if(amount < 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }
            accounts[account].Balance += amount;
        }

        public bool Withdraw(string account, int amount)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            else if (amount < 0)
            {
                throw new ArgumentException("Withdraw amount must be positive");
            }
            else if (accounts[account].Balance < amount)
            {
                //throw new ArgumentException("Insufficient funds");
                return false;
            }
            accounts[account].Balance -= amount;
            return true;
        }

        public bool Transfer(string fromAccount, string toAccount, int amount)
        {
            if (!accounts.ContainsKey(fromAccount) || !accounts.ContainsKey(toAccount))
            {
                throw new ArgumentException("Invalid account");
            }
            else if (amount < 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
            }
            else if (accounts[fromAccount].Balance < amount)
            {
                //throw new ArgumentException("Insufficient funds");
                return false;
            }
            accounts[fromAccount].Balance -= amount;
            accounts[toAccount].Balance += amount;
            return true;
        }

        public void CloseAccount(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            } else if(accounts[account].Balance != 0)
            {
                throw new InvalidOperationException("Account balance must be zero to close account");
            }
            accounts.Remove(account);
        }

        public string GetOwner(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[account].Name;
        }
    }
}
