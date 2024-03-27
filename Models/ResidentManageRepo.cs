using Api1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            tgresident.PhoneNumber = modifiedResident.PhoneNumber;
            tgresident.ResidentName = modifiedResident.ResidentName;
          
            await _context.SaveChangesAsync();
            return tgresident;
        }

    }
}
