using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Core.EFCore.Model.Configuration {
    /// <summary>
    /// This class implement a basic EFEntity configuration that can be inherited by base configuration.
    /// Your EF entity configuration class must implement ConfigureEntity method in order for the DbContext to configure them.
    /// </summary>    
    public abstract class EFEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : EFEntity {

        public EFEntityConfiguration() { }

        public void Configure(EntityTypeBuilder<T> builder) {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id");
            builder.Property(t => t.CreatedBy)
                .IsRequired(true)
                .HasColumnName("CreatedBy");
            builder.Property(t => t.CreatedDate)
                .IsRequired(false)
                .HasColumnName("CreatedDate");
            builder.Property(t => t.UpdatedBy)
                .IsRequired(false)
                .HasColumnName("UpdatedBy");
            builder.Property(t => t.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("UpdatedAt");

            ConfigureEntity(builder);
        }

        /// <summary>
        ///  Implement this method for configuring entity
        /// </summary>
        /// <param name="builder"></param>
        public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);

    }
}