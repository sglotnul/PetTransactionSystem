using System.Globalization;

namespace Pet.Repository.Manager
{
    public class TransactionManager : Manager<Transaction>, 
        IReceivable<Transaction>, 
        IDeletable
    {
        protected override Transaction ZipEntity(dynamic reader) => new Transaction((int)reader["from_account"], (int)reader["to_account"], (double)reader["sum"], (DateTime)reader["date"]) { Id = (int)reader["id"] };
        public Transaction? GetById(int id)
        {
            List<Transaction> results = Fetch($"SELECT * FROM transactions WHERE id={id};");
            if (results.Count == 0) return null;
            return results[0];
        }

        public List<Transaction> GetBySenderId(int id)
        {
            return Fetch($"SELECT * FROM transactions WHERE from_account={id};");
        }

        public List<Transaction> GetByAddresseeId(int id)
        {
            return Fetch($"SELECT * FROM transactions WHERE to_account={id};");
        }

        public List<Transaction> GetAll()
        {
            return Fetch("SELECT * FROM transactions");
        }

        public void Delete(int id)
        {
            connection.Open();
            string query = $"DELETE FROM transactions WHERE id={id}";
            connection.Execute(query);
            connection.Close();
        }
    }
}
