namespace BankApp12E
{
    public class Bank
    {
        Dictionary<string, int> accounts = new Dictionary<string, int>();

        public void CreateAccount(string account)
        {
            accounts.Add(account, 0);
        }

        public int GetBalance(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[account];
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
            accounts[account] += amount;
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
            else if (accounts[account] < amount)
            {
                //throw new ArgumentException("Insufficient funds");
                return false;
            }
            accounts[account] -= amount;
            return true;
        }

        public void Transfer(string fromAccount, string toAccount, int amount)
        {
            if (!accounts.ContainsKey(fromAccount) || !accounts.ContainsKey(toAccount))
            {
                throw new ArgumentException("Invalid account");
            }
            else if (amount < 0)
            {
                throw new ArgumentException("Transfer amount must be positive");
            }
            else if (accounts[fromAccount] < amount)
            {
                throw new ArgumentException("Insufficient funds");
            }
            accounts[fromAccount] -= amount;
            accounts[toAccount] += amount;
        }

        public void CloseAccount(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            } else if(accounts[account] != 0)
            {
                throw new InvalidOperationException("Account balance must be zero to close account");
            }
            accounts.Remove(account);
        }
    }
}
