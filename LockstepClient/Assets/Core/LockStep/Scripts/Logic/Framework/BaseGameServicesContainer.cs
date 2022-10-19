using Lockstep.Game;
using Lockstep.Game;

public class BaseGameServicesContainer : ServiceContainer {

    /// <summary>
    /// NOTE：主要的服务注册
    /// </summary>
    public BaseGameServicesContainer(){
        RegisterService(new RandomService());
        RegisterService(new CommonStateService());
        RegisterService(new ConstStateService());
        RegisterService(new SimulatorService());
        //接入 LiteNetLib  来处理 Socket 
        RegisterService(new NetworkService());
        RegisterService(new IdService());
        RegisterService(new GameResourceService());
        
        RegisterService(new GameStateService());
        RegisterService(new GameConfigService());
        RegisterService(new GameInputService());
    }
}