using UnityEngine;

public class GameWorldManager
{
    private static GameWorldManager _instance;
    public static GameWorldManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameWorldManager();
            }
            return _instance;
        }
    }

    public void Awake()
    {

    }

    public void Init()
    {
        //Contexts.sharedInstance.
    }

    void CreateLocalCharacter()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void Update()
    {
#if UNITY_EDITOR

#endif
    }
}
