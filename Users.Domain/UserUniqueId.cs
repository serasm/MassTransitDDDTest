using System;
using System.Text.Json.Serialization;
using Domain.Shared.Business.UniqueIdValueObjects;

namespace Users.Domain;

public class UserUniqueId : UniqueIdBase
{
    public UserUniqueId(Guid alternateId) : base(alternateId)
    {
    }

    public UserUniqueId()
    {
        
    }
}