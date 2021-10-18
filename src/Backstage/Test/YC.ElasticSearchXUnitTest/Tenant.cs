using YC.Core;

namespace YC.ElasticSearchXUnitTest
{
    public class TestTenant : ITenant
    {
        public int TenantId { get; set; }
        public string TenantDbString { get; set; }
        public int TenantDbType { get; set; }
    }
}