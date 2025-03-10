using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace WebApi.Dtos
{
    //Member name, last name, username, password, type, type of committee, governance, wilayats, polling station, kiosk, gender, English and Arabic names.image_url
    public class UserDto
    {
        public int Id { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string CommiteeType { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string AttendedAt { get; set; }

        public string GovernorateCode { get; set; }
        public string WilayatCode { get; set; }
        public int? PollingStationId { get; set; }
        public int? KioskId { get; set; }
        public int? RoleId { get; set; }
        public bool? PasswordChanged { get; set; }

        [ForeignKey("GovernorateCode")]
        public Governorates Governorate { get; set; }
        [ForeignKey("WilayatCode")]
        public Wilayats Wilayat { get; set; }
        [ForeignKey("PollingStationId")]
        public PollingStations PollingStation { get; set; }
        [ForeignKey("KioskId")]
        public Kiosks Kiosks { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
    }

    public class BankDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public int CivilId { get; set; }
        public string CivilIdUrl { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }

    public class ChangePassword
    {
        public int Id { get; set; }
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }

}