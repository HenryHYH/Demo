using System.Data.Entity.ModelConfiguration;

namespace DbTest
{
    public class ModelConfiguration : EntityTypeConfiguration<Model>
    {
        public ModelConfiguration()
        {
            ToTable("Model");
            HasKey(x => x.Id);
        }
    }
}
