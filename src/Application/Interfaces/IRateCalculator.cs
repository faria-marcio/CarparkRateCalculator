using Domain.ValueObjects;

namespace Application.Interfaces;

public interface IRateCalculator
{
    RateResponse CalculateRate(DateTime entry, DateTime exit);
}