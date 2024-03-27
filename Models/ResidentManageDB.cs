using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Api1.Models
{ 
    public class ResidentInfor
    {

        [Key] public int Id { get; set; }
        public string ResidentName { get; set; }
        public int PhoneNumber { get; set; }
        // Navigation property for many-to-many relationship
    }

    public class ApartmentsInfor
    {

        [Key] public int ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public string ApartmentDescription { get; set; }
        public string ApartmentLocation { get; set; }



        // Navigation property for many-to-many relationship
    }

    public class ApartmentsOwner
    {

        // Composite primary key
        [Key] public int Id { get; set; }
        public int? OwnerId { get; set; }

        public int? ApartmentId { get; set; }


    }
}

