using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.PackageAgg;
using Query.Package.DTOs;

namespace Query.Package
{
    public static class PackageMapper
    {
        public static PackageDto Map(this Domain.PackageAgg.Package model)
        {
            return new PackageDto()
            {
                Price = model.Price,
                Title = model.Title,
                CreationDate = model.CreationDate,
                Id = model.Id,
                ImageName = model.ImageName,
                Link = model.Link,
                Active = model.Active,
                Specification = model.Specification.MapSpecification()
            };
        }

        internal static List<PackageSpecificationDto?> MapSpecification(this List<PackageSpecification?> model)
        {
            var specification = new List<PackageSpecificationDto>();
            foreach (var item in model)
            {
                var specificationDto = new PackageSpecificationDto()
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    Key = item.Key,
                    Value = item.Value,
                    PackageId = item.PackageId
                };
                specification.Add(specificationDto);
            }
            return specification;
        }
       
    }
}
