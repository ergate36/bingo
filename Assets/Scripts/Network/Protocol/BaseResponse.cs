namespace MarigoldGame.Protocol
{
    public class BaseResponse : BaseProtocol
    {
        protected BaseResponse(MessageType messageType) : base(messageType)
        {
        }

        // 기본 응답
        public string Status { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
