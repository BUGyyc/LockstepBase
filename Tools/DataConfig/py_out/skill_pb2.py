# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: skill.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='skill.proto',
  package='DataConfig',
  syntax='proto2',
  serialized_options=None,
  serialized_pb=_b('\n\x0bskill.proto\x12\nDataConfig\"\xc9\x01\n\x08SkillCfg\x12\x11\n\x06\x63\x66g_id\x18\x01 \x02(\r:\x01\x30\x12\x14\n\nskill_name\x18\x02 \x02(\t:\x00\x12\x14\n\nskill_desc\x18\x03 \x02(\t:\x00\x12\x0e\n\x04icon\x18\x04 \x01(\t:\x00\x12\x10\n\x05level\x18\x05 \x02(\x05:\x01\x30\x12\x18\n\x0eplaymaker_path\x18\x06 \x02(\t:\x00\x12\x15\n\nskill_type\x18\x07 \x02(\x05:\x01\x30\x12\x13\n\x08skill_cd\x18\x08 \x02(\x05:\x01\x30\x12\x16\n\x0b\x65nergy_cost\x18\t \x01(\x02:\x01\x30\"5\n\x0eSkillCfg_ARRAY\x12#\n\x05items\x18\x01 \x03(\x0b\x32\x14.DataConfig.SkillCfg')
)




_SKILLCFG = _descriptor.Descriptor(
  name='SkillCfg',
  full_name='DataConfig.SkillCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.SkillCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='skill_name', full_name='DataConfig.SkillCfg.skill_name', index=1,
      number=2, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='skill_desc', full_name='DataConfig.SkillCfg.skill_desc', index=2,
      number=3, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='icon', full_name='DataConfig.SkillCfg.icon', index=3,
      number=4, type=9, cpp_type=9, label=1,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='level', full_name='DataConfig.SkillCfg.level', index=4,
      number=5, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='playmaker_path', full_name='DataConfig.SkillCfg.playmaker_path', index=5,
      number=6, type=9, cpp_type=9, label=2,
      has_default_value=True, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='skill_type', full_name='DataConfig.SkillCfg.skill_type', index=6,
      number=7, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='skill_cd', full_name='DataConfig.SkillCfg.skill_cd', index=7,
      number=8, type=5, cpp_type=1, label=2,
      has_default_value=True, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='energy_cost', full_name='DataConfig.SkillCfg.energy_cost', index=8,
      number=9, type=2, cpp_type=6, label=1,
      has_default_value=True, default_value=float(0),
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
  serialized_end=229,
)


_SKILLCFG_ARRAY = _descriptor.Descriptor(
  name='SkillCfg_ARRAY',
  full_name='DataConfig.SkillCfg_ARRAY',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='items', full_name='DataConfig.SkillCfg_ARRAY.items', index=0,
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
  serialized_start=231,
  serialized_end=284,
)

_SKILLCFG_ARRAY.fields_by_name['items'].message_type = _SKILLCFG
DESCRIPTOR.message_types_by_name['SkillCfg'] = _SKILLCFG
DESCRIPTOR.message_types_by_name['SkillCfg_ARRAY'] = _SKILLCFG_ARRAY
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

SkillCfg = _reflection.GeneratedProtocolMessageType('SkillCfg', (_message.Message,), {
  'DESCRIPTOR' : _SKILLCFG,
  '__module__' : 'skill_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.SkillCfg)
  })
_sym_db.RegisterMessage(SkillCfg)

SkillCfg_ARRAY = _reflection.GeneratedProtocolMessageType('SkillCfg_ARRAY', (_message.Message,), {
  'DESCRIPTOR' : _SKILLCFG_ARRAY,
  '__module__' : 'skill_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.SkillCfg_ARRAY)
  })
_sym_db.RegisterMessage(SkillCfg_ARRAY)


# @@protoc_insertion_point(module_scope)
