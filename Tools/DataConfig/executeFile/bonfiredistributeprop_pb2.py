# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: bonfiredistributeprop.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='bonfiredistributeprop.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x1b\x62onfiredistributeprop.proto\x12\nDataConfig\"\x94\x01\n\x18\x42onfireDistributePropCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x10\n\x03num\x18\x02 \x01(\rH\x01\x88\x01\x01\x12#\n\x16\x64istribute_prop_weight\x18\x03 \x01(\x02H\x02\x88\x01\x01\x42\t\n\x07_cfg_idB\x06\n\x04_numB\x19\n\x17_distribute_prop_weight\"\xbf\x01\n\x1d\x42onfireDistributePropCfgSheet\x12H\n\x08item_dic\x18\x01 \x03(\x0b\x32\x36.DataConfig.BonfireDistributePropCfgSheet.ItemDicEntry\x1aT\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12\x33\n\x05value\x18\x02 \x01(\x0b\x32$.DataConfig.BonfireDistributePropCfg:\x02\x38\x01\x62\x06proto3'
)




_BONFIREDISTRIBUTEPROPCFG = _descriptor.Descriptor(
  name='BonfireDistributePropCfg',
  full_name='DataConfig.BonfireDistributePropCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.BonfireDistributePropCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='num', full_name='DataConfig.BonfireDistributePropCfg.num', index=1,
      number=2, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='distribute_prop_weight', full_name='DataConfig.BonfireDistributePropCfg.distribute_prop_weight', index=2,
      number=3, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
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
      name='_cfg_id', full_name='DataConfig.BonfireDistributePropCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_num', full_name='DataConfig.BonfireDistributePropCfg._num',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_distribute_prop_weight', full_name='DataConfig.BonfireDistributePropCfg._distribute_prop_weight',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=44,
  serialized_end=192,
)


_BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.BonfireDistributePropCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.BonfireDistributePropCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.BonfireDistributePropCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=302,
  serialized_end=386,
)

_BONFIREDISTRIBUTEPROPCFGSHEET = _descriptor.Descriptor(
  name='BonfireDistributePropCfgSheet',
  full_name='DataConfig.BonfireDistributePropCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.BonfireDistributePropCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=195,
  serialized_end=386,
)

_BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_cfg_id'].fields.append(
  _BONFIREDISTRIBUTEPROPCFG.fields_by_name['cfg_id'])
_BONFIREDISTRIBUTEPROPCFG.fields_by_name['cfg_id'].containing_oneof = _BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_cfg_id']
_BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_num'].fields.append(
  _BONFIREDISTRIBUTEPROPCFG.fields_by_name['num'])
_BONFIREDISTRIBUTEPROPCFG.fields_by_name['num'].containing_oneof = _BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_num']
_BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_distribute_prop_weight'].fields.append(
  _BONFIREDISTRIBUTEPROPCFG.fields_by_name['distribute_prop_weight'])
_BONFIREDISTRIBUTEPROPCFG.fields_by_name['distribute_prop_weight'].containing_oneof = _BONFIREDISTRIBUTEPROPCFG.oneofs_by_name['_distribute_prop_weight']
_BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _BONFIREDISTRIBUTEPROPCFG
_BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY.containing_type = _BONFIREDISTRIBUTEPROPCFGSHEET
_BONFIREDISTRIBUTEPROPCFGSHEET.fields_by_name['item_dic'].message_type = _BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['BonfireDistributePropCfg'] = _BONFIREDISTRIBUTEPROPCFG
DESCRIPTOR.message_types_by_name['BonfireDistributePropCfgSheet'] = _BONFIREDISTRIBUTEPROPCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

BonfireDistributePropCfg = _reflection.GeneratedProtocolMessageType('BonfireDistributePropCfg', (_message.Message,), {
  'DESCRIPTOR' : _BONFIREDISTRIBUTEPROPCFG,
  '__module__' : 'bonfiredistributeprop_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.BonfireDistributePropCfg)
  })
_sym_db.RegisterMessage(BonfireDistributePropCfg)

BonfireDistributePropCfgSheet = _reflection.GeneratedProtocolMessageType('BonfireDistributePropCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY,
    '__module__' : 'bonfiredistributeprop_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.BonfireDistributePropCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _BONFIREDISTRIBUTEPROPCFGSHEET,
  '__module__' : 'bonfiredistributeprop_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.BonfireDistributePropCfgSheet)
  })
_sym_db.RegisterMessage(BonfireDistributePropCfgSheet)
_sym_db.RegisterMessage(BonfireDistributePropCfgSheet.ItemDicEntry)


_BONFIREDISTRIBUTEPROPCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)
