﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
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
    }
    public class Governorates
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public int SortOrder { get; set; }
    }
    public class Witness
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

    public class Wilayats
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string GovernorateCode { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("GovernorateCode")]
        public Governorates Governorate { get; set; }
    }
    public class PollingStations
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
    public class Kiosks
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int PollingDayStatus { get; set; }
        public string OpenTime { get; set; }
        public bool HasIssue { get; set; }
        public string CloseTime { get; set; }
        public string UnlockCode { get; set; }
        public string WilayatCode { get; set; }
        public int PollingStationID { get; set; }
        public bool IsActive { get; set; }
        public bool IsUnifiedKiosk { get; set; }
        public bool AreVotersPresentAsWitnesses { get; set; }
        public bool IsNoFingerprintKiosk { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
        [ForeignKey("PollingStationID")]
        public PollingStations PollingStation { get; set; }
    }

    public class KiosksAssign
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
    public class Images
    {
        public int Id { get; set; }
        public string Title { get; set; }
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

    public class MessagingModel
    {
        public int Id { get; set; }
        public int? By { get; set; }
        public string To { get; set; }
        public string CreatedAt { get; set; }
        public string Message { get; set; }
        public string WilayatCode { get; set; }

        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
        [ForeignKey("By")]
        public User CreatedBy { get; set; }
    }
}
