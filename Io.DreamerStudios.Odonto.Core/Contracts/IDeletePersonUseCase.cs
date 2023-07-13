using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Io.DreamerStudios.Odonto.Core.Contracts
{
    public interface IDeletePersonUseCase
    {
        void DeletePerson(long id);
    }
}
