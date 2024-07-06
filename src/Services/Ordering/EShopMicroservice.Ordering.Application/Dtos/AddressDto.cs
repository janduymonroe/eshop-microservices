namespace EShopMicroservice.Ordering.Application.Dtos;

public record AddressDto(
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country, 
    string State, 
    string ZipCode
    )
{
    public Address ToValueObject()
    {
        return Address.Of(
                    FirstName,
                    LastName,
                    EmailAddress,
                    AddressLine,
                    Country,
                    State,
                    ZipCode);
    }

    public static AddressDto FromAddress(Address address)
    {
        return new AddressDto(
            address.FirstName,
            address.LastName,
            address.EmailAddress!,
            address.AddressLine,
            address.Country,
            address.State,
            address.ZipCode
        );
    }
};
