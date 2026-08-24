using IronGridC2Api.Data;

namespace IronGridC2Api.Servise
{
    public class ReportsRepository: IReportsRepository
    {
        private readonly IronGridC2DbContext _DbContext;
        public ReportsRepository(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
