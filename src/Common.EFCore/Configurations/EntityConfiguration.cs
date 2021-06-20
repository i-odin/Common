﻿using Common.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.EFCore.Configurations
{
    //TODO: Test
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.Id).IsUnique();
            //TODO: Добавить генерацию timeStamp 
            //builder.Property(o => o.TimeStamp).HasColumnName("Name").IsRequired();
        }
    }
}
