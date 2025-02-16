using AutoDice.Models;
using AutoDice.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoDice.Repositories;

public class WebUserSessionRepository : IRepository<WebUserSession>
{
    readonly AutoDiceDbContext _context;
    readonly IRepository<Player> _players;

    public WebUserSessionRepository(AutoDiceDbContext ctx, IRepository<Player> players)
    {
        _players = players;
        _context = ctx;
    }

    public WebUserSession Add(WebUserSession value)
    {
        value.PlayerId = value.Player.Id;

        var player = _players.GetById(value.PlayerId);
        if (player == null) player = _players.Add(value.Player);
        
        value.Player = player;
        _context.WebUserSessions.Add(value);
        _context.SaveChanges();
        return value;
    }

    public void Delete(WebUserSession value)
    {
        var toDelete = GetById(value.Id);
        if (toDelete == null) throw new KeyNotFoundException("Given WebUserSession isn't in database");
        _context.WebUserSessions.Remove(toDelete);
        _context.SaveChanges();
    }

    public void Update(WebUserSession value)
    {
        var toUpdate = GetById(value.Id);
        if (toUpdate == null) throw new KeyNotFoundException("Given WebUserSession isn't in database");
        toUpdate.Player = value.Player;
        toUpdate.PlayerId = value.Player.Id;
        _context.SaveChanges();
    }

    public WebUserSession? GetById(params object[] args)
    {
        if (args.Length != 1) throw new ArgumentException("GetById takes 1 argument");
        if (args[0] is Guid id)
            return GetAll().FirstOrDefault(x => x.Id == id);
        else
            throw new ArgumentException("The argument should be Guid");
    }

    public IEnumerable<WebUserSession> GetAll()
    {
        return (IEnumerable<WebUserSession>)_context.WebUserSessions
            .Include(x => x.Player);
    }
}
