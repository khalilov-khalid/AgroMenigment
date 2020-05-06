using API_AGROMG.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_AGROMG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<LanguageContext> LanguageContexts { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Workers> Workers { get; set; }

        public DbSet<Profession> Professions { get; set; }
        public DbSet<ProfessionLanguange> ProfessionLanguanges { get; set; }

        public DbSet<WorkerProfessions> WorkerProfessions { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Packet> Packets { get; set; }

        public DbSet<Modules> Modules { get; set; }

        public DbSet<PermissionsGroups> PermissionsGroups { get; set; }

        public DbSet<MainIngredient> MainIngredients { get; set; }

        public DbSet<Fertilizer> Fertilizer { get; set; }

        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }

        public DbSet<MeasurementUnitLanguage> MeasurementUnitLanguage { get; set; }

        public DbSet<WareHouseCategory> WareHouseCategories { get; set; }

        public DbSet<WareHourse> WareHourses { get; set; }

        public DbSet<MedicalStock> MedicalStock { get; set; }

        public DbSet<CropCategory> CropCategories { get; set; }

        public DbSet<CropCategoryLanguage> CropCategoryLanguages { get; set; }

        public DbSet<Crops> Crops { get; set; }

        public DbSet<CropLanguage> CropLanguages { get; set; }

        public DbSet<ParcelCategory> ParcelCategories { get; set; }

        public DbSet<ParcelCategoryLanguage> ParcelCategoryLanguages { get; set; }

        public DbSet<Parcel> Parcels { get; set; }

        public DbSet<Model.Action> Actions { get; set; }

        public DbSet<ActionLanguange> ActionLanguanges { get; set; }

        public DbSet<WorkPlan> WorkPlans { get; set; }

        public DbSet<WorkPlanTask> WorkPlanTasks { get; set; }

        public DbSet<WorkPlanActionLog> WorkPlanActionLogs { get; set; }

        public DbSet<WorkPlanTaskActionLog> WorkPlanTaskActionLogs { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<WorkerSalary> WorkerSalaries { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryOperationKind> TemporaryOperationKind { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryAccountKind> TemporaryAccountKind { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryCustomer> TemporaryCustomer { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryPayAccount> TemporaryPayAccount { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryParcel> TemporaryParcel { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryInAndOutItems> TemporaryInAndOutItems { get; set; }

        public DbSet<API_AGROMG.Model.TemporaryExsel> TemporaryExsel { get; set; }

        public DbSet<API_AGROMG.Model.TemporarySector> TemporarySector { get; set; }

        public DbSet<FertilizerKind> FertilizerKind { get; set; }

        public DbSet<FertilizerKindLanguage> FertilizerKindLanguage { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<CountryLanguage> CountryLanguage { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Demand> Demands { get; set; }





    }
}
