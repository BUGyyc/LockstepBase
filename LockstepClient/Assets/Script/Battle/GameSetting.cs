using Lockstep;
public static class GameSetting
{
    public const string WalkMove = "WalkMove";
    public const string ReturnIDLE = "WalkMove";

    public const string BulletPrefab = "Assets/Bundles/Prefabs/Bullet.prefab";

    public const string HeroPrefabPath = "Assets/Bundles/Prefabs/ClazyRunner.prefab";//"Prefabs/Hero";

    public const string AIPrefab = "Assets/Bundles/Prefabs/AI.prefab";

    public const string HeroInitAnimationName = "WalkMove";


    public const string HERO_ANIMATOR = "A_Debug_RootMotion";

    public const string ARC_ROTATE_LEFT = "Mvm_Walk_Arc_180_Left";

    public const string ARC_ROTATE_RIGHT = "Mvm_Walk_Arc_180_Right";

    public const int ARC_ROTATE_SPEED = 560 * LFloat.Precision;//每秒旋转的角度范围

    public static readonly LFloat BULLET_SPEED = new LFloat(true, 20000);

    public static readonly LFloat HERO_BASE_SPEED = new LFloat(true, 10000);

    public static readonly LFloat Key_Time = new LFloat(true, 33);


    public static readonly byte AI_ACTOR = 100;
}