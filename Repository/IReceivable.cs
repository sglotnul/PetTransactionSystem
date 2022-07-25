namespace Pet.Repository
{
    public interface IReceivable<T> where T : struct
    {
        T? GetById(int id);
        List<T> GetAll();
    }
}
