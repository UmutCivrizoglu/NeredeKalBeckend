namespace Core.Entity;

public class Hotel
{
    public Guid Id { get; set; }  
    public string ManagerFirstName { get; set; }  
    public string ManagerLastName { get; set; }  
    public string CompanyName { get; set; }  
    
    public string Address { get; set; }         
    public string City { get; set; }            
    public string Country { get; set; }

    public bool isDeleted { get; set; }

    public ICollection<ContactInformation> ContactInformations { get; set; }
}
