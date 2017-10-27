
static class MonsterCard
{

	public enum CardName
	{
		// MonsterCard 이름 
		card_a_kiki = 0,
		card_b_taro, 
		card_c_moya,
        card_MAX,
	}
	public enum CardOption
	{
		// MonsterCard 옵션
		PLUS_COIN_OPTION = 0,
		BINGO_TIKECT_OPTION,
		COIN_ITEM_OPTION, 
		ATTACK_ITEM_OPTION,
		DAUB_ITEM_OPTION, 
		MAX_OPTION_MAX,
	}

    static public string[] cardSpriteName =
    {
        "kiki",
        "taro",
        "moya",
 
    };

}