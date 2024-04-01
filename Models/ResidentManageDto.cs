namespace Api1.Models
{
    public class ResidentDto
    {
        public int Id { get; set; }
        public string ResidentName { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class ApartmentsInforDto
    {

        public int ApartmentId { get; set; }
        public string  ApartmentName { get; set; }
        public string  ApartmentDescription { get; set; }
        public string  ApartmentLocation { get; set; }
    }

    public class ApartmentsOwnerDto
    {

        public int? Id { get; set; }

        public int? UpdateOwnerId { get; set; }

        public int? UpdateApartmentId { get; set; }
    }

    public class PropertyOwnerDto
    {
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhonenumber { get; set; }
        public List<PropertyOwnerDto> PropertyOwnerList
        {
            get; set;
        }
    }
}
