namespace ThrottlePro.Shared.Enums;

public enum CustomerLifecycleStage
{
    New = 0,           // 0-30 days since first visit
    Active = 1,        // 31-90 days, regular visits
    AtRisk = 2,        // 91-180 days since last visit
    Lapsed = 3,        // 181-365 days since last visit
    Lost = 4           // 365+ days since last visit
}
