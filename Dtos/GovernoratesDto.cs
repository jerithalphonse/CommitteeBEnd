using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Dtos
{
    public class Roles
    {
        public int Id { get; set; }
        public bool KiosksTab { get; set; }
        public string Name { get; set; }
        public bool AttendanceTab { get; set; }
        public bool WitnessTab { get; set; }
        public bool MessageTab { get; set; }
        public bool AssignPollingStationTab { get; set; }
        public bool DirectionsTab { get; set; }
        public bool KiosksTabReassign { get; set; }
        public bool AttendanceTabReassign { get; set; }
        public bool ScanQRTab { get; set; }
        public bool WitnessTabAddMore { get; set; }
        public bool ScanQrTabSelfAssignKiosks { get; set; }
        public bool MessageTabAllMembers { get; set; }
        public bool MessageTabHeadCommittees { get; set; }
        public bool MessageTabToHeadCommittee { get; set; }
        public bool MessageTabWaliOfficers { get; set; }
        public bool MessageTabCheckMessage { get; set; }
        public bool MessageTabRestrictMessage { get; set; }
        public bool NotificationTab { get; set; }
        public bool? CountingSoftwareTab { get; set; }
        public bool? VotingTab { get; set; }
    }
    public class GovernoratesDto
	{
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public int SortOrder { get; set; }
    }
    public class WitnessDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int? UploadedBy { get; set; }
        public string UploadedTime { get; set; }
        public string WilayatCode { get; set; }
        public int PollingStationID { get; set; }
      
        [ForeignKey("UploadedBy")]
        public User uploadedByMember { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }

        [ForeignKey("PollingStationID")]
        public PollingStations PollingStation { get; set; }
    }

    public class WilayatsDto
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string GovernorateCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int? RegisteredFemaleVoters { get; set; }
        public int? RegisteredMaleVoters { get; set; }

        [ForeignKey("GovernorateCode")]
        public Governorates Governorate { get; set; }
    }
    public class CountingSoftwareUsersDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string WilayatCode { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
    }

    public class PollingStationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string WilayatCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnifiedPollingCenter { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
    }
    public class KiosksDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int PollingDayStatus { get; set; }
        public System.Nullable<System.DateTime> OpenTime { get; set; }
        public bool HasIssue { get; set; }
        public System.Nullable<System.DateTime> CloseTime { get; set; }
        public string UnlockCode { get; set; }
        public string WilayatCode { get; set; }
        public int PollingStationID { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnifiedKiosk { get; set; }
        public bool AreVotersPresentAsWitnesses { get; set; }
        public bool IsNoFingerprintKiosk { get; set; }
        public string LastRegisteredVoteAt { get; set; }
        public int? NoOfVotes { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
        [ForeignKey("PollingStationID")]
        public PollingStations PollingStation { get; set; }
    }

    public class KiosksAssignDto
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public int? AssignedBy { get; set; }
        public int? KioskId { get; set; }
        public int? PollingStationID { get; set; }
        public string AttendanceStartedAt { get; set; }
        public string AttendanceCompletedAt { get; set; }
        public bool isDeleted { get; set; }

        [ForeignKey("MemberId")]
        public User member { get; set; }
        [ForeignKey("AssignedBy")]
        public User assignedbymember { get; set; }
        [ForeignKey("KioskId")]
        public Kiosks kiosks { get; set; }
        [ForeignKey("PollingStationID")]
        public PollingStations PollingStation { get; set; }

    }

    public class ImagesDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ImagePath { get; set; }
        public int? MemberId { get; set; }
        public int? KioskId { get; set; }
        public int? PollingStationID { get; set; }
        public string UploadedAt { get; set; }
    

        [ForeignKey("MemberId")]
        public User member { get; set; }
        [ForeignKey("KioskId")]
        public Kiosks kiosks { get; set; }
        [ForeignKey("PollingStationID")]
        public PollingStations PollingStation { get; set; }
    }

    public class MessagingDto
    {
        public int Id { get; set; }
        public int? By { get; set; }
        public string To { get; set; }
        public int? ToId { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
        public string WilayatCode { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
        [ForeignKey("By")]
        public User CreatedBy { get; set; }
        [ForeignKey("ToId")]
        public User CreatedToUser { get; set; }
    }
}
