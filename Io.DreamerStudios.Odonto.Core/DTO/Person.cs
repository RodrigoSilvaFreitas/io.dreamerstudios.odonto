namespace Io.DreamerStudios.Odonto.Core.DTO
{
    public class Person
    {
        public Person(
            long? responsibleId,
            string name,
            DateTime birthday,
            string sex,
            string document,
            string phone,
            long cityId
            )
        {
            Id = 0;
            ResponsibleId = responsibleId;
            Name = name;
            Birthday = birthday;
            Sex = sex;
            Document = document;
            Phone = phone;
            CityId = cityId;
        }

        public long Id { get; set; }

        public long? ResponsibleId { get; set; }
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Sex { get; set; }

        public string Document { get; set; }

        public string Phone { get; set; }

        public long CityId { get; set; }
        public bool IsAdult
        {
            get
            {
                var minimumDate = DateTime.Now.AddYears(-18);
                return Birthday <= minimumDate;
            }
        }

        public bool IsChild
        {
            get
            {
                var minimumDate = DateTime.Now.AddYears(-18);
                return Birthday >= minimumDate;
            }
        }

    }
}

