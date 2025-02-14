using AutoDice.Models;
using AutoDice.Interfaces;

namespace AutoDice.Repositories;

public class PlayerRepository : IRepository<Player>
{
    readonly AutoDiceDbContext _context;

    public PlayerRepository(AutoDiceDbContext ctx)
    {
        _context = ctx;
    }

    public void Add(Player value)
    {
        _context.Players.Add(value);
        _context.SaveChanges();
    }

    public void Delete(Player value)
    {
        var toDelete = GetById(value.Id);
        if (toDelete == null) throw new KeyNotFoundException("Given Player isn't in database");
        _context.Players.Remove(toDelete);
        _context.SaveChanges();
    }

    public void Update(Player value)
    {
        var toUpdate = GetById(value.Id);
        if (toUpdate == null) throw new KeyNotFoundException("Given Player isn't in database");
        toUpdate.Name = value.Name;
        toUpdate.IsGameMaster = value.IsGameMaster;
        _context.SaveChanges();
    }

    public Player? GetById(int id)
    {
        return _context.Players.FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<Player> GetAll()
    {
        return (IEnumerable<Player>)_context.Players;
    }
}

