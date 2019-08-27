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

        public string GovernorateCode { get; set; }
        public string WilayatCode { get; set; }
        public int PollingStationId { get; set; }
        public int KioskId { get; set; }
        public int RoleId { get; set; }

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

}