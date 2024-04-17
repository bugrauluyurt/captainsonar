namespace CaptainSonar.Common.Domain.Commands
{
    public enum CommandName
    {
        Session_Start,
        Session_End,
        Session_Quit,
        Session_Join,
        Session_Invite,
        Map_Move,
        Map_Surface,
        Report_AfterSonar,
        Report_AfterDrone,
        Report_AfterSurface,
        RoomUnit_Damage,
        RoomUnits_Repair,
        RoomUnits_RepairByType,
        Asset_Increase,
        Asset_Use_Mine,
        Info_AddDots,
    }
}