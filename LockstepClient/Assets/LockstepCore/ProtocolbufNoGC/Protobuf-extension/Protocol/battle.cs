//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: battle.proto
// Note: requires additional types generated from: client_common.proto
namespace battle
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"one_cmd")]
  public partial class one_cmd : global::ProtoBuf.IExtensible
  {
    public one_cmd() {}
    
    private int _cmd_id = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"cmd_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int cmd_id
    {
      get { return _cmd_id; }
      set { _cmd_id = value; }
    }
    private uint _UID = (uint)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"UID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint UID
    {
      get { return _UID; }
      set { _UID = value; }
    }
    private byte[] _cmd_data = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"cmd_data", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] cmd_data
    {
      get { return _cmd_data; }
      set { _cmd_data = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ntf_battle_frame_data")]
  public partial class ntf_battle_frame_data : global::ProtoBuf.IExtensible
  {
    public ntf_battle_frame_data() {}
    
    private int _time = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int time
    {
      get { return _time; }
      set { _time = value; }
    }
    private readonly global::System.Collections.Generic.List<battle.ntf_battle_frame_data.one_slot> _slot_list = new global::System.Collections.Generic.List<battle.ntf_battle_frame_data.one_slot>();
    [global::ProtoBuf.ProtoMember(3, Name=@"slot_list", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<battle.ntf_battle_frame_data.one_slot> slot_list
    {
      get { return _slot_list; }
    }
  
    private int _server_from_slot = (int)0;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"server_from_slot", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int server_from_slot
    {
      get { return _server_from_slot; }
      set { _server_from_slot = value; }
    }
    private int _server_to_slot = (int)0;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"server_to_slot", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int server_to_slot
    {
      get { return _server_to_slot; }
      set { _server_to_slot = value; }
    }
    private int _server_curr_frame = (int)0;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"server_curr_frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int server_curr_frame
    {
      get { return _server_curr_frame; }
      set { _server_curr_frame = value; }
    }
    private int _is_check_frame = (int)0;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"is_check_frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int is_check_frame
    {
      get { return _is_check_frame; }
      set { _is_check_frame = value; }
    }
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"cmd_with_frame")]
  public partial class cmd_with_frame : global::ProtoBuf.IExtensible
  {
    public cmd_with_frame() {}
    
    private int _server_frame = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"server_frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int server_frame
    {
      get { return _server_frame; }
      set { _server_frame = value; }
    }
    private battle.one_cmd _cmd = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"cmd", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public battle.one_cmd cmd
    {
      get { return _cmd; }
      set { _cmd = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"one_slot")]
  public partial class one_slot : global::ProtoBuf.IExtensible
  {
    public one_slot() {}
    
    private int _slot = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"slot", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int slot
    {
      get { return _slot; }
      set { _slot = value; }
    }
    private readonly global::System.Collections.Generic.List<battle.ntf_battle_frame_data.cmd_with_frame> _cmd_list = new global::System.Collections.Generic.List<battle.ntf_battle_frame_data.cmd_with_frame>();
    [global::ProtoBuf.ProtoMember(3, Name=@"cmd_list", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<battle.ntf_battle_frame_data.cmd_with_frame> cmd_list
    {
      get { return _cmd_list; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"req_enter_room")]
  public partial class req_enter_room : global::ProtoBuf.IExtensible
  {
    public req_enter_room() {}
    
    private long _battle_id = (long)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"battle_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((long)0)]
    public long battle_id
    {
      get { return _battle_id; }
      set { _battle_id = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _card_list = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"card_list", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> card_list
    {
      get { return _card_list; }
    }
  
    private int _race = (int)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"race", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int race
    {
      get { return _race; }
      set { _race = value; }
    }
    private int _to_battle_trans_1 = (int)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"to_battle_trans_1", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int to_battle_trans_1
    {
      get { return _to_battle_trans_1; }
      set { _to_battle_trans_1 = value; }
    }
    private int _to_battle_trans_2 = (int)0;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"to_battle_trans_2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int to_battle_trans_2
    {
      get { return _to_battle_trans_2; }
      set { _to_battle_trans_2 = value; }
    }
    private int _flag = (int)0;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"flag", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int flag
    {
      get { return _flag; }
      set { _flag = value; }
    }
    private int _start_frame = (int)0;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"start_frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int start_frame
    {
      get { return _start_frame; }
      set { _start_frame = value; }
    }
    private int _region_id = (int)0;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"region_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int region_id
    {
      get { return _region_id; }
      set { _region_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
}