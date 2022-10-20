# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: global.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='global.proto',
  package='DataConfig',
  syntax='proto2',
  serialized_options=None,
  serialized_pb=_b('\n\x0cglobal.proto\x12\nDataConfig\"a\n\tGlobalCfg\x12\x11\n\x06\x63\x66g_id\x18\x01 \x02(\x05:\x01\x30\x12\x14\n\tvalue_int\x18\x02 \x01(\x05:\x01\x30\x12\x16\n\x0bvalue_float\x18\x03 \x01(\x02:\x01\x30\x12\x13\n\tvalue_str\x18\x04 \x01(\t:\x00\"7\n\x0fGlobalCfg_ARRAY\x12$\n\x05items\x18\x01 \x03(\x0b\x32\x15.DataConfig.GlobalCfg')
)




_GLOBALCFG = _descriptor.Descriptor(
  name='GlobalCfg',
  full_name='DataConfig.GlobalCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.GlobalCfg.cfg_id', index=0,
      number=1, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value_int', full_name='DataConfig.GlobalCfg.value_int', index=1,
      number=2, type=5, cpp_type=1, label=1,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value_float', full_name='DataConfig.GlobalCfg.value_float', index=2,
      number=3, type=2, cpp_type=6, label=1,
      has_default_value=True, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='value_str', full_name='DataConfig.GlobalCfg.value_str', index=3,
      number=4, type=9, cpp_type=9, label=1,
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
  serialized_start=28,
  serialized_end=125,
)


_GLOBALCFG_ARRAY = _descriptor.Descriptor(
  name='GlobalCfg_ARRAY',
  full_name='DataConfig.GlobalCfg_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='DataConfig.GlobalCfg_ARRAY.items', index=0,
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
  serialized_start=127,
  serialized_end=182,
)

_GLOBALCFG_ARRAY.fields_by_name['items'].message_type = _GLOBALCFG
DESCRIPTOR.message_types_by_name['GlobalCfg'] = _GLOBALCFG
DESCRIPTOR.message_types_by_name['GlobalCfg_ARRAY'] = _GLOBALCFG_ARRAY
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

GlobalCfg = _reflection.GeneratedProtocolMessageType('GlobalCfg', (_message.Message,), {
  'DESCRIPTOR' : _GLOBALCFG,
  '__module__' : 'global_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.GlobalCfg)
  })
_sym_db.RegisterMessage(GlobalCfg)

GlobalCfg_ARRAY = _reflection.GeneratedProtocolMessageType('GlobalCfg_ARRAY', (_message.Message,), {
  'DESCRIPTOR' : _GLOBALCFG_ARRAY,
  '__module__' : 'global_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.GlobalCfg_ARRAY)
  })
_sym_db.RegisterMessage(GlobalCfg_ARRAY)


# @@protoc_insertion_point(module_scope)