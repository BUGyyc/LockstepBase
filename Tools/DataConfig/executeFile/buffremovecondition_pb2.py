# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: buffremovecondition.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='buffremovecondition.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x19\x62uffremovecondition.proto\x12\nDataConfig\"\xc6\x01\n\x16\x42uffRemoveConditionCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x11\n\x04name\x18\x02 \x01(\tH\x01\x88\x01\x01\x12\x11\n\x04type\x18\x03 \x01(\rH\x02\x88\x01\x01\x12\x18\n\x0b\x66loat_param\x18\x04 \x01(\x02H\x03\x88\x01\x01\x12\x19\n\x0cstring_param\x18\x05 \x01(\tH\x04\x88\x01\x01\x42\t\n\x07_cfg_idB\x07\n\x05_nameB\x07\n\x05_typeB\x0e\n\x0c_float_paramB\x0f\n\r_string_param\"\xb9\x01\n\x1b\x42uffRemoveConditionCfgSheet\x12\x46\n\x08item_dic\x18\x01 \x03(\x0b\x32\x34.DataConfig.BuffRemoveConditionCfgSheet.ItemDicEntry\x1aR\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12\x31\n\x05value\x18\x02 \x01(\x0b\x32\".DataConfig.BuffRemoveConditionCfg:\x02\x38\x01\x62\x06proto3'
)




_BUFFREMOVECONDITIONCFG = _descriptor.Descriptor(
  name='BuffRemoveConditionCfg',
  full_name='DataConfig.BuffRemoveConditionCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.BuffRemoveConditionCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='name', full_name='DataConfig.BuffRemoveConditionCfg.name', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='type', full_name='DataConfig.BuffRemoveConditionCfg.type', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='float_param', full_name='DataConfig.BuffRemoveConditionCfg.float_param', index=3,
      number=4, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='string_param', full_name='DataConfig.BuffRemoveConditionCfg.string_param', index=4,
      number=5, type=9, cpp_type=9, label=1,
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
      name='_cfg_id', full_name='DataConfig.BuffRemoveConditionCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_name', full_name='DataConfig.BuffRemoveConditionCfg._name',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_type', full_name='DataConfig.BuffRemoveConditionCfg._type',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_float_param', full_name='DataConfig.BuffRemoveConditionCfg._float_param',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_string_param', full_name='DataConfig.BuffRemoveConditionCfg._string_param',
      index=4, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=42,
  serialized_end=240,
)


_BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.BuffRemoveConditionCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.BuffRemoveConditionCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.BuffRemoveConditionCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=346,
  serialized_end=428,
)

_BUFFREMOVECONDITIONCFGSHEET = _descriptor.Descriptor(
  name='BuffRemoveConditionCfgSheet',
  full_name='DataConfig.BuffRemoveConditionCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.BuffRemoveConditionCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=243,
  serialized_end=428,
)

_BUFFREMOVECONDITIONCFG.oneofs_by_name['_cfg_id'].fields.append(
  _BUFFREMOVECONDITIONCFG.fields_by_name['cfg_id'])
_BUFFREMOVECONDITIONCFG.fields_by_name['cfg_id'].containing_oneof = _BUFFREMOVECONDITIONCFG.oneofs_by_name['_cfg_id']
_BUFFREMOVECONDITIONCFG.oneofs_by_name['_name'].fields.append(
  _BUFFREMOVECONDITIONCFG.fields_by_name['name'])
_BUFFREMOVECONDITIONCFG.fields_by_name['name'].containing_oneof = _BUFFREMOVECONDITIONCFG.oneofs_by_name['_name']
_BUFFREMOVECONDITIONCFG.oneofs_by_name['_type'].fields.append(
  _BUFFREMOVECONDITIONCFG.fields_by_name['type'])
_BUFFREMOVECONDITIONCFG.fields_by_name['type'].containing_oneof = _BUFFREMOVECONDITIONCFG.oneofs_by_name['_type']
_BUFFREMOVECONDITIONCFG.oneofs_by_name['_float_param'].fields.append(
  _BUFFREMOVECONDITIONCFG.fields_by_name['float_param'])
_BUFFREMOVECONDITIONCFG.fields_by_name['float_param'].containing_oneof = _BUFFREMOVECONDITIONCFG.oneofs_by_name['_float_param']
_BUFFREMOVECONDITIONCFG.oneofs_by_name['_string_param'].fields.append(
  _BUFFREMOVECONDITIONCFG.fields_by_name['string_param'])
_BUFFREMOVECONDITIONCFG.fields_by_name['string_param'].containing_oneof = _BUFFREMOVECONDITIONCFG.oneofs_by_name['_string_param']
_BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _BUFFREMOVECONDITIONCFG
_BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY.containing_type = _BUFFREMOVECONDITIONCFGSHEET
_BUFFREMOVECONDITIONCFGSHEET.fields_by_name['item_dic'].message_type = _BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['BuffRemoveConditionCfg'] = _BUFFREMOVECONDITIONCFG
DESCRIPTOR.message_types_by_name['BuffRemoveConditionCfgSheet'] = _BUFFREMOVECONDITIONCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

BuffRemoveConditionCfg = _reflection.GeneratedProtocolMessageType('BuffRemoveConditionCfg', (_message.Message,), {
  'DESCRIPTOR' : _BUFFREMOVECONDITIONCFG,
  '__module__' : 'buffremovecondition_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.BuffRemoveConditionCfg)
  })
_sym_db.RegisterMessage(BuffRemoveConditionCfg)

BuffRemoveConditionCfgSheet = _reflection.GeneratedProtocolMessageType('BuffRemoveConditionCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY,
    '__module__' : 'buffremovecondition_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.BuffRemoveConditionCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _BUFFREMOVECONDITIONCFGSHEET,
  '__module__' : 'buffremovecondition_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.BuffRemoveConditionCfgSheet)
  })
_sym_db.RegisterMessage(BuffRemoveConditionCfgSheet)
_sym_db.RegisterMessage(BuffRemoveConditionCfgSheet.ItemDicEntry)


_BUFFREMOVECONDITIONCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)