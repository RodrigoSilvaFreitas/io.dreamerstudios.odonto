using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Io.DreamerStudios.Odonto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PersonController : ControllerBase

    {
        private readonly ILogger<PersonController> _logger;
        private readonly IGetPersonUseCase _getUseCase;
        private readonly ICreatePersonUseCase _createUseCase;
        private readonly IDeletePersonUseCase _deleteUseCase;
        private readonly IUpdatePersonUseCase _updateUseCase;

        public PersonController(ILogger<PersonController> logger,
                                         IGetPersonUseCase getuseCase,
                                         ICreatePersonUseCase createUseCase,
                                         IDeletePersonUseCase deleteUseCase,
                                         IUpdatePersonUseCase updateUseCase)
        {
            _logger = logger;
            _getUseCase = getuseCase;
            _createUseCase = createUseCase;
            _deleteUseCase = deleteUseCase;
            _updateUseCase = updateUseCase;
        }

        [HttpPut]

        public IActionResult Put([FromBody] Person person)
        {
            return Ok(_updateUseCase.UpdatePerson(person));
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id)
        {
            _deleteUseCase.DeletePerson(id);
            return Ok();
        }

        [HttpGet(Name = "GetPerson")]
        public IActionResult Get(
            [FromQuery] int quantity,
            [FromQuery] bool? adult,
            [FromQuery] long? responsibleId,
            [FromQuery] string sex,
            [FromQuery] string name,
            [FromQuery] DateTime birthday,
            [FromQuery] string document,
            [FromQuery] string phone,
            [FromQuery] long cityId)
        {
            if (quantity > 0)
            {
                return Ok(_getUseCase.GetPerson(
                    quantity, adult, responsibleId, name, birthday, sex, document,
                    phone, cityId));
            }
            return new BadRequestObjectResult("Quantidade inválida");
        }

        [HttpPost(Name = "PostPerson")]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null)
            {
                ModelState.AddModelError("", "É obrigatório informar uma pessoa válida.");
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(person.Name))
            {
                ModelState.AddModelError("Name", "O campo Nome é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(person.Sex))
            {
                ModelState.AddModelError("Sex", "O campo Sexo é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(person.Document))
            {
                ModelState.AddModelError("Cpf", "O campo CPF é obrigatório.");
            }

            if (person.Birthday == null)
            {
                ModelState.AddModelError("Birthday", "O campo Data de Nascimento é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(person.Phone))
            {
                ModelState.AddModelError("Phone", "É obrigatório informar um telefone");
            }

            if (person.Phone?.Length > 17 || person.Phone?.Length < 14)
            {
                ModelState.AddModelError("Phone", "O Telefone informado não está no formato correto," +
                                         "favor inserir um número de telefone com o formato:" +
                                         " +XX XX XXXXX-XXXX");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var retorno = _createUseCase.CreatePerson(person);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
