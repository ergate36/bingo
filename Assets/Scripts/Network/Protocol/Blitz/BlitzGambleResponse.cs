using MarigoldModel.Model;

namespace MarigoldGame.Protocol
{
    public class BlitzGambleResponse : BaseResponse
    {
        public BlitzGambleResponse() : base(MessageType.BlitzGambleResponse)
        {
        }

        // TODO: Command형식으로 통합하는게 어떻지 고민 필요.
        public BaseUserModel RemoveResult { get; set; } // 지워진 Asset 정보. 정확한 클래스는 확인이 필요함.
        public BaseUserModel AddResult { get; set; } // 추가된 Asset 정보. 정확한 클래스는 확인이 필요함.
    }
}
