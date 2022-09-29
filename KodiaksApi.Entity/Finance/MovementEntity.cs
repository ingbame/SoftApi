namespace KodiaksApi.Entity.Finance
{
    public class MovementEntity
    {
        public long? MovementId { get; set; }
        public long? MemberId { get; set; }
        public short? MovementTypeId { get; set; }
        public short? ConceptId { get; set; }
        public short? MethodId { get; set; }
        public DateTime? MovementDate { get; set; }
        public decimal? Amount { get; set; }
        public string AdditionalComment { get; set; }
        public string EvidenceUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ByUser { get; set; }
        public long? CreatedBy { get; set; }
    }
}
