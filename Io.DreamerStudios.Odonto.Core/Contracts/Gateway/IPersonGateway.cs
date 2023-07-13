using Io.DreamerStudios.Odonto.Core.DTO;

namespace Io.DreamerStudios.Odonto.Core.Contracts.Gateway
{
    public interface IPersonGateway
    {
        Person[] GetPeople(
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
        Person CreatePerson(Person person);

        bool ExistsDocument(string document);

        bool ExistsCustomerId(int customerId);

        Person? GetById(int customerId);

        void Delete(long id);

        Person Update(Person person);
    }
}