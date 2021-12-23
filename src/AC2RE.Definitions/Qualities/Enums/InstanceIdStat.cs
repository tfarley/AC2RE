namespace AC2RE.Definitions;

// Const *_IIDStat
public enum InstanceIdStat : uint {
    Undef = 0, // Undef_IIDStat
    WB_RuntimeID = 1, // WB_RuntimeID_IIDStat

    Owner = 256, // Owner_IIDStat
    Viewer = 257, // Viewer_IIDStat
    Container = 258, // Container_IIDStat
    Bonded = 259, // Bonded_IIDStat
    Freezer = 260, // Freezer_IIDStat
    Equipper = 261, // Equipper_IIDStat
    Viewing = 262, // Viewing_IIDStat
    Attuned = 263, // Attuned_IIDStat
    Author = 264, // Author_IIDStat

    Vendor_Current = 275, // Vendor_Current_IIDStat

    Experience_ForWhom = 280, // Experience_ForWhom_IIDStat

    Gen_Manager = 300, // Gen_Manager_IIDStat
    Gen_PersonalizationTarget = 301, // Gen_PersonalizationTarget_IIDStat
    Crafter = 302, // Crafter_IIDStat

    Plunderer = 320, // Plunderer_IIDStat
    Originator = 321, // Originator_IIDStat
    Claimant = 322, // Claimant_IIDStat
    Last_User = 323, // Last_User_IIDStat

    Fellowship_Recruiter = 400, // Fellowship_Recruiter_IIDStat

    Allegiance_Vassal = 450, // Allegiance_Vassal_IIDStat
    Allegiance_Patron = 451, // Allegiance_Patron_IIDStat
    Allegiance_Monarch = 452, // Allegiance_Monarch_IIDStat
    Allegiance_ProfileTarget = 453, // Allegiance_ProfileTarget_IIDStat

    AI_Master = 500, // AI_Master_IIDStat
    AI_PetMaster = 501, // AI_PetMaster_IIDStat
    AI_PetTarget = 502, // AI_PetTarget_IIDStat
    AI_Fellowship = 503, // AI_Fellowship_IIDStat
    AI_Allegiance = 504, // AI_Allegiance_IIDStat
    AI_PetSummoner = 505, // AI_PetSummoner_IIDStat
    AI_PetFellowshipLeader = 506, // AI_PetFellowshipLeader_IIDStat
    CurrentNPCBeingUsed = 507, // CurrentNPCBeingUsed_IIDStat

    Effect_Summoner = 600, // Effect_Summoner_IIDStat
    Effect_SummonerTarget = 601, // Effect_SummonerTarget_IIDStat
}
