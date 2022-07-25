namespace Pet.Entity
{
    public struct Transaction : IEntity<int>
    {
        public int Id { get; init; } = 0;
        public int FromAccount { get; init; }
        public int ToAccount { get; init; }
        public double Sum { get; init; }
        public DateTime Date { get; init; }
        public Transaction(int from, int to, double sum, DateTime date)
        {
            FromAccount = from;
            ToAccount = to;
            Sum = sum;
            Date = date;
        }
        public Transaction(int from, int to, double sum) : this(from, to, sum, DateTime.Now) { }
    }
}
