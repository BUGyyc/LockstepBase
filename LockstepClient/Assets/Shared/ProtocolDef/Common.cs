//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Common.proto
namespace Protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Float3")]
  public partial class Float3 : global::ProtoBuf.IExtensible
  {
    public Float3() {}
    

    private float _x = default(float);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float x
    {
      get { return _x; }
      set { _x = value; }
    }

    private float _y = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float y
    {
      get { return _y; }
      set { _y = value; }
    }

    private float _z = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Float4")]
  public partial class Float4 : global::ProtoBuf.IExtensible
  {
    public Float4() {}
    

    private float _x = default(float);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float x
    {
      get { return _x; }
      set { _x = value; }
    }

    private float _y = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float y
    {
      get { return _y; }
      set { _y = value; }
    }

    private float _z = default(float);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float z
    {
      get { return _z; }
      set { _z = value; }
    }

    private float _w = default(float);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"w", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float w
    {
      get { return _w; }
      set { _w = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Int3")]
  public partial class Int3 : global::ProtoBuf.IExtensible
  {
    public Int3() {}
    

    private int _x = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }

    private int _y = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }

    private int _z = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Int4")]
  public partial class Int4 : global::ProtoBuf.IExtensible
  {
    public Int4() {}
    

    private int _x = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }

    private int _y = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }

    private int _z = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int z
    {
      get { return _z; }
      set { _z = value; }
    }

    private int _w = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"w", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int w
    {
      get { return _w; }
      set { _w = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}