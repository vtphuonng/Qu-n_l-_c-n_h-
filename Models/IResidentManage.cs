namespace Api1.Models
{
    public interface IResidentInforManage
    {
        Task<ResidentInfor> CreateResident(ResidentInfor reinfor);
        Task<ResidentInfor> GetResidentById(int id);
        Task<IEnumerable<ResidentInfor>> GetAllResident();

        Task<ResidentInfor> RemoveResident(int id);

        Task<ResidentInfor> ModifyResident(ResidentDto modifiedResident);
    }


    public interface IApartmentInforManage
    {
        Task<ApartmentsInfor> CreateApartment(ApartmentsInfor apartinfor);
        Task<ApartmentsInfor> GetApartmentById(int id);
        Task<IEnumerable<ApartmentsInfor>> GetAllApartment();

        Task<ApartmentsInfor> RemoveApartment(int id);

        Task<ApartmentsInfor> ModifyApartment(ApartmentsInforDto modifiedApartment);
    }

    public interface IRelationship
    {
        Task<ApartmentsOwner> CreateRelationship(ApartmentsOwnerDto relationship);
        Task<IEnumerable<ApartmentsOwner>> GetAllRelationship();

        Task<ApartmentsOwner> RemoveRelation(int id);

        Task<ApartmentsOwner> ModifyRelation(ApartmentsOwnerDto modifiedResident);
    }
}
