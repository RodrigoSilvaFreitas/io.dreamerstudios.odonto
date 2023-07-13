using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;
using Io.DreamerStudios.Odonto.Core.DTO;

namespace Io.DreamerStudios.Odonto.Domain.UseCases
{
    public class GetPersonUseCase : IGetPersonUseCase
    {
        private readonly IPersonGateway _personGateway;

        public GetPersonUseCase(IPersonGateway personGateway)
        {
            _personGateway = personGateway;
        }

        public Person[] GetPerson(
            int quantity,
            bool? adult,
            long responsibleId,
            string? name,
            DateTime? birthday,
            string? sex,
            string? document,
            string? phone,
            long cityId
            )
        {
            return _personGateway.GetPeople(
                quantity,
                adult,
                responsibleId,
                name,
                sex,
                birthday,
                document,
                phone,
                cityId);
        }

    }
}

