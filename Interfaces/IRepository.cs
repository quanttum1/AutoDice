namespace AutoDice.Interfaces;

public interface IRepository <T> where T : class
{
    public T Add(T value); // Return the entity because Id might've been set automatically
    public void Delete(T value);
    public T? GetById(params object[] args); // Some may use int, some use Guid, others use Composite key
    public IEnumerable<T> GetAll();
    public void Update(T value);
}
