# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: state.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='state.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x0bstate.proto\x12\nDataConfig\"\xf8\x02\n\x08StateCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x17\n\nmain_state\x18\x02 \x01(\rH\x01\x88\x01\x01\x12\x16\n\tsub_state\x18\x03 \x01(\rH\x02\x88\x01\x01\x12\x19\n\x0c\x64isable_move\x18\x04 \x01(\x08H\x03\x88\x01\x01\x12\x1a\n\rdisable_skill\x18\x05 \x01(\x08H\x04\x88\x01\x01\x12\x1a\n\ris_invincible\x18\x06 \x01(\x08H\x05\x88\x01\x01\x12\x18\n\x0b\x66\x61llback_id\x18\x07 \x01(\rH\x06\x88\x01\x01\x12\x11\n\x04lock\x18\x08 \x01(\x08H\x07\x88\x01\x01\x12\x1c\n\x0f\x64isable_gravity\x18\t \x01(\x08H\x08\x88\x01\x01\x42\t\n\x07_cfg_idB\r\n\x0b_main_stateB\x0c\n\n_sub_stateB\x0f\n\r_disable_moveB\x10\n\x0e_disable_skillB\x10\n\x0e_is_invincibleB\x0e\n\x0c_fallback_idB\x07\n\x05_lockB\x12\n\x10_disable_gravity\"\x8f\x01\n\rStateCfgSheet\x12\x38\n\x08item_dic\x18\x01 \x03(\x0b\x32&.DataConfig.StateCfgSheet.ItemDicEntry\x1a\x44\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12#\n\x05value\x18\x02 \x01(\x0b\x32\x14.DataConfig.StateCfg:\x02\x38\x01\x62\x06proto3'
)




_STATECFG = _descriptor.Descriptor(
  name='StateCfg',
  full_name='DataConfig.StateCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.StateCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='main_state', full_name='DataConfig.StateCfg.main_state', index=1,
      number=2, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='sub_state', full_name='DataConfig.StateCfg.sub_state', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='disable_move', full_name='DataConfig.StateCfg.disable_move', index=3,
      number=4, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='disable_skill', full_name='DataConfig.StateCfg.disable_skill', index=4,
      number=5, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='is_invincible', full_name='DataConfig.StateCfg.is_invincible', index=5,
      number=6, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='fallback_id', full_name='DataConfig.StateCfg.fallback_id', index=6,
      number=7, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='lock', full_name='DataConfig.StateCfg.lock', index=7,
      number=8, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='disable_gravity', full_name='DataConfig.StateCfg.disable_gravity', index=8,
      number=9, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
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
      name='_cfg_id', full_name='DataConfig.StateCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_main_state', full_name='DataConfig.StateCfg._main_state',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_sub_state', full_name='DataConfig.StateCfg._sub_state',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_disable_move', full_name='DataConfig.StateCfg._disable_move',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_disable_skill', full_name='DataConfig.StateCfg._disable_skill',
      index=4, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_is_invincible', full_name='DataConfig.StateCfg._is_invincible',
      index=5, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_fallback_id', full_name='DataConfig.StateCfg._fallback_id',
      index=6, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_lock', full_name='DataConfig.StateCfg._lock',
      index=7, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_disable_gravity', full_name='DataConfig.StateCfg._disable_gravity',
      index=8, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=28,
  serialized_end=404,
)


_STATECFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.StateCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.StateCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.StateCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=482,
  serialized_end=550,
)

_STATECFGSHEET = _descriptor.Descriptor(
  name='StateCfgSheet',
  full_name='DataConfig.StateCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.StateCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_STATECFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=407,
  serialized_end=550,
)

_STATECFG.oneofs_by_name['_cfg_id'].fields.append(
  _STATECFG.fields_by_name['cfg_id'])
_STATECFG.fields_by_name['cfg_id'].containing_oneof = _STATECFG.oneofs_by_name['_cfg_id']
_STATECFG.oneofs_by_name['_main_state'].fields.append(
  _STATECFG.fields_by_name['main_state'])
_STATECFG.fields_by_name['main_state'].containing_oneof = _STATECFG.oneofs_by_name['_main_state']
_STATECFG.oneofs_by_name['_sub_state'].fields.append(
  _STATECFG.fields_by_name['sub_state'])
_STATECFG.fields_by_name['sub_state'].containing_oneof = _STATECFG.oneofs_by_name['_sub_state']
_STATECFG.oneofs_by_name['_disable_move'].fields.append(
  _STATECFG.fields_by_name['disable_move'])
_STATECFG.fields_by_name['disable_move'].containing_oneof = _STATECFG.oneofs_by_name['_disable_move']
_STATECFG.oneofs_by_name['_disable_skill'].fields.append(
  _STATECFG.fields_by_name['disable_skill'])
_STATECFG.fields_by_name['disable_skill'].containing_oneof = _STATECFG.oneofs_by_name['_disable_skill']
_STATECFG.oneofs_by_name['_is_invincible'].fields.append(
  _STATECFG.fields_by_name['is_invincible'])
_STATECFG.fields_by_name['is_invincible'].containing_oneof = _STATECFG.oneofs_by_name['_is_invincible']
_STATECFG.oneofs_by_name['_fallback_id'].fields.append(
  _STATECFG.fields_by_name['fallback_id'])
_STATECFG.fields_by_name['fallback_id'].containing_oneof = _STATECFG.oneofs_by_name['_fallback_id']
_STATECFG.oneofs_by_name['_lock'].fields.append(
  _STATECFG.fields_by_name['lock'])
_STATECFG.fields_by_name['lock'].containing_oneof = _STATECFG.oneofs_by_name['_lock']
_STATECFG.oneofs_by_name['_disable_gravity'].fields.append(
  _STATECFG.fields_by_name['disable_gravity'])
_STATECFG.fields_by_name['disable_gravity'].containing_oneof = _STATECFG.oneofs_by_name['_disable_gravity']
_STATECFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _STATECFG
_STATECFGSHEET_ITEMDICENTRY.containing_type = _STATECFGSHEET
_STATECFGSHEET.fields_by_name['item_dic'].message_type = _STATECFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['StateCfg'] = _STATECFG
DESCRIPTOR.message_types_by_name['StateCfgSheet'] = _STATECFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

StateCfg = _reflection.GeneratedProtocolMessageType('StateCfg', (_message.Message,), {
  'DESCRIPTOR' : _STATECFG,
  '__module__' : 'state_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.StateCfg)
  })
_sym_db.RegisterMessage(StateCfg)

StateCfgSheet = _reflection.GeneratedProtocolMessageType('StateCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _STATECFGSHEET_ITEMDICENTRY,
    '__module__' : 'state_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.StateCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _STATECFGSHEET,
  '__module__' : 'state_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.StateCfgSheet)
  })
_sym_db.RegisterMessage(StateCfgSheet)
_sym_db.RegisterMessage(StateCfgSheet.ItemDicEntry)


_STATECFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)
