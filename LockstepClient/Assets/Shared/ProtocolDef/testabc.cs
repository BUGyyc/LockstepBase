//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: testabc.proto
namespace Protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ABC")]
  public partial class ABC : global::ProtoBuf.IExtensible
  {
    public ABC() {}
    

    private uint _UVal = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"UVal", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint UVal
    {
      get { return _UVal; }
      set { _UVal = value; }
    }

    private int _IVal = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"IVal", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int IVal
    {
      get { return _IVal; }
      set { _IVal = value; }
    }

    private float _FVal = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"FVal", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float FVal
    {
      get { return _FVal; }
      set { _FVal = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}