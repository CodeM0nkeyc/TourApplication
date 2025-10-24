namespace TourApp.Persistence.TypeConfigs.ValueComparers;

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() 
        : base(
            (x, y) => x.DayNumber == y.DayNumber,
            date => date.GetHashCode()
        )
    {
    }
}