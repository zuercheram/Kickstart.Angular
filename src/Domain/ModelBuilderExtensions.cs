using Kickstart.Angular.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kickstart.Angular.Domain;

internal static class ModelBuilderExtensions
{
    internal static void IsTrackingEntity<T>(this EntityTypeBuilder<T> typeBuilder) where T : EntityTrackingBase
    {
        typeBuilder.Property(p => p.Updated)
            .IsConcurrencyToken();
    }
}
