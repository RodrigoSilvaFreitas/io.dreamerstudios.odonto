using Io.DreamerStudios.Odonto.Core.DTO;

namespace Io.DreamerStudios.Odonto.Core.Contracts
{
    public interface ICreatePersonUseCase
    {
        Person CreatePerson(Person person);
    }
}
