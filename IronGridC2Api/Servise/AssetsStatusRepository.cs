using IronGridC2Api.Data;

namespace IronGridC2Api.Servise
{
    public class AssetsStatusRepository : IAssetsStatusRepository
    {
        private readonly IronGridC2DbContext _DbContext;
        public AssetsStatusRepository(IronGridC2DbContext DbContext)
        {
            _DbContext = DbContext;
        }
    }
}
