namespace Core.Results
{
    public sealed partial class ResponseDataResult<T>
    {
        public IEnumerable<T> ListResponseModel { get; set; } = null;
        public T ResponseModel { get; set; }
        public bool Status { get; set; } = false;
        public string Message { get; set; }
        public long ModelCount { get; set; } = 0;
        public ResponseDataResult()
        {
        }
        public ResponseDataResult(IEnumerable<T> ListResponseModel, bool Status, string Message, long ModelCount)
        {
            this.ListResponseModel = ListResponseModel;
            this.Status = Status;
            this.Message = Message;
            this.ModelCount = ModelCount;
        }
        public ResponseDataResult(T ResponseModel, bool Status, string Message)
        {
            this.ResponseModel = ResponseModel;
            this.Status = Status;
            this.Message = Message;
        }
        public ResponseDataResult( bool Status, string Message)
        {
            this.Status = Status;
            this.Message = Message;
        }
    }
}