namespace VotingSystemApi.DTO
{
    public class ResponseDTO
    {
        public bool IsPassed { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
