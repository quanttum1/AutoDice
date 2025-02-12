using AutoDice.Models;
using AutoDice.Interfaces;

namespace AutoDice.Repositories;

public class TgTokenRepository : IRepository<TgToken>
{
    AutoDiceDbContext _context;

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
        _context.TgTokens.Remove(GetById(value.Id));
        _context.SaveChanges();
    }

    public void Update(TgToken value)
    {
        var toUpdate = GetById(value.Id);
        toUpdate.Token = value.Token;
        _context.SaveChanges();
    }

    public TgToken GetById(int id)
    {
        TgToken? result = _context.TgTokens.FirstOrDefault(x => x.Id == id);
        if (result == null)
        {
            throw new KeyNotFoundException($"Couldn't find TgToken with id {id}");
        }
        return (TgToken)result;
    }

    public IEnumerable<TgToken> GetAll()
    {
        return (IEnumerable<TgToken>)_context.TgTokens;
    }
}
