namespace MegafonATS.Client.Models
{
    public enum Advaced
    {
        Off,
        MsgBusy,
        Callback
    }

    public enum ClientCallDirection
    {
        In,
        Out
    }

    public enum CallOrder
    {
        All,
        Evenly,
        ByOrder,
        Waterfall
    }

    public enum ClientCallStatus
    {
        Success,
        Missed,
        Cancel,
        Busy,
        NotAvailable,
        NotAllowed,
        NotFound
    }

    public enum FilterCallType
    {
        All,
        In,
        Out,
        Missed
    }

    public enum Period
    {
        Today,
        Yesterday,
        ThisWeek,
        LastWeek,
        ThisMonth,
        LastMonth
    }

    public enum UserRole
    {
        User,
        Admin,
        GroupHead,
        RestrictedUser
    }
}
