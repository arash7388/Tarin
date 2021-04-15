using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using CodeFirstStoreFunctions;
using Repository.Entity.Map;

namespace Repository.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Reflection;

    using Repository.Data.Migrations;
    using Repository.Entity.Domain;
    using Repository.Entity.Domain.Tashim;

    public class MTOContext : DbContext
    {
       
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Link> Links{ get; set; }
        public DbSet<CategoryProp> CategoryProps { get; set; }
        public DbSet<CategoryPropValue> CategoryPropValues { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementPropValues> AdvertisementPropValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InputOutputDetail> InputOutputDetails { get; set; }
        public DbSet<InputOutput> InputOutput { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessCategory> ProcessCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Worksheet> Worksheets { get; set; }
        public DbSet<WorksheetDetail> WorksheetDetails { get; set; }
        public DbSet<WorkLine> WorkLines { get; set; }
        public DbSet<ReworkReason> ReworkReasons { get; set; }
        public DbSet<Rework> Reworks { get; set; }
        public DbSet<ReworkDetail> ReworkDetails { get; set; }
        public DbSet<Esghat> Esghats { get; set; }
        public DbSet<EsghatDetail> EsghatDetails { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<ShareDivision> ShareDivisions{ get; set; }
        public DbSet<ShareDivisionDetail> ShareDivisionDetails{ get; set; }



        //public MTOContext() :base(ConfigurationManager.ConnectionStrings["Tarin"].ToString())
        //public MTOContext() :base("Data Source=.;Initial Catalog=Tashim;Integrated Security=SSPI;MultipleActiveResultSets=true;")
        public MTOContext() :base("Data Source=DESKTOP-6700VR9\\SQLEXPRESS;Initial Catalog=Tashim;Integrated Security=SSPI;MultipleActiveResultSets=true;")
        //attentionnnnnnnnnnnnnn MultipleActiveResultSets=true should be added 
        //public MTOContext() :base("Tarin")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MTOContext>());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MTOContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var asm = Assembly.GetExecutingAssembly();
            this.LoadEntity(asm, modelBuilder);
            this.LoadEntityConfigurations(asm, modelBuilder);

            modelBuilder.Conventions.Add(new FunctionsConvention<MTOContext>("dbo"));
            modelBuilder.Configurations.Add(new AdvertisementPropValuesMap());
            
            base.OnModelCreating(modelBuilder);
        }

        private void LoadEntity(Assembly asm, DbModelBuilder dbModelBuilder)
        {

            var entityTypes = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               //type.Namespace == "RepositoryPattern.Model.Domain" &&
                               //type.BaseType.IsAbstract &&
                               type.BaseType == typeof(BaseEntity))
                .ToList();

            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");
            entityTypes.ForEach(type =>
                entityMethod.MakeGenericMethod(type).Invoke(dbModelBuilder, new object[] { }));
        }

        private void LoadEntityConfigurations(Assembly asm, DbModelBuilder dbModelBuilder)
        {

            var configurations = asm.GetTypes()
                .Where(type => type.BaseType != null &&
                               //type.Namespace == "RepositoryPattern.Model.Map" &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                .ToList();
            configurations.ForEach(type =>
            {
                dynamic instance = Activator.CreateInstance(type);
                dbModelBuilder.Configurations.Add(instance);
            });
        }

        [DbFunction("MTOContext", "GetAllLeafCategories")]
        public IQueryable<Category> GetAllLeafCategories(string root)
        {
            var rootParameter = root != null ? new ObjectParameter("root", root) : new ObjectParameter("root", typeof(string));

            return ((IObjectContextAdapter)this).ObjectContext
                   .CreateQuery<Category>(
                       string.Format("[{0}].{1}", GetType().Name,
                           "[GetAllLeafCategories](@root)"), rootParameter);
        }

        //public static List<ValidationResult> GetErrorMessage(DbEntityValidationException validationException)
        //{
        //    var validationResults = validationException.EntityValidationErrors;
        //    var errorMessage = GetErrorMessage(validationResults);

        //    return errorMessage;
        //}

        //public static List<ValidationResult> GetErrorMessage(IEnumerable<DbEntityValidationResult> validationResults)
        //{
        //    var validationResult = new List<ValidationResult>();

        //    foreach (var dbEntityValidationResult in validationResults)
        //    {
        //        return GetErrorMessage(dbEntityValidationResult.ValidationErrors);
        //    }

        //    return validationResult;
        //}

        //public static List<ValidationResult> GetErrorMessage(ICollection<DbValidationError> validationErrors)
        //{
        //    List<ValidationResult> errorMessages = (from validationError in validationErrors
        //                                            select new ValidationResult(validationError.ErrorMessage,
        //                                                                        new[] { validationError.PropertyName })).
        //        ToList<ValidationResult>();
        //    return errorMessages;
        //}
    }
}