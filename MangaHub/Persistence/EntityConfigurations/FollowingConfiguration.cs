﻿using MangaHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace MangaHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration : EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            Property(c => c.FolloweeId)
                .HasColumnOrder(0);
            Property(c => c.FollowerId)
                .HasColumnOrder(1);

            HasRequired(c => c.Follower)
                .WithMany()
                .WillCascadeOnDelete(false);
                
            HasKey(c => new { c.FolloweeId, c.FollowerId });
        }
    }
}