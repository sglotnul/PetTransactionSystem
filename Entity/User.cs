namespace Pet.Entity
{
    public struct User : IEntity<int>
    {
        public int Id { get; init; } = 0;
        public string Name { get; init; }
        public string Password { get; init; }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
