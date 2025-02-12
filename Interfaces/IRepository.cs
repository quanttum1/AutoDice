namespace AutoDice.Interfaces;

public interface IRepository <T> where T : class
{
    public void Add(T value);
    public void Delete(T value);
    public T GetById(int id);
    public IEnumerable<T> GetAll();
}
