namespace SoftApi.Entity.Application
{
    public partial class MemberEntity
    {
        public long? MemberId { get; set; }
        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public int? ShirtNumber { get; set; }
        public short? BtsideId { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
    }
}
