namespace Domain.Shared.Business.UniqueIdValueObjects;

public class UniqueIdBase : IUniqueId
{
    public Guid AlternateId { get; set; }

    public UniqueIdBase()
    {
        
    }
    
    public UniqueIdBase(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidOperationException("Id value cannot be empty!");
        }

        AlternateId = value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((UniqueIdBase)obj);
    }

    public override int GetHashCode() => AlternateId.GetHashCode();
    
    public static bool operator ==(UniqueIdBase obj1, UniqueIdBase obj2)
    {
        if (object.Equals(obj1, null))
        {
            if (object.Equals(obj2, null))
            {
                return true;
            }

            return false;
        }

        return obj1.Equals(obj2);
    }

    public static bool operator !=(UniqueIdBase x, UniqueIdBase y)
    {
        return !(x == y);
    }
}