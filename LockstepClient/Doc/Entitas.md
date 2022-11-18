

- IReactiveSystem
- BaseMove 与 Animation 分离
- 大量Entitas实测
- 帧同步录像
- 帧同步内确定性物理
- Timeline



---

# ActorID、Id、LocalId 的关系

在整个框架内，我们主要关心 GameEntity 下的 Component，其中有几个 Component 的概念必须分清楚

- ActorID  
- LocalId 
- Id

## ActorIdComponent

相当于阵营，这种Component 在 RTS游戏下比较有作用，可以保留

## IdComponent

意思比较直接，就是单独表示Id，作为一种唯一表示，但是在某些情况就比较特别，例如快照数据

## LocalIdComponent 

这个概念很容易混淆，其实这都源于 快照数据，因为快照数据也会创建GameEntity，并且也是带 IdComponent 的，所以为了区分，那么就需要新的特别Id，也就有了LocalId。如下，创建快照数据：

![](res/20221117180320.png)  



---


# TODO

- [x] EntityType 描述 GameEntity
- [x] Jenny 代码生成器，更快生成 ECS 代码
- [ ] ECS 下，预测、预表现测试
- [ ] 外网网络环境测试
- [x] GameEntityManager 通用逻辑
- [ ] 重构动画导出工具
- [x] HashCode 验证
- [ ] 引入确定性物理
- [ ] 帧同步录像模块
- [ ] 尝试引入Timeline
