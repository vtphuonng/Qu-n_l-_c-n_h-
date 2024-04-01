using Microsoft.EntityFrameworkCore;


namespace Api1.Models
{
    public class ResidentManagementRepository : IResidentInforManage
    {
        private readonly ResidentManagementDbContext _context;

        public ResidentManagementRepository(ResidentManagementDbContext context)
        {
            _context = context;
        }


        public async Task<ResidentInfor> CreateResident(ResidentInfor reinfo)
        {
            try
            {
                _context.Residents.Add(reinfo);

                await _context.SaveChangesAsync();

                return reinfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                throw;
            }
        }

        public async Task<ResidentInfor> GetResidentById(int id)
        {
            var resident = await _context.Residents.FirstOrDefaultAsync(_ => _.Id == id);
            if (resident == null)
            {
                throw new InvalidOperationException("Resident not found");
            };

            var owningApartmentIds = await _context.ApartmentsOwners
                .Where(item => item.OwnerId == id)
                .Select(item => item.ApartmentId)
                .ToListAsync();

            var ownedApartments = await _context.Apartments
                .Where(apartment => owningApartmentIds.Contains(apartment.ApartmentId))
                .ToListAsync();

            // Assuming you want to attach owned apartments to the resident
            return resident;
        }


        public async Task<IEnumerable<ResidentInfor>> GetAllResident()
        {
            return await _context.Residents.ToListAsync();
        }


        public async Task<ResidentInfor> RemoveResident(int id)
        {
            var resident = await _context.Residents.FindAsync(id);
            if (resident == null)
            {
                throw new InvalidOperationException("Resident not found");
            }

            _context.Residents.Remove(resident);
            await _context.SaveChangesAsync();

            return resident;
        }

        public async Task<ResidentInfor> ModifyResident(ResidentDto modifiedResident)
        {
            var tgresident = await _context.Residents.FindAsync(modifiedResident.Id);
            //var resinfor = await _context.Residents.FirstOrDefaultAsync(r => r.Id == id);

            if (tgresident == null)
            {
                throw new InvalidOperationException("Resident not found");
            }

            var entry = _context.Entry(tgresident);
            await entry.ReloadAsync();

            var props = typeof(ResidentDto).GetProperties();
            var tgresident_props = typeof(ResidentInfor).GetProperties();
            foreach (var prop in props)
            {
                var new_value = prop.GetValue(modifiedResident);
                if (new_value  != null)
                {
                    var tgresident_prop = tgresident_props.FirstOrDefault(tg => tg.Name == prop.Name);
                    if (tgresident_prop != null && tgresident_prop.PropertyType == prop.PropertyType)
                    {
                        if (prop.PropertyType == typeof(int))
                        {
                            var set_value = (int)new_value;
                            if (set_value != 0)
                            {
                                tgresident_prop.SetValue(tgresident, new_value);
                            }
                        }
                        else
                        {
                            var set_value = new_value.ToString();
                            if (!string.IsNullOrEmpty(set_value))
                            {
                                tgresident_prop.SetValue(tgresident, new_value);
                            }
                          
                        }
                    }
                }
            }
          
            _context.Update(tgresident);
            await _context.SaveChangesAsync();
            return tgresident;
        }

    }
}
