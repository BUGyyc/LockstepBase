// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: proto_pid.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protocol {

  /// <summary>Holder for reflection information generated from proto_pid.proto</summary>
  public static partial class ProtoPidReflection {

    #region Descriptor
    /// <summary>File descriptor for proto_pid.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtoPidReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg9wcm90b19waWQucHJvdG8SCFByb3RvY29sKoMVCgpQcm90b2NvbElEEgwK",
            "CFBJRF9Ob25lEAASDwoLUElEX1BhcnRpYWwQZBIXChJQSURfRW50ZXJCYXR0",
            "bGVSc3AQoR8SFgoRUElEX0xvYWRCYXR0bGVSZXEQox8SFgoRUElEX0xvYWRC",
            "YXR0bGVSc3AQpB8SFQoQUElEX0xvYWRQcm9ncmVzcxClHxIXChJQSURfRW50",
            "ZXJCYXR0bGVOdGYQph8SFAoPUElEX0JhdHRsZVN0YXJ0EKcfEhcKElBJRF9M",
            "ZWF2ZUJhdHRsZVJlcRCqHxIXChJQSURfTGVhdmVCYXR0bGVSc3AQqx8SGgoV",
            "UElEX0xlYXZlQmF0dGxlTm90aWZ5EKwfEhwKF1BJRF9CYXR0bGVHYW1lUmVz",
            "dWx0UmVxELQfEhwKF1BJRF9CYXR0bGVHYW1lUmVzdWx0UnNwELUfEhgKE1BJ",
            "RF9CYXR0bGVFbmROb3RpZnkQ+h8SGwoWUElEX0NyZWF0ZUVudGl0eU5vdGlm",
            "eRCIJxIbChZQSURfRGVsZXRlRW50aXR5Tm90aWZ5EJInEhcKElBJRF9BY3Rp",
            "b25WYU1zZ1JlcRCYJxIXChJQSURfQWN0aW9uVmFNc2dSc3AQmScSFAoPUElE",
            "X1N0YXRlTm90aWZ5EJwnEhEKDFBJRF9TdGF0ZVJlcRCdJxIaChVQSURfQWN0",
            "aW9uRW50aXR5RXZlbnQQpicSEwoOUElEX0FjdGlvbk1vdmUQ7CcSGQoUUElE",
            "X0FjdGlvbk1vdmVOb3RpZnkQ7ScSGQoUUElEX0FjdGlvbkNsaWVudE1vdmUQ",
            "7icSHwoaUElEX0FjdGlvbkNsaWVudE1vdmVOb3RpZnkQ7ycSIAobUElEX0Fj",
            "dGlvblN0YXRlQ2hhbmdlTm90aWZ5EPAnEh4KGVBJRF9DaGFuZ2VUaW1lU2Nh",
            "bGVOb3RpZnkQ8ScSHgoZUElEX0FjdGlvbkV4ZWN1dGVTa2lsbE1zZxDQKBIa",
            "ChVQSURfQWN0aW9uU2tpbGxSZXN1bHQQ1CgSIAobUElEX0FjdGlvblNraWxs",
            "UmVzdWx0Tm90aWZ5ENUoEiEKHFBJRF9BY3Rpb25FeGVjdXRlVGltZWxpbnNN",
            "c2cQ1igSGwoWUElEX0FjdGlvblN0b3BTa2lsbE1zZxDaKBIeChlQSURfQWN0",
            "aW9uU3RvcFNraWxsTm90aWZ5ENsoEhoKFVBJRF9BY3Rpb25BZGRTa2lsbFJl",
            "cRDcKBIaChVQSURfQWN0aW9uQWRkU2tpbGxSc3AQ3SgSHQoYUElEX0FjdGlv",
            "bkRlbGV0ZVNraWxsUmVxEN4oEh0KGFBJRF9BY3Rpb25EZWxldGVTa2lsbFJz",
            "cBDfKBIXChJQSURfU3dpdGNoU2tpbGxNc2cQ5CgSFwoSUElEX0FjdGlvblBp",
            "Y2tQcm9wELQpEh0KGFBJRF9BY3Rpb25QaWNrUHJvcE5vdGlmeRC1KRIZChRQ",
            "SURfQWN0aW9uQnV5UHJvZHVjdBC2KRIaChVQSURfQWN0aW9uU2VsbFByb2R1",
            "Y3QQtykSGAoTUElEX0FjdGlvblVzZUxhZGRlchC+KRIeChlQSURfQWN0aW9u",
            "VXNlTGFkZGVyTm90aWZ5EL8pEhYKEVBJRF9BY3Rpb25Vc2VKdW1wEMIpEhwK",
            "F1BJRF9BY3Rpb25Vc2VKdW1wTm90aWZ5EMMpEh0KGFBJRF9BY3Rpb25Cb25m",
            "aXJlQWRkV29vZBDSKRIcChdQSURfQWN0aW9uQm9uZmlyZUlnbml0ZRDTKRIe",
            "ChlQSURfQm9uZmlyZUJ1cm5SZXdhcmRQcm9wENQpEh4KGVBJRF9BY3Rpb25B",
            "bnRpZG90ZVNjYXR0ZXIQ1SkSGgoVUElEX0FjdGlvblRha2VCb3hQcm9wENwp",
            "Eh0KGFBJRF9BY3Rpb25QdXBwZXRVbnBpbm5lZBDwKRIgChtQSURfQWN0aW9u",
            "UHVwcGV0VW5waW5uZWROdGYQ8SkSEQoMUElEX0Ryb3BQcm9wEJgqEhcKElBJ",
            "RF9Ecm9wUHJvcE5vdGlmeRCZKhIQCgtQSURfVXNlUHJvcBCaKhIRCgxQSURf",
            "U3dhcFNsb3QQmyoSFwoSUElEX0JyZWFrQWRkVG9TbG90EJwqEh0KGFBJRF9Q",
            "bGF5ZXJHZXRDbHVlQW5kRXhpdBCdKhIjCh5QSURfUGxheWVyR2V0Q2x1ZUFu",
            "ZEV4aXROb3RpZnkQnioSFQoQUElEX1RyYW5zZmVyTmVzdBCfKhITCg5QSURf",
            "U2hvd0V4cG9zZRCgKhIUCg9QSURfRGVzdG9yeU5lc3QQoSoSFgoRUElEX0Vu",
            "dGl0eURlYWROdGYQrCoSHQoYUElEX0FjdGlvbkJvYXRNb3ZlTm90aWZ5EPwq",
            "EhMKDlBJRF9BY3RpdmVUYXNrEP0qEhsKFlBJRF9UYXNrQ29tcGxldGVOb3Rp",
            "ZnkQ/ioSEgoNUElEX1Rocm93QmFsbBD/KhIZChRQSURfUGxheUVmZmVjdE5v",
            "dGlmeRCGKxIZChRQSURfSGl0U3VjY2Vzc05vdGlmeRCQKxIhChxQSURfQ2x1",
            "ZUNvbGxlY3RTdWNjZXNzTm90aWZ5EPAuEh0KGFBJRF9DaGFuZ2VFbnRpdHlQ",
            "b3NpdGlvbhDULxIaChVQSURfVGltZWxpbmVBY3Rpb25SZXEQuDASHQoYUElE",
            "X1RpbWVsaW5lQWN0aW9uTm90aWZ5ELkwEh0KGFBJRF9BSVJvdGF0ZVB1dFdh",
            "bGxBbmdsZRDCMBIVChBQSURfQUlSb3RhdGVTaG90EMwwEhsKFlBJRF9Mb2Fk",
            "R2FtZUJ5U3RhdGVSZXEQ1jASHAoXUElEX0FJUm90YXRlVGh1bmRlclNob3QQ",
            "4DASFwoSUElEX0NyZWF0ZUluc2FuaXR5EJwxEhQKD1BJRF9GaXhTdGVwQ29z",
            "dBCAMhIUCg9QSURfSW5wdXRLZXlSZXEQ5DISHAoXUElEX1NraWxsSW50ZXJh",
            "Y3Rpb25Sc3AQ5TISFQoQUElEX0Vycm9yVGlwc1JzcBCXMxIVChBQSURfQ2hh",
            "bmdlVGFnUmVxEMgzEhsKFlBJRF9VcGRhdGVDaGFyYWN0ZXJEaXIQrDQSEwoO",
            "UElEX0NoYW5nZUFuaW0QkDUSFAoPUElEX0Ryb3BQcm9wUmVxEP41EhQKD1BJ",
            "RF9Ecm9wUHJvcFJzcBD/NRIXChJQSURfUGlja0JveFByb3BSZXEQiDYSFwoS",
            "UElEX1BpY2tCb3hQcm9wUnNwEIk2EhQKD1BJRF9DbG9zZUJveFJzcBCKNhIa",
            "ChVQSURfUGlja0dyb3VuZFByb3BSZXEQkjYSFgoRUElEX1BpY2tUYWxlbnRS",
            "ZXEQjy8SGgoVUElEX1BpY2tHcm91bmRQcm9wUnNwEJw2Eh0KGFBJRF9TZXRQ",
            "aWNrVGFyZ2V0UHJvcFJzcBCmNhIbChZQSURfVW5TZXRQaWNrVGFyZ2V0UnNw",
            "EKc2EhIKDVBJRF9HTUNvbW1hbmQQkE4SFwoSUElEX0F0dGFja0JveERlYnVn",
            "EJFOEh0KF1BJRF9GcmFtZ2VudEhlYWRQYWNrYWdlEKCcARIeChhQSURfRnJh",
            "bWdlbnRCbG9ja1BhY2thZ2UQoZwBYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Protocol.ProtocolID), }, null, null));
    }
    #endregion

  }
  #region Enums
  /// <summary>
  /// 网络协议ID定义
  /// </summary>
  public enum ProtocolID {
    [pbr::OriginalName("PID_None")] PidNone = 0,
    [pbr::OriginalName("PID_Partial")] PidPartial = 100,
    /// <summary>
    ///	PID_EnterBattleReq				= 4000;
    /// </summary>
    [pbr::OriginalName("PID_EnterBattleRsp")] PidEnterBattleRsp = 4001,
    [pbr::OriginalName("PID_LoadBattleReq")] PidLoadBattleReq = 4003,
    [pbr::OriginalName("PID_LoadBattleRsp")] PidLoadBattleRsp = 4004,
    [pbr::OriginalName("PID_LoadProgress")] PidLoadProgress = 4005,
    [pbr::OriginalName("PID_EnterBattleNtf")] PidEnterBattleNtf = 4006,
    [pbr::OriginalName("PID_BattleStart")] PidBattleStart = 4007,
    /// <summary>
    ///离开房间
    /// </summary>
    [pbr::OriginalName("PID_LeaveBattleReq")] PidLeaveBattleReq = 4010,
    [pbr::OriginalName("PID_LeaveBattleRsp")] PidLeaveBattleRsp = 4011,
    [pbr::OriginalName("PID_LeaveBattleNotify")] PidLeaveBattleNotify = 4012,
    [pbr::OriginalName("PID_BattleGameResultReq")] PidBattleGameResultReq = 4020,
    /// <summary>
    ///通知展示游戏结算
    /// </summary>
    [pbr::OriginalName("PID_BattleGameResultRsp")] PidBattleGameResultRsp = 4021,
    /// <summary>
    ///游戏结束
    /// </summary>
    [pbr::OriginalName("PID_BattleEndNotify")] PidBattleEndNotify = 4090,
    [pbr::OriginalName("PID_CreateEntityNotify")] PidCreateEntityNotify = 5000,
    [pbr::OriginalName("PID_DeleteEntityNotify")] PidDeleteEntityNotify = 5010,
    [pbr::OriginalName("PID_ActionVaMsgReq")] PidActionVaMsgReq = 5016,
    [pbr::OriginalName("PID_ActionVaMsgRsp")] PidActionVaMsgRsp = 5017,
    /// <summary>
    ///state更新的广播
    /// </summary>
    [pbr::OriginalName("PID_StateNotify")] PidStateNotify = 5020,
    /// <summary>
    ///状态请求
    /// </summary>
    [pbr::OriginalName("PID_StateReq")] PidStateReq = 5021,
    /// <summary>
    ///entity事件
    /// </summary>
    [pbr::OriginalName("PID_ActionEntityEvent")] PidActionEntityEvent = 5030,
    /// <summary>
    ///entity移动
    /// </summary>
    [pbr::OriginalName("PID_ActionMove")] PidActionMove = 5100,
    [pbr::OriginalName("PID_ActionMoveNotify")] PidActionMoveNotify = 5101,
    [pbr::OriginalName("PID_ActionClientMove")] PidActionClientMove = 5102,
    [pbr::OriginalName("PID_ActionClientMoveNotify")] PidActionClientMoveNotify = 5103,
    /// <summary>
    ///用于服务器及时的通知客户端的状态变化		
    /// </summary>
    [pbr::OriginalName("PID_ActionStateChangeNotify")] PidActionStateChangeNotify = 5104,
    /// <summary>
    ///修改时间缩放	
    /// </summary>
    [pbr::OriginalName("PID_ChangeTimeScaleNotify")] PidChangeTimeScaleNotify = 5105,
    [pbr::OriginalName("PID_ActionExecuteSkillMsg")] PidActionExecuteSkillMsg = 5200,
    /// <summary>
    ///PID_ActionExecuteSkillNotify	= 5201;
    ///PID_ActionSkillEvent 			= 5202;//释放技能前进行判定
    ///PID_ActionSkillEventNotify		= 5203;
    /// </summary>
    [pbr::OriginalName("PID_ActionSkillResult")] PidActionSkillResult = 5204,
    [pbr::OriginalName("PID_ActionSkillResultNotify")] PidActionSkillResultNotify = 5205,
    /// <summary>
    ///执行timeline
    /// </summary>
    [pbr::OriginalName("PID_ActionExecuteTimelinsMsg")] PidActionExecuteTimelinsMsg = 5206,
    [pbr::OriginalName("PID_ActionStopSkillMsg")] PidActionStopSkillMsg = 5210,
    [pbr::OriginalName("PID_ActionStopSkillNotify")] PidActionStopSkillNotify = 5211,
    [pbr::OriginalName("PID_ActionAddSkillReq")] PidActionAddSkillReq = 5212,
    [pbr::OriginalName("PID_ActionAddSkillRsp")] PidActionAddSkillRsp = 5213,
    [pbr::OriginalName("PID_ActionDeleteSkillReq")] PidActionDeleteSkillReq = 5214,
    [pbr::OriginalName("PID_ActionDeleteSkillRsp")] PidActionDeleteSkillRsp = 5215,
    [pbr::OriginalName("PID_SwitchSkillMsg")] PidSwitchSkillMsg = 5220,
    [pbr::OriginalName("PID_ActionPickProp")] PidActionPickProp = 5300,
    /// <summary>
    ///捡东西	
    /// </summary>
    [pbr::OriginalName("PID_ActionPickPropNotify")] PidActionPickPropNotify = 5301,
    /// <summary>
    ///买物品
    /// </summary>
    [pbr::OriginalName("PID_ActionBuyProduct")] PidActionBuyProduct = 5302,
    /// <summary>
    ///卖物品
    /// </summary>
    [pbr::OriginalName("PID_ActionSellProduct")] PidActionSellProduct = 5303,
    [pbr::OriginalName("PID_ActionUseLadder")] PidActionUseLadder = 5310,
    /// <summary>
    ///使用连接点
    /// </summary>
    [pbr::OriginalName("PID_ActionUseLadderNotify")] PidActionUseLadderNotify = 5311,
    [pbr::OriginalName("PID_ActionUseJump")] PidActionUseJump = 5314,
    /// <summary>
    ///使用勾爪
    /// </summary>
    [pbr::OriginalName("PID_ActionUseJumpNotify")] PidActionUseJumpNotify = 5315,
    /// <summary>
    /// 添加木炭
    /// </summary>
    [pbr::OriginalName("PID_ActionBonfireAddWood")] PidActionBonfireAddWood = 5330,
    /// <summary>
    /// 点火
    /// </summary>
    [pbr::OriginalName("PID_ActionBonfireIgnite")] PidActionBonfireIgnite = 5331,
    /// <summary>
    /// 奖励道具
    /// </summary>
    [pbr::OriginalName("PID_BonfireBurnRewardProp")] PidBonfireBurnRewardProp = 5332,
    /// <summary>
    /// 解药散布
    /// </summary>
    [pbr::OriginalName("PID_ActionAntidoteScatter")] PidActionAntidoteScatter = 5333,
    /// <summary>
    ///从宝箱中取道具
    /// </summary>
    [pbr::OriginalName("PID_ActionTakeBoxProp")] PidActionTakeBoxProp = 5340,
    /// <summary>
    /// 木偶失衡
    /// </summary>
    [pbr::OriginalName("PID_ActionPuppetUnpinned")] PidActionPuppetUnpinned = 5360,
    /// <summary>
    /// 木偶失衡广播
    /// </summary>
    [pbr::OriginalName("PID_ActionPuppetUnpinnedNtf")] PidActionPuppetUnpinnedNtf = 5361,
    [pbr::OriginalName("PID_DropProp")] PidDropProp = 5400,
    /// <summary>
    ///丢弃物品
    /// </summary>
    [pbr::OriginalName("PID_DropPropNotify")] PidDropPropNotify = 5401,
    /// <summary>
    ///使用物品
    /// </summary>
    [pbr::OriginalName("PID_UseProp")] PidUseProp = 5402,
    /// <summary>
    ///交换槽位
    /// </summary>
    [pbr::OriginalName("PID_SwapSlot")] PidSwapSlot = 5403,
    /// <summary>
    ///打断物品的装备
    /// </summary>
    [pbr::OriginalName("PID_BreakAddToSlot")] PidBreakAddToSlot = 5404,
    /// <summary>
    ///玩家携带至宝准备撤离
    /// </summary>
    [pbr::OriginalName("PID_PlayerGetClueAndExit")] PidPlayerGetClueAndExit = 5405,
    /// <summary>
    ///广播通知
    /// </summary>
    [pbr::OriginalName("PID_PlayerGetClueAndExitNotify")] PidPlayerGetClueAndExitNotify = 5406,
    /// <summary>
    ///传送巢穴
    /// </summary>
    [pbr::OriginalName("PID_TransferNest")] PidTransferNest = 5407,
    /// <summary>
    ///显示暴露
    /// </summary>
    [pbr::OriginalName("PID_ShowExpose")] PidShowExpose = 5408,
    /// <summary>
    ///破坏巢穴
    /// </summary>
    [pbr::OriginalName("PID_DestoryNest")] PidDestoryNest = 5409,
    /// <summary>
    ///玩家死亡广播
    /// </summary>
    [pbr::OriginalName("PID_EntityDeadNtf")] PidEntityDeadNtf = 5420,
    /// <summary>
    ///船移动notify
    /// </summary>
    [pbr::OriginalName("PID_ActionBoatMoveNotify")] PidActionBoatMoveNotify = 5500,
    /// <summary>
    ///领取任务
    /// </summary>
    [pbr::OriginalName("PID_ActiveTask")] PidActiveTask = 5501,
    /// <summary>
    ///任务完成
    /// </summary>
    [pbr::OriginalName("PID_TaskCompleteNotify")] PidTaskCompleteNotify = 5502,
    /// <summary>
    ///扔球
    /// </summary>
    [pbr::OriginalName("PID_ThrowBall")] PidThrowBall = 5503,
    /// <summary>
    ///播放特效
    /// </summary>
    [pbr::OriginalName("PID_PlayEffectNotify")] PidPlayEffectNotify = 5510,
    /// <summary>
    ///攻击击中广播
    /// </summary>
    [pbr::OriginalName("PID_HitSuccessNotify")] PidHitSuccessNotify = 5520,
    /// <summary>
    ///搜集线索成功
    /// </summary>
    [pbr::OriginalName("PID_ClueCollectSuccessNotify")] PidClueCollectSuccessNotify = 6000,
    /// <summary>
    ///移动entity位置，调试用
    /// </summary>
    [pbr::OriginalName("PID_ChangeEntityPosition")] PidChangeEntityPosition = 6100,
    /// <summary>
    ///时间轴请求
    /// </summary>
    [pbr::OriginalName("PID_TimelineActionReq")] PidTimelineActionReq = 6200,
    /// <summary>
    ///时间轴广播
    /// </summary>
    [pbr::OriginalName("PID_TimelineActionNotify")] PidTimelineActionNotify = 6201,
    /// <summary>
    ///旋转放置墙的角度
    /// </summary>
    [pbr::OriginalName("PID_AIRotatePutWallAngle")] PidAirotatePutWallAngle = 6210,
    /// <summary>
    ///射箭和旋转合并
    /// </summary>
    [pbr::OriginalName("PID_AIRotateShot")] PidAirotateShot = 6220,
    /// <summary>
    ///通过State 启动游戏
    /// </summary>
    [pbr::OriginalName("PID_LoadGameByStateReq")] PidLoadGameByStateReq = 6230,
    /// <summary>
    ///旋转雷斩角度
    /// </summary>
    [pbr::OriginalName("PID_AIRotateThunderShot")] PidAirotateThunderShot = 6240,
    /// <summary>
    ///创建蛊灵
    /// </summary>
    [pbr::OriginalName("PID_CreateInsanity")] PidCreateInsanity = 6300,
    /// <summary>
    ///一部分技能需要长按，实时间隔消耗
    /// </summary>
    [pbr::OriginalName("PID_FixStepCost")] PidFixStepCost = 6400,
    /// <summary>
    ///客户端输入
    /// </summary>
    [pbr::OriginalName("PID_InputKeyReq")] PidInputKeyReq = 6500,
    /// <summary>
    ///技能指示器
    /// </summary>
    [pbr::OriginalName("PID_SkillInteractionRsp")] PidSkillInteractionRsp = 6501,
    /// <summary>
    ///tips
    /// </summary>
    [pbr::OriginalName("PID_ErrorTipsRsp")] PidErrorTipsRsp = 6551,
    /// <summary>
    ///修改Tag 
    /// </summary>
    [pbr::OriginalName("PID_ChangeTagReq")] PidChangeTagReq = 6600,
    /// <summary>
    ///修改玩家朝向
    /// </summary>
    [pbr::OriginalName("PID_UpdateCharacterDir")] PidUpdateCharacterDir = 6700,
    /// <summary>
    ///修改玩家动画
    /// </summary>
    [pbr::OriginalName("PID_ChangeAnim")] PidChangeAnim = 6800,
    /// <summary>
    ///丢道具
    /// </summary>
    [pbr::OriginalName("PID_DropPropReq")] PidDropPropReq = 6910,
    /// <summary>
    ///丢道具
    /// </summary>
    [pbr::OriginalName("PID_DropPropRsp")] PidDropPropRsp = 6911,
    /// <summary>
    ///捡盒子道具
    /// </summary>
    [pbr::OriginalName("PID_PickBoxPropReq")] PidPickBoxPropReq = 6920,
    /// <summary>
    ///捡盒子道具
    /// </summary>
    [pbr::OriginalName("PID_PickBoxPropRsp")] PidPickBoxPropRsp = 6921,
    /// <summary>
    ///关闭宝箱
    /// </summary>
    [pbr::OriginalName("PID_CloseBoxRsp")] PidCloseBoxRsp = 6922,
    /// <summary>
    ///捡地上道具
    /// </summary>
    [pbr::OriginalName("PID_PickGroundPropReq")] PidPickGroundPropReq = 6930,
    /// <summary>
    ///选择天赋
    /// </summary>
    [pbr::OriginalName("PID_PickTalentReq")] PidPickTalentReq = 6031,
    /// <summary>
    ///捡地上道具
    /// </summary>
    [pbr::OriginalName("PID_PickGroundPropRsp")] PidPickGroundPropRsp = 6940,
    /// <summary>
    ///设定捡起目标道具
    /// </summary>
    [pbr::OriginalName("PID_SetPickTargetPropRsp")] PidSetPickTargetPropRsp = 6950,
    /// <summary>
    ///取消目标道
    /// </summary>
    [pbr::OriginalName("PID_UnSetPickTargetRsp")] PidUnSetPickTargetRsp = 6951,
    /// <summary>
    ///gm命令
    /// </summary>
    [pbr::OriginalName("PID_GMCommand")] PidGmcommand = 10000,
    /// <summary>
    ///攻击盒显示
    /// </summary>
    [pbr::OriginalName("PID_AttackBoxDebug")] PidAttackBoxDebug = 10001,
    [pbr::OriginalName("PID_FramgentHeadPackage")] PidFramgentHeadPackage = 20000,
    [pbr::OriginalName("PID_FramgentBlockPackage")] PidFramgentBlockPackage = 20001,
  }

  #endregion

}

#endregion Designer generated code