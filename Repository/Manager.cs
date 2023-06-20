using Pet.Repository.Connection;

namespace Pet.Repository
{
    public abstract class Manager<T> where T : struct
    {
        protected DbConnection connection;
        protected abstract T ZipEntity(dynamic reader);
        protected List<T> Fetch(string query)
        {
            connection.Open();
            var reader = connection.Fetch(query);
            List<T> results = new List<T>();
            while (reader.Read())
            {
                results.Add(ZipEntity(reader));
            }
            reader.Close();
            connection.Close();
            return results;
        }
        protected int GetLastRowId()
        {
            string query = "SELECT SCOPE_IDENTITY()";
            var reader = connection.Fetch(query);
            reader.Read();
            int lastRowId = (int)(decimal)reader[0];
            reader.Close();
            return lastRowId;
        }
        public abstract T? GetById(int id);
        public Manager()
        {

            string? connectionString = Environment.GetEnvironmentVariable("MyDbConnectionString");
            if (connectionString != null)
            {
                connection = new(connectionString);
                return;
            }
            throw new Exception("connection string variable not found");
        }
    }
}
