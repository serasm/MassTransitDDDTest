using System;
using Domain.Shared.Business.UniqueIdValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Users.Database.SeedWork;

public class UniqueIdValueConverter<TUniqueId> : ValueConverter<TUniqueId, Guid>
    where TUniqueId : UniqueIdBase
{
    public UniqueIdValueConverter(ConverterMappingHints mappingHints = null)
        : base(id => id.AlternateId, value => Create(value), mappingHints)
    {
        
    }

    private static TUniqueId Create(Guid id) => Activator.CreateInstance(typeof(TUniqueId), id) as TUniqueId;
}