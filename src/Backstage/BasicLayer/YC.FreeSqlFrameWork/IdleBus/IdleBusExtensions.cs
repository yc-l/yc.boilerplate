


namespace YC.FreeSqlFrameWork
{
    public static class IdleBusExtensions
    {
        public static IFreeSql Get(this IdleBus<IFreeSql> ib, long tenantId)
        {
            var freeSql = ib.Get(tenantId.ToString());
            return freeSql;
        }
    }
}
