namespace TourApp.Application.Features.Tours.Specifications;

public class TourSpecificationFactory : SpecificationFactory<Tour, TourQuerySettings>
{
    public override Specification<Tour>? CreateSpecification(TourQuerySettings? settings)
    {
        if (settings is null)
        {
            return null;
        }
        
        Specification<Tour> spec = new Specification<Tour>();

        if (settings.Id.HasValue)
        {
            return spec.And(new TourIdSpecification(settings.Id.Value));
        }

        if (settings.Heading is not null)
        {
            return spec.And(new TourNameSpecification(settings.Heading));
        }

        if (settings.PriceSettings is not null)
        {
            TourPriceFilter pSettings = settings.PriceSettings;
            spec.And(new TourPriceSpecification(
                pSettings.LowerBound, 
                pSettings.UpperBound, 
                pSettings.WithDiscount));
        }

        if (settings.Countries is not null)
        {
            spec.And(new TourCountrySpecification(settings.Countries));
        }

        if (settings.Difficulties is not null)
        {
            spec.And(new TourDifficultySpecification(settings.Difficulties));
        }

        if (settings.RemainingPlaces.HasValue)
        {
            spec.And(new TourRemainingPlacesSpecification(settings.RemainingPlaces.Value));
        }

        if (settings.States is not null)
        {
            spec.And(new TourStateSpecification(settings.States));
        }

        if (settings.OrderSettings is not null)
        {
            spec.And(new TourOrderBySpecification(settings.OrderSettings));
        }
        
        spec.And(new TourPageSpecification(settings.PageIndex));

        return spec;
    }
}