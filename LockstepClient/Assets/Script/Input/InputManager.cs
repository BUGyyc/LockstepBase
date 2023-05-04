using Entitas;
using UnityEngine;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector]
    public Camera mainCamera;
    //[HideInInspector]
    //public CharacterCameraController cameraController;

    /// <summary>
    /// 本地操作角色
    /// </summary>
    public ActorEntity localActor { private set; get; }

    public GameEntity localGameEntity { private set; get; }


    private InputHandler inputHandler;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.transform.gameObject.AddComponent<CharacterCameraController>();

        inputHandler = this.gameObject.AddComponent<InputHandler>();
        inputHandler.Init();

        //InputUtil.HideMouse();
    }


    public void BindLocalActor(ActorEntity entity)
    {
        localActor = entity;
    }

    public void BindGameEntity(GameEntity entity)
    {
        localGameEntity = entity;
    }

    //public void BindGameEntity(uint entityId)
    //{
    //    var entity = Contexts.sharedInstance.game.GetGameEntityWithId(entityId);
    //    localGameEntity = entity;
    //}

    private void Update()
    {

    }

}
