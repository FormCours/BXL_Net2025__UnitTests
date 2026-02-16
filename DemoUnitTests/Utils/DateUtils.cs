namespace DemoUnitTests.API.Utils
{
    public class DateUtils
    {
        public static bool Overlap(DateTime startDate1, DateTime startDate2, DateTime endDate1, DateTime endDate2)
        {
            // s1 2000-01-01T12:00:00 
            // s2 2000-01-02T12:00:00
            // e1 2000-01-01T13:00:00 
            // e2 2000-01-03T12:00:00
            // false
            throw new NotImplementedException();
        }

        public DateProvider _dateProvider = new DateProvider();

        public int GetAge(DateTime birthDate)
        {
            var today = _dateProvider.GetDate();
            if(birthDate > today)
            {
                throw new ArgumentException();
            }
            int age = today.Year - birthDate.Year;
            if(today.Month < birthDate.Month)
            {
                age -= 1;
            }
            else if(today.Month == birthDate.Month && today.Day < birthDate.Day)
            {
                age -= 1;
            }
            return age;
        }
    }

    public class DateProvider
    {
        public virtual DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}
