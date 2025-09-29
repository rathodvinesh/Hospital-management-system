namespace HMAS.DTO.Response
{
    public class ResponseDTO<T>
    {
        public string Status { get; set; } // "Success", "Failed", etc.
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseDTO() { }

        public ResponseDTO(string status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }

}
