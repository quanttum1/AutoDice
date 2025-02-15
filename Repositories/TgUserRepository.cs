using AutoDice.Models;
using AutoDice.Interfaces;

namespace AutoDice.Repositories;

public class TgUserRepository : IRepository<TgUser>
{
    readonly AutoDiceDbContext _context;

    public TgUserRepository(AutoDiceDbContext ctx)
    {
        _context = ctx;
    }

    public void Add(TgUser value)
    {
        _context.TgUsers.Add(value);
        _context.SaveChanges();
    }

    public void Delete(TgUser value)
    {
        var toDelete = GetById(value.TgUserId);
        if (toDelete == null) throw new KeyNotFoundException("Given TgUser isn't in database");
        _context.TgUsers.Remove(toDelete);
        _context.SaveChanges();
    }

    public void Update(TgUser value)
    {
        var toUpdate = GetById(value.TgUserId);
        if (toUpdate == null) throw new KeyNotFoundException("Given TgUser isn't in database");
        toUpdate.Player = value.Player;
        toUpdate.PlayerId = value.Player.Id;
        _context.SaveChanges();
    }

    public TgUser? GetById(params object[] args)
    {
        if (args.Length != 1) throw new ArgumentException("GetById takes 1 argument");
        if (args[0] is long id)
            return _context.TgUsers.FirstOrDefault(x => x.TgUserId == id);
        else
            throw new ArgumentException("The argument should be long");
    }

    public IEnumerable<TgUser> GetAll()
    {
        return (IEnumerable<TgUser>)_context.TgUsers;
    }
}

