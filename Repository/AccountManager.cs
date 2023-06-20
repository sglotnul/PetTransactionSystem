using System.Globalization;

namespace Pet.Repository
{
    public class AccountManager : Manager<Account>
    {
        protected override Account ZipEntity(dynamic reader) => new Account((int)reader["owner"], (double)reader["sum"]) { Id = (int)reader["id"] };
        public override Account? GetById(int id)
        {
            List<Account> results = Fetch($"SELECT * FROM accounts WHERE id={id}");
            if (results.Count == 0) return null;
            return results[0];
        }

        public List<Account> GetByUserId(int id)
        {
            return Fetch($"SELECT * FROM accounts WHERE owner={id}");
        }

        public Account Insert(Account entity)
        {
            connection.Open();
            string query = $"INSERT INTO accounts (owner, sum) VALUES ({entity.RelatedUserId}, 0)";
            connection.Execute(query);
            int lastRowId = GetLastRowId();
            connection.Close();
            return entity with { Id = lastRowId, Sum = 0 };
        }

        public void Transact(int from_acc, int to_acc, double sum)
        {
            sum = Math.Round(sum, 2);
            CultureInfo culture = new CultureInfo("en-us", false);
            culture.NumberFormat.NumberDecimalDigits = 2;
            DateTime date = DateTime.Now;
            string[] queries = new[]
            {
                $"UPDATE accounts SET sum=sum-{sum.ToString(culture)} WHERE id={from_acc};",
                $"UPDATE accounts SET sum=sum+{sum.ToString(culture)} WHERE id={to_acc};",
                $"INSERT INTO transactions (from_account, to_account, sum, date) VALUES ({from_acc}, {to_acc}, {sum.ToString(culture)}, '{date.ToString("yyyy-MM-dd HH:mm:ss")}');"
            };
            connection.Open();
            connection.ExecuteMany(queries);
            connection.Close();
        }
    }
}
