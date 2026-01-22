namespace DoubleV.Application.DTO
{
    public class BaseResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public bool Succes { get; set; }
        public dynamic Data { get; set; }
        public int totalElements { get; set; }
    }
}
