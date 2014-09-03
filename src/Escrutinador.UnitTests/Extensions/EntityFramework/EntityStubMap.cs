using Escrutinador.Extensions.EntityFramework;

namespace Escrutinador.UnitTests.Extensions.EntityFramework
{
    public class EntityStubMap : MetadataEntityTypeConfiguration<EntityStub>
    {
        public EntityStubMap()
            : base(new DataAnnotationsMetadataProvider())
        {
            this.ToTable("TableName");
            this.MapMetadata(t => t.UserName);
            this.MapMetadata(t => t.LastModifiedDate);
            this.MapMetadata(t => t.DateKind);
            this.Property(t => t.UserName).HasMaxLength(50).IsRequired();
        }
    }
}
