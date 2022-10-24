# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: npc.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='npc.proto',
  package='DataConfig',
  syntax='proto2',
  serialized_options=None,
  serialized_pb=_b('\n\tnpc.proto\x12\nDataConfig\"\xc3\x01\n\x06NpcCfg\x12\x11\n\x06\x63\x66g_id\x18\x01 \x02(\r:\x01\x30\x12\x13\n\x08model_id\x18\x02 \x02(\r:\x01\x30\x12\x15\n\rskill_id_list\x18\x03 \x03(\r\x12\x11\n\tattr_list\x18\x04 \x03(\r\x12\x19\n\x0fmove_controller\x18\x05 \x02(\t:\x00\x12\x18\n\thas_dodge\x18\x06 \x01(\x08:\x05\x66\x61lse\x12\x1c\n\x14no_recycle_type_list\x18\x07 \x03(\r\x12\x14\n\thold_coin\x18\x08 \x01(\r:\x01\x30\"1\n\x0cNpcCfg_ARRAY\x12!\n\x05items\x18\x01 \x03(\x0b\x32\x12.DataConfig.NpcCfg')
)




_NPCCFG = _descriptor.Descriptor(
  name='NpcCfg',
  full_name='DataConfig.NpcCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.NpcCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='model_id', full_name='DataConfig.NpcCfg.model_id', index=1,
      number=2, type=13, cpp_type=3, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='skill_id_list', full_name='DataConfig.NpcCfg.skill_id_list', index=2,
      number=3, type=13, cpp_type=3, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='attr_list', full_name='DataConfig.NpcCfg.attr_list', index=3,
      number=4, type=13, cpp_type=3, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='move_controller', full_name='DataConfig.NpcCfg.move_controller', index=4,
      number=5, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='has_dodge', full_name='DataConfig.NpcCfg.has_dodge', index=5,
      number=6, type=8, cpp_type=7, label=1,
      has_default_value=True, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='no_recycle_type_list', full_name='DataConfig.NpcCfg.no_recycle_type_list', index=6,
      number=7, type=13, cpp_type=3, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='hold_coin', full_name='DataConfig.NpcCfg.hold_coin', index=7,
      number=8, type=13, cpp_type=3, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto2',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=26,
  serialized_end=221,
)


_NPCCFG_ARRAY = _descriptor.Descriptor(
  name='NpcCfg_ARRAY',
  full_name='DataConfig.NpcCfg_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='DataConfig.NpcCfg_ARRAY.items', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto2',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=223,
  serialized_end=272,
)

_NPCCFG_ARRAY.fields_by_name['items'].message_type = _NPCCFG
DESCRIPTOR.message_types_by_name['NpcCfg'] = _NPCCFG
DESCRIPTOR.message_types_by_name['NpcCfg_ARRAY'] = _NPCCFG_ARRAY
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

NpcCfg = _reflection.GeneratedProtocolMessageType('NpcCfg', (_message.Message,), {
  'DESCRIPTOR' : _NPCCFG,
  '__module__' : 'npc_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NpcCfg)
  })
_sym_db.RegisterMessage(NpcCfg)

NpcCfg_ARRAY = _reflection.GeneratedProtocolMessageType('NpcCfg_ARRAY', (_message.Message,), {
  'DESCRIPTOR' : _NPCCFG_ARRAY,
  '__module__' : 'npc_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NpcCfg_ARRAY)
  })
_sym_db.RegisterMessage(NpcCfg_ARRAY)


# @@protoc_insertion_point(module_scope)