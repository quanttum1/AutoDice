using AutoDice.Models;

namespace AutoDice.Interfaces;

public interface IAuthCodeService <T> where T : class
{
    string CreateCode(T value);
    T? Validate(string code);
}
