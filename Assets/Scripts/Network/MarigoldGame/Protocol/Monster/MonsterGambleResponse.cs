using MarigoldModel.Commands;

namespace MarigoldGame.Protocol
{
    public class MonsterGambleResponse : BaseResponse
    {
        public RemoveAssetCommand RemoveAssetCommand { get; set; } // 지워진 Asset 정보. 정확한 클래스는 확인이 필요함.
        public AddAssetCommand AddAssetCommand { get; set; } // 추가된 Asset 정보. 정확한 클래스는 확인이 필요함.

        public MonsterGambleResponse() : base(MessageType.MonsterGambleResponse)
        {
        }
    }
}
