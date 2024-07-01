namespace Trips.Models.DTOs;

public class AddClientToTrip
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string PESEL { get; set; }
    public int TripID { get; set; }
    public string TripName { get; set; }
    //„PaymentDate” może posiadać wartość null, dla tych klientów, którzy jeszcze nie zapłacili za podróż.
    public DateTime? PaymentDate { get; set; }
}
