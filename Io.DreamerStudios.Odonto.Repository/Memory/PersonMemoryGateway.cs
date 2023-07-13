using Io.DreamerStudios.Odonto.Core.Contracts.Gateway;
using Io.DreamerStudios.Odonto.Core.DTO;
using Io.DreamerStudios.Odonto.Repository.Config;

namespace Io.DreamerStudios.Odonto.Repository.Memory
{
    public class PersonMemoryGateway : IPersonGateway
    {
        private readonly OdontoContext _context;

        public PersonMemoryGateway(OdontoContext context)
        {
            _context = context;
        }

        public Person Update(Person person)
        {
            var entity = _context.People.FirstOrDefault(p => p.Id == person.Id);
            if (entity != null)
            {
                _context.People.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.People.Entry(entity).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            return person;
        }


        public void Delete(long id)
        {
            var entity = _context.People.FirstOrDefault(p => p.Id == id);
            if (entity != null)
            {
                _context.People.Remove(entity);
                _context.SaveChanges();
            }
        }
        public Person[] GetPeople(
            int quantity,
            bool? adult,
            long? responsibleId,
            string name,
            DateTime birthday,
            string sex,
            string document,
            string phone,
            long cityId
            )
        {
            var minimumDate = DateTime.Now.AddYears(-18);

            return _context.People.Where(x => (sex == null || x.Sex == sex)
            && (name == null || x.Name == name)
            && (document == null || x.Document == document)
            && (birthday == null || x.Birthday == birthday)
            && (phone == null || x.Phone == phone)
            && (adult == null
             || (adult == true && x.Birthday <= minimumDate)
             || (adult == false && x.Birthday >= minimumDate)))
                .Take(quantity)
                .ToArray();
        }

        public Person CreatePerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();

            return person;
        }

        public bool ExistsDocument(string document)
        {
            return _context.People.Where(x => x.Document == document).Any();
        }

        public bool ExistsCustomerId(int customerId)
        {
            return _context.People.Where(x => x.Id == customerId).Any();
        }

        public Person? GetById(int customerId)
        {
            return _context.People.Where(x => x.Id == customerId).FirstOrDefault();
        }
    }
}