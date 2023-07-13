using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;
using Io.DreamerStudios.Odonto.Core.DTO;

namespace Io.DreamerStudios.Odonto.Domain.UseCases
{
    public class UpdatePersonUseCase : IUpdatePersonUseCase
    {
        private readonly IPersonGateway _personGateway;

        public UpdatePersonUseCase(IPersonGateway personGateway)
        {
            _personGateway = personGateway;
        }

        public Person UpdatePerson(Person person)
        {
            return _personGateway.Update(person);
        }
    }
}
