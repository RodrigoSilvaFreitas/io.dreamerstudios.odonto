using Io.DreamerStudios.Odonto.Core.Contracts;
using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;
using Io.DreamerStudios.Odonto.Core.DTO;
using Io.DreamerStudios.Odonto.Core.Exceptions;
using System.Text.RegularExpressions;

namespace Io.DreamerStudios.Odonto.Domain.UseCases
{
    public class CreatePersonUseCase : ICreatePersonUseCase
    {
        private readonly IPersonGateway _personGateway;

        public CreatePersonUseCase(IPersonGateway personGateway)
        {
            _personGateway = personGateway;
        }

        public Person CreatePerson(Person person)
        {
            bool alreadyHasCpf = _personGateway.ExistsDocument(person.Document);

            if (alreadyHasCpf)
            {
                throw new BusinessException("CPF já existe");
            }

            if (!IsCpfValid(person.Document))
            {
                throw new BusinessException("CPF inválido");
            }

            if (!person.IsAdult)
            {
                throw new BusinessException("A pessoa não é adulto");
            }

            if (!IsPhoneNumberValid(person.Phone))
            {
                throw new BusinessException("Número de telefone inválido");
            }

            return _personGateway.CreatePerson(person);
        }

        //public Person CreatePerson(Person person)
        //{
          //  throw new NotImplementedException();
        //}

        public bool IsPhoneNumberValid(string phone)
        {
            string validatorPattern = @"^\+\d{2}\s?\d{2}\s?\d{4,5}\-?\d{4}$"; //+11 11 11111-1111

            return Regex.IsMatch(phone, validatorPattern);
        }

        private bool IsCpfValid(string document)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digit;
            int sum;
            int rest;

            document = document.Trim();
            document = document.Replace(".", "").Replace("-", "");

            if (document.Length != 11)
                return false;

            tempCpf = document.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = rest.ToString();
            tempCpf = tempCpf + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;
            digit = digit + rest.ToString();

            return document.EndsWith(digit);
        }
    }
}