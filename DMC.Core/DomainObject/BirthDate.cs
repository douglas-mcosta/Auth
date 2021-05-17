using System;

namespace DMC.Core.DomainObject
{
    public class BirthDate
    {
        public DateTime Date { get; private set; }

        public BirthDate(DateTime date)
        {
            Date = date;
            if (!IsValid(date)) throw new DomainException("Data de Nascimento Invalida");
        }

        public static bool IsValid(DateTime birthDate)
        {
            var today = DateTime.Now.Date;
            if (birthDate.Date >= today || birthDate.Date <= today.AddYears(-130))
                return false;

            return true;
        }
    }
}