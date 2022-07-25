namespace Pet.Repository
{
    public interface IUpdatable<T> where T : struct
    {
        void Update(int id, T entity);
    }
}
