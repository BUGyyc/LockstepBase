# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: level.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='level.proto',
  package='DataConfig',
  syntax='proto2',
  serialized_options=None,
  serialized_pb=_b('\n\x0blevel.proto\x12\nDataConfig\"=\n\x08LevelCfg\x12\x11\n\x06\x63\x66g_id\x18\x01 \x02(\r:\x01\x30\x12\x0e\n\x04path\x18\x02 \x02(\t:\x00\x12\x0e\n\x04name\x18\x03 \x01(\t:\x00\"5\n\x0eLevelCfg_ARRAY\x12#\n\x05items\x18\x01 \x03(\x0b\x32\x14.DataConfig.LevelCfg')
)




_LEVELCFG = _descriptor.Descriptor(
  name='LevelCfg',
  full_name='DataConfig.LevelCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.LevelCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='path', full_name='DataConfig.LevelCfg.path', index=1,
      number=2, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='name', full_name='DataConfig.LevelCfg.name', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
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
  serialized_start=27,
  serialized_end=88,
)


_LEVELCFG_ARRAY = _descriptor.Descriptor(
  name='LevelCfg_ARRAY',
  full_name='DataConfig.LevelCfg_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='DataConfig.LevelCfg_ARRAY.items', index=0,
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
  serialized_start=90,
  serialized_end=143,
)

_LEVELCFG_ARRAY.fields_by_name['items'].message_type = _LEVELCFG
DESCRIPTOR.message_types_by_name['LevelCfg'] = _LEVELCFG
DESCRIPTOR.message_types_by_name['LevelCfg_ARRAY'] = _LEVELCFG_ARRAY
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

LevelCfg = _reflection.GeneratedProtocolMessageType('LevelCfg', (_message.Message,), {
  'DESCRIPTOR' : _LEVELCFG,
  '__module__' : 'level_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.LevelCfg)
  })
_sym_db.RegisterMessage(LevelCfg)

LevelCfg_ARRAY = _reflection.GeneratedProtocolMessageType('LevelCfg_ARRAY', (_message.Message,), {
  'DESCRIPTOR' : _LEVELCFG_ARRAY,
  '__module__' : 'level_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.LevelCfg_ARRAY)
  })
_sym_db.RegisterMessage(LevelCfg_ARRAY)


# @@protoc_insertion_point(module_scope)