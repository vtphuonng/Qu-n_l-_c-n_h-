using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.Common;

namespace Api1.Models
{

    public class ApartmentManagementRepository : IApartmentInforManage
    {
        private readonly ResidentManagementDbContext _context;

        public ApartmentManagementRepository(ResidentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<ApartmentsInfor> CreateApartment(ApartmentsInfor apartments)
        {

            try
            {
                _context.Apartments.Add(apartments);

                await _context.SaveChangesAsync();

                return apartments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                throw;
            }
        }

        public async Task<ApartmentsInfor> GetApartmentById(int id)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(_ => _.ApartmentId == id);
            if (apartment == null)
            {
                throw new InvalidOperationException("Apartment not found");
            };

            //var owningApartmentIds = await _context.ApartmentsOwners
            //    .Where(item => item.OwnerId == id)
            //    .Select(item => item.ApartmentId)
            //    .ToListAsync();

            //var owner = await _context.
            //    .Where(resident => owningApartmentIds.Contains(apartment.ApartmentId))
            //    .ToListAsync();

            // Assuming you want to attach owned apartments to the resident
            return apartment;
        }


        public async Task<IEnumerable<ApartmentsInfor>> GetAllApartment()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task<ApartmentsInfor> RemoveApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                throw new InvalidOperationException("Apartment not found");
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return apartment;
        }

        public async Task<ApartmentsInfor> ModifyApartment(ApartmentsInforDto modifiedApartment)
        {
            var tgapartment = await _context.Apartments.FindAsync(modifiedApartment.ApartmentId);
            //var resinfor = await _context.Residents.FirstOrDefaultAsync(r => r.Id == id);

            if (tgapartment == null)
            {
                throw new InvalidOperationException("Resident not found");
            }

            var entry = _context.Entry(tgapartment);
            await entry.ReloadAsync();

            var props = typeof(ApartmentsInforDto).GetProperties();
            //var tgapartment_props = typeof(ApartmentsInfor).GetProperties();
            foreach (var prop in props)
            {
                var new_value = prop.GetValue(modifiedApartment);
                if (new_value != null)
                {
                    var tgapartment_prop = typeof(ApartmentsInfor).GetProperty(prop.Name);
                    if (tgapartment_prop != null)
                    {
                        var set_value = new_value.ToString();
                        if (!string.IsNullOrEmpty(set_value)){
                            tgapartment_prop.SetValue(tgapartment, new_value);
                        }
                     
                    }
                }
            }

            //tgapartment.ApartmentId = (int)modifiedApartment.ApartmentId;
            //tgapartment.ApartmentName = modifiedApartment.ApartmentName;
            //tgapartment.ApartmentDescription = modifiedApartment.ApartmentDescription;
            //tgapartment.ApartmentLocation = modifiedApartment.ApartmentLocation;

            _context.Apartments.Update(tgapartment);
            await _context.SaveChangesAsync();
            return tgapartment;
        }

    }
}
