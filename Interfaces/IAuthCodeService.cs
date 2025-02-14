using AutoDice.Models;

namespace AutoDice.Interfaces;

interface IAuthCodeService <T> where T : class
{
    string CreateCode(T value);
    T? Validate(string code);
}
