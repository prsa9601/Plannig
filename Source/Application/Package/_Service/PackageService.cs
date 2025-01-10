using System;
using Domain.PackageAgg.Repository;
using Domain.PackageAgg.Service;

namespace Application.Package._Service
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _repository;

        public PackageService(IPackageRepository repository)
        {
            _repository = repository;
        }

        public bool ExistTitle(string title)
        {
            return _repository.Exists(s => s.Title == title);
        }

    }
}
