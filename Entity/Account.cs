namespace Pet.Entity
{
    public struct Account : IEntity<int>
    {
        public int Id { get; init; } = 0;
        public int RelatedUserId { get; set; }
        public double Sum { get; init; }
        public Account(int userId, double sum)
        {
            RelatedUserId = userId;
            Sum = sum;
        }

        public Account(int userId) : this(userId, 0) { }
    }
}
