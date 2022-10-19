# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: recode.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='recode.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x0crecode.proto\x12\nDataConfig\"Q\n\tReCodeCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\x05H\x00\x88\x01\x01\x12\x16\n\tvalue_str\x18\x02 \x01(\tH\x01\x88\x01\x01\x42\t\n\x07_cfg_idB\x0c\n\n_value_str\"\x92\x01\n\x0eReCodeCfgSheet\x12\x39\n\x08item_dic\x18\x01 \x03(\x0b\x32\'.DataConfig.ReCodeCfgSheet.ItemDicEntry\x1a\x45\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12$\n\x05value\x18\x02 \x01(\x0b\x32\x15.DataConfig.ReCodeCfg:\x02\x38\x01\x62\x06proto3'
)




_RECODECFG = _descriptor.Descriptor(
  name='ReCodeCfg',
  full_name='DataConfig.ReCodeCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.ReCodeCfg.cfg_id', index=0,
      number=1, type=5, cpp_type=1, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value_str', full_name='DataConfig.ReCodeCfg.value_str', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
    _descriptor.OneofDescriptor(
      name='_cfg_id', full_name='DataConfig.ReCodeCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_value_str', full_name='DataConfig.ReCodeCfg._value_str',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=28,
  serialized_end=109,
)


_RECODECFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.ReCodeCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.ReCodeCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.ReCodeCfgSheet.ItemDicEntry.value', index=1,
      number=2, type=11, cpp_type=10, label=1,
      has_default_value=False, default_value=None,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=b'8\001',
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=189,
  serialized_end=258,
)

_RECODECFGSHEET = _descriptor.Descriptor(
  name='ReCodeCfgSheet',
  full_name='DataConfig.ReCodeCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.ReCodeCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_RECODECFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=112,
  serialized_end=258,
)

_RECODECFG.oneofs_by_name['_cfg_id'].fields.append(
  _RECODECFG.fields_by_name['cfg_id'])
_RECODECFG.fields_by_name['cfg_id'].containing_oneof = _RECODECFG.oneofs_by_name['_cfg_id']
_RECODECFG.oneofs_by_name['_value_str'].fields.append(
  _RECODECFG.fields_by_name['value_str'])
_RECODECFG.fields_by_name['value_str'].containing_oneof = _RECODECFG.oneofs_by_name['_value_str']
_RECODECFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _RECODECFG
_RECODECFGSHEET_ITEMDICENTRY.containing_type = _RECODECFGSHEET
_RECODECFGSHEET.fields_by_name['item_dic'].message_type = _RECODECFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['ReCodeCfg'] = _RECODECFG
DESCRIPTOR.message_types_by_name['ReCodeCfgSheet'] = _RECODECFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

ReCodeCfg = _reflection.GeneratedProtocolMessageType('ReCodeCfg', (_message.Message,), {
  'DESCRIPTOR' : _RECODECFG,
  '__module__' : 'recode_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.ReCodeCfg)
  })
_sym_db.RegisterMessage(ReCodeCfg)

ReCodeCfgSheet = _reflection.GeneratedProtocolMessageType('ReCodeCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _RECODECFGSHEET_ITEMDICENTRY,
    '__module__' : 'recode_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.ReCodeCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _RECODECFGSHEET,
  '__module__' : 'recode_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.ReCodeCfgSheet)
  })
_sym_db.RegisterMessage(ReCodeCfgSheet)
_sym_db.RegisterMessage(ReCodeCfgSheet.ItemDicEntry)


_RECODECFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)
