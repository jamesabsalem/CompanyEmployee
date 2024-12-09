using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContest _repositoryContest;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(RepositoryContest repositoryContest)
        {
            _repositoryContest = repositoryContest;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContest));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContest));
        }

        public ICompanyRepository Company => _companyRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public void Save() => _repositoryContest.SaveChanges();
    }
}
