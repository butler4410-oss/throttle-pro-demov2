namespace ThrottlePro.Shared.Enums;

public enum JourneyTriggerType
{
    SegmentEntry = 0,      // Customer enters a segment
    VisitCompleted = 1,    // Customer completes a visit
    CouponRedeemed = 2,    // Customer redeems a coupon
    LifecycleChange = 3,   // Customer moves to new lifecycle stage
    DateBased = 4,         // Specific date/time trigger
    Abandoned = 5          // Customer abandoned action (cart, booking, etc.)
}
