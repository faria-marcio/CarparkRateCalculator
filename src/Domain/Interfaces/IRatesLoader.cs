using Domain.Entities;

namespace Domain.Interfaces;

public interface IRatesLoader
{
    List<Rate> LoadRatesData();
}
