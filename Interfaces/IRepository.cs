namespace AutoDice.Interfaces;

public interface IRepository <T> where T : class
{
    public void Add(T value);
    public void Delete(T value);
    public T? GetById(params object[] args); // Some may use int, some use Guid, others use Composite key
    public IEnumerable<T> GetAll();
    public void Update(T value);
}
