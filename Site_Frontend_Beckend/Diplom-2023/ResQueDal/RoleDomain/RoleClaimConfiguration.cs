// <copyright file="RoleClaimConfiguration.cs" company="Lime-Tec AG">
// Copyright (C) Lime-Tec AG, Switzerland - All Rights Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResQueDal.RoleDomain;

internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        builder.HasIndex(x => x.RoleId).IsClustered(false);
    }
}