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
        Asset_Deploy_Mine,
        Asset_Deploy_Torpedo,
        Asset_Deploy_Drone,
        Asset_Deploy_Sonar,
        Asset_Deploy_Silence,
        Asset_Detonate_Mine,
        Info_Upsert,
        Info_Remove,
    }
}