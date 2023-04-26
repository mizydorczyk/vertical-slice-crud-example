using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Publisher).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.ReleaseDate).IsRequired();
        builder.Property(x => x.AuthorId).IsRequired();
        builder.Property(x => x.GenreId).IsRequired();

        builder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Genre).WithMany(x => x.Books).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Cascade);
    }
}