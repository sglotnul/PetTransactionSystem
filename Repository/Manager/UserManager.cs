﻿namespace Pet.Repository.Manager
{
    public class UserManager : Manager<User>, 
        IReceivable<User>, 
        IInsertable<User>, 
        IUpdatable<User>, 
        IDeletable
    {
        protected override User ZipEntity(dynamic reader) => new User((string)reader["name"], (string)reader["password"]) { Id = (int)reader["id"] };
        public User? GetById(int id)
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

        public List<User> GetAll()
        {
            return Fetch("SELECT * FROM users;");
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

        public void Update(int id, User entity)
        {
            connection.Open();
            string query = $"UPDATE users SET name='{entity.Name}', password='{entity.Password}' WHERE id={id}";
            connection.Execute(query);
            connection.Close();
        }

        public void Delete(int id)
        {
            connection.Open();
            string query = $"DELETE FROM users WHERE id={id}";
            connection.Execute(query);
            connection.Close();
        }
    }
}
