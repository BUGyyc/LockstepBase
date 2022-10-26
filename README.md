
- [Start](#start)
- [参考](#参考)

# Start

初始构建工程需要运行 Link.bat，生成 Server 工程。

这样做主要是为了省事，方便在Unity 内写Server 代码，不然还得开一个单独的C# 工程。

运行游戏需要按照以下步骤：

- 分别打开 LockstepClient 、LockstepServer 两个工程
- LockstepServer 工程打开 LaunchServer 场景，作为房主Server端
- LockstepClient 工程打开 LaunchClient 场景，作为Client

![](Doc/pic.res/20221021114044.png)  

- 设定好IP、端口后，进行链接，进入游戏

![](Doc/pic.res/20221021114904.png)  


---

# 参考


UnityLockstep
<https://github.com/proepkes/UnityLockstep>

Inspired by LockstepFramework, in memory of SnpM:
<https://github.com/SnpM/LockstepFramework>

Uses a fork of BEPUPhysics for deterministic physics:
<https://github.com/sam-vdp/bepuphysics1int>

Uses FixedMath.Net for deterministic fp-calculations:
<https://github.com/asik/FixedMath.Net>

Uses Entitas as ECS Framework:
<https://github.com/sschmid/Entitas-CSharp>

The project includes an implementation example using LiteNetLib:
<https://github.com/RevenantX/LiteNetLib>

Initial commit was targeting the following protocol: https://www.reddit.com/r/Unity3D/comments/aewepu/rts_networking_simulate_on_serverbeat/.  The project has shifted to an architecture you see above.




