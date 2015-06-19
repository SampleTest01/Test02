namespace Team3
{
    //画面サイズ
    public static class Screen
    {
        public const int Height = 600;
        public const int Width = 900;
    }
    //その他データ
    public static class GameSetData
    {
        public const int BGSpeed1 = 2;
        public const int BGSpeed2 = 4;

        public const string Title = "03";//タイトル
    }
    //シーン定義

    public enum EScene
    {
        //○=>Title =>△=>Select=>☆GameMain
        //                             ↓
        //                        □Result
        
     

        /*○*/Title,
        /*△*/Select,
        /*☆*/GameMain,Battle,
        /*□*/Result,
    }
}
