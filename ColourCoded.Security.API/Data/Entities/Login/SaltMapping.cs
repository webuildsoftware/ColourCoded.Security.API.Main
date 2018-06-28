using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColourCoded.Security.API.Data.Entities.Login
{
  public class SaltMapping : IEntityTypeConfiguration<Salt>
  {
    public void Configure(EntityTypeBuilder<Salt> builder)
    {
      builder.ToTable("Salts");

      builder.HasKey("SaltId");
    }
  }
}
