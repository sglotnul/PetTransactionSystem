namespace Pet.Repository
{
    public interface IInsertable<T> where T : struct
    {
        T Insert(T entity);
    }
}
