using Microsoft.EntityFrameworkCore;

namespace Api1.Models
{

    public class RelationshipManagementRepository : IRelationship
    {
        private readonly ResidentManagementDbContext _context;

        public RelationshipManagementRepository(ResidentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<ApartmentsOwner> CreateRelationship(ApartmentsOwnerDto relation)
        {
            try
            {
                var new_relation = new ApartmentsOwner();
                new_relation.OwnerId = relation.UpdateOwnerId;
                new_relation.ApartmentId = relation.UpdateApartmentId;
                _context.ApartmentsOwners.Add(new_relation);

                await _context.SaveChangesAsync();

                return new_relation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                throw;
            }
        }

        public async Task<IEnumerable<ApartmentsOwner>> GetAllRelationship()
        {
            return await _context.ApartmentsOwners.ToListAsync();
        }

        public async Task<ApartmentsOwner> RemoveRelation(int id)
        {
            var relation = await _context.ApartmentsOwners.FirstOrDefaultAsync(r => r.Id == id);
            if (relation == null)
            {
                throw new InvalidOperationException("Apartment not found");
            }

            _context.ApartmentsOwners.Remove(relation);
            await _context.SaveChangesAsync();

            return relation;
        }

        public async Task<ApartmentsOwner> ModifyRelation(ApartmentsOwnerDto modifiedRelation)
        {
            var tgrelation = await _context.ApartmentsOwners.//FindAsync(modifiedRelation.OwnerId, modifiedRelation.ApartmentId);
                FirstOrDefaultAsync(r => r.Id == modifiedRelation.Id);
            //var resinfor = await _context.Residents.FirstOrDefaultAsync(r => r.Id == id);


            if (tgrelation == null)
            {
                throw new InvalidOperationException("Resident not found");
            }

            tgrelation.OwnerId = modifiedRelation.UpdateOwnerId;
            tgrelation.ApartmentId = modifiedRelation.UpdateApartmentId;
            _context.ApartmentsOwners.Update(tgrelation);
            await _context.SaveChangesAsync();
            return tgrelation;
        }

        async public async Task<ApartmentsOwner> SearchPropertyOwners(PropertyOwnerDto propertyOwner)
        {
            tgrelationship = _context.ApartmentsOwners.FirstOrDefaultAsync(tgr => tgr.OwnerId == propertyOwner.OwnerId);
        }
    }
}
