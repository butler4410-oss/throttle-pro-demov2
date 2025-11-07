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

    public static class Reports
    {
        /// <summary>
        /// Maximum time a report can execute before timing out (5 minutes)
        /// </summary>
        public const int MaxExecutionTimeSeconds = 300;

        /// <summary>
        /// Maximum number of rows a report can return (prevents memory issues)
        /// </summary>
        public const int MaxResultRowCount = 10000;

        /// <summary>
        /// Default page size for paginated reports
        /// </summary>
        public const int DefaultPageSize = 25;

        /// <summary>
        /// Maximum page size for paginated reports
        /// </summary>
        public const int MaxPageSize = 1000;

        /// <summary>
        /// Maximum file size for exported reports (50 MB)
        /// </summary>
        public const long MaxExportFileSizeBytes = 52428800;

        /// <summary>
        /// Filter operators supported by the report engine
        /// </summary>
        public static class FilterOperators
        {
            public const string Equals = "Equals";
            public const string NotEquals = "NotEquals";
            public const string Contains = "Contains";
            public const string StartsWith = "StartsWith";
            public const string EndsWith = "EndsWith";
            public const string GreaterThan = "GreaterThan";
            public const string GreaterThanOrEqual = "GreaterThanOrEqual";
            public const string LessThan = "LessThan";
            public const string LessThanOrEqual = "LessThanOrEqual";
            public const string Between = "Between";
            public const string In = "In";
            public const string NotIn = "NotIn";
            public const string IsNull = "IsNull";
            public const string IsNotNull = "IsNotNull";
        }

        /// <summary>
        /// Date interval options for grouping date fields
        /// </summary>
        public static class DateIntervals
        {
            public const string Day = "day";
            public const string Week = "week";
            public const string Month = "month";
            public const string Quarter = "quarter";
            public const string Year = "year";
        }
    }
}
