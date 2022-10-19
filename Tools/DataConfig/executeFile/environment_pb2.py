# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: environment.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='environment.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x11\x65nvironment.proto\x12\nDataConfig\"v\n\x0e\x45nvironmentCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x15\n\x08model_id\x18\x02 \x01(\rH\x01\x88\x01\x01\x12\x14\n\x07\x61ttr_id\x18\x03 \x01(\rH\x02\x88\x01\x01\x42\t\n\x07_cfg_idB\x0b\n\t_model_idB\n\n\x08_attr_id\"\xa1\x01\n\x13\x45nvironmentCfgSheet\x12>\n\x08item_dic\x18\x01 \x03(\x0b\x32,.DataConfig.EnvironmentCfgSheet.ItemDicEntry\x1aJ\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12)\n\x05value\x18\x02 \x01(\x0b\x32\x1a.DataConfig.EnvironmentCfg:\x02\x38\x01\x62\x06proto3'
)




_ENVIRONMENTCFG = _descriptor.Descriptor(
  name='EnvironmentCfg',
  full_name='DataConfig.EnvironmentCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.EnvironmentCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='model_id', full_name='DataConfig.EnvironmentCfg.model_id', index=1,
      number=2, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='attr_id', full_name='DataConfig.EnvironmentCfg.attr_id', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
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
      name='_cfg_id', full_name='DataConfig.EnvironmentCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_model_id', full_name='DataConfig.EnvironmentCfg._model_id',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_attr_id', full_name='DataConfig.EnvironmentCfg._attr_id',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=33,
  serialized_end=151,
)


_ENVIRONMENTCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.EnvironmentCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.EnvironmentCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.EnvironmentCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=241,
  serialized_end=315,
)

_ENVIRONMENTCFGSHEET = _descriptor.Descriptor(
  name='EnvironmentCfgSheet',
  full_name='DataConfig.EnvironmentCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.EnvironmentCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_ENVIRONMENTCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=154,
  serialized_end=315,
)

_ENVIRONMENTCFG.oneofs_by_name['_cfg_id'].fields.append(
  _ENVIRONMENTCFG.fields_by_name['cfg_id'])
_ENVIRONMENTCFG.fields_by_name['cfg_id'].containing_oneof = _ENVIRONMENTCFG.oneofs_by_name['_cfg_id']
_ENVIRONMENTCFG.oneofs_by_name['_model_id'].fields.append(
  _ENVIRONMENTCFG.fields_by_name['model_id'])
_ENVIRONMENTCFG.fields_by_name['model_id'].containing_oneof = _ENVIRONMENTCFG.oneofs_by_name['_model_id']
_ENVIRONMENTCFG.oneofs_by_name['_attr_id'].fields.append(
  _ENVIRONMENTCFG.fields_by_name['attr_id'])
_ENVIRONMENTCFG.fields_by_name['attr_id'].containing_oneof = _ENVIRONMENTCFG.oneofs_by_name['_attr_id']
_ENVIRONMENTCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _ENVIRONMENTCFG
_ENVIRONMENTCFGSHEET_ITEMDICENTRY.containing_type = _ENVIRONMENTCFGSHEET
_ENVIRONMENTCFGSHEET.fields_by_name['item_dic'].message_type = _ENVIRONMENTCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['EnvironmentCfg'] = _ENVIRONMENTCFG
DESCRIPTOR.message_types_by_name['EnvironmentCfgSheet'] = _ENVIRONMENTCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

EnvironmentCfg = _reflection.GeneratedProtocolMessageType('EnvironmentCfg', (_message.Message,), {
  'DESCRIPTOR' : _ENVIRONMENTCFG,
  '__module__' : 'environment_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.EnvironmentCfg)
  })
_sym_db.RegisterMessage(EnvironmentCfg)

EnvironmentCfgSheet = _reflection.GeneratedProtocolMessageType('EnvironmentCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _ENVIRONMENTCFGSHEET_ITEMDICENTRY,
    '__module__' : 'environment_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.EnvironmentCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _ENVIRONMENTCFGSHEET,
  '__module__' : 'environment_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.EnvironmentCfgSheet)
  })
_sym_db.RegisterMessage(EnvironmentCfgSheet)
_sym_db.RegisterMessage(EnvironmentCfgSheet.ItemDicEntry)


_ENVIRONMENTCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)
