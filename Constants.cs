namespace ThrottlePro.Shared;

public static class Constants
{
    public static class Headers
    {
        public const string ParentId = "X-Parent-Id";
        public const string StoreId = "X-Store-Id";
    }

    public static class Roles
    {
        public const string Admin = "Admin";
        public const string ParentAdmin = "ParentAdmin";
        public const string StoreManager = "StoreManager";
        public const string ReadOnly = "ReadOnly";
    }

    public static class LifecycleDays
    {
        public const int NewMaxDays = 30;
        public const int ActiveMaxDays = 90;
        public const int AtRiskMaxDays = 180;
        public const int LapsedMaxDays = 365;
        // Lost is 365+ days
    }

    public static class CacheKeys
    {
        public const string DashboardSummary = "dashboard_summary_{0}"; // {0} = ParentId
        public const string CustomerList = "customer_list_{0}_{1}"; // {0} = ParentId, {1} = Page
        public const string SegmentList = "segment_list_{0}"; // {0} = ParentId
    }

    public static class Pagination
    {
        public const int DefaultPageSize = 25;
        public const int MaxPageSize = 100;
    }
}
