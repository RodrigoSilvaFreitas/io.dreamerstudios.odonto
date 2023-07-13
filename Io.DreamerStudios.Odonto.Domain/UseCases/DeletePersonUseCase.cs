using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;

namespace Io.DreamerStudios.Odonto.Domain.UseCases
{
    public class DeletePersonUseCase : IDeletePersonUseCase
    {
        private readonly IPersonGateway _personGateway;

        public DeletePersonUseCase(IPersonGateway personGateway)
        {
            _personGateway = personGateway;
        }

        public void DeletePerson(long id)
        {
            _personGateway.Delete(id);
        }
    }
}
