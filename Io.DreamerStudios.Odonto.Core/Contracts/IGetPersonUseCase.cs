using Io.DreamerStudios.Odonto.Core.DTO;

namespace Io.DreamerStudios.Odonto.Core.Contracts
{
    public interface IGetPersonUseCase
    {
        Person[] GetPerson(
            int quantity,
            bool? adult,
            long? responsibleId,
            string name,
            DateTime birthday,
            string sex,
            string document,
            string phone,
            long cityId
            );
    }
}
