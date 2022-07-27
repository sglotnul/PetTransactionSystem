namespace Pet.Repository
{
    public class UserManager : Manager<User>
    {
        protected override User ZipEntity(dynamic reader) => new User((string)reader["name"], (string)reader["password"]) { Id = (int)reader["id"] };
        public override User? GetById(int id)
        {
            List<User> results = Fetch($"SELECT * FROM users WHERE id={id};");
            if (results.Count == 0) return null;
            return results[0];
        }

        public User? GetByName(string name)
        {
            List<User> results = Fetch($"SELECT * FROM users WHERE name='{name}';");
            if (results.Count == 0) return null;
            return results[0];
        }

        public User Insert(User entity)
        {
            connection.Open();
            string query = $"INSERT INTO users (name, password) VALUES ('{entity.Name}', '{entity.Password}')";
            connection.Execute(query);
            int lastRowId = GetLastRowId();
            connection.Close();
            return entity with { Id = lastRowId };
        }
    }
}
