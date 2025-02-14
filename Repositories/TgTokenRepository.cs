using AutoDice.Models;
using AutoDice.Interfaces;

namespace AutoDice.Repositories;

public class TgTokenRepository : IRepository<TgToken>
{
    readonly AutoDiceDbContext _context;

    public TgTokenRepository(AutoDiceDbContext ctx)
    {
        _context = ctx;
    }

    public void Add(TgToken value)
    {
        _context.TgTokens.Add(value);
        _context.SaveChanges();
    }

    public void Delete(TgToken value)
    {
        var toDelete = GetById(value.Id);
        if (toDelete == null) throw new KeyNotFoundException("Given TgToken isn't in database");
        _context.TgTokens.Remove(toDelete);
        _context.SaveChanges();
    }

    public void Update(TgToken value)
    {
        var toUpdate = GetById(value.Id);
        if (toUpdate == null) throw new KeyNotFoundException("Given TgToken isn't in database");
        toUpdate.Token = value.Token;
        _context.SaveChanges();
    }

    public TgToken? GetById(int id)
    {
        return _context.TgTokens.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<TgToken> GetAll()
    {
        return (IEnumerable<TgToken>)_context.TgTokens;
    }
}
