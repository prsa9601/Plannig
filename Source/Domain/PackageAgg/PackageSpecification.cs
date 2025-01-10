using Common.Domain;
using Common.Domain.Exceptions;

namespace Domain.PackageAgg;

public class PackageSpecification : BaseEntity
{
    public PackageSpecification(string key, string value)
    {
        NullOrEmptyDomainDataException.CheckString(key, nameof(key));
        NullOrEmptyDomainDataException.CheckString(value, nameof(value));

        Key = key;
        Value = value;
    }

    public long PackageId { get; internal set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
}