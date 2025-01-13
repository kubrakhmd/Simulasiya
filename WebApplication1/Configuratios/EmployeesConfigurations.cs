using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configuratios
{
    public class EmployeesConfigurations:IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");
            builder.Property(a => a.Surname)
                .IsRequired()
                .HasColumnType("varchar(50)");
           
        }
    }
}
