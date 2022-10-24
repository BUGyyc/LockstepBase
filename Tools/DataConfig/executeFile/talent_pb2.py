# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: talent.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='talent.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x0ctalent.proto\x12\nDataConfig\"\x81\x02\n\tTalentCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x11\n\x04name\x18\x02 \x01(\tH\x01\x88\x01\x01\x12\x11\n\x04\x64\x65sc\x18\x03 \x01(\tH\x02\x88\x01\x01\x12\x10\n\x03tag\x18\x04 \x01(\rH\x03\x88\x01\x01\x12\x11\n\x04\x62uff\x18\x05 \x01(\rH\x04\x88\x01\x01\x12\x18\n\x0binsanity_id\x18\x06 \x01(\rH\x05\x88\x01\x01\x12\x12\n\x05owner\x18\x07 \x01(\rH\x06\x88\x01\x01\x12\x13\n\x06pre_id\x18\x08 \x01(\rH\x07\x88\x01\x01\x42\t\n\x07_cfg_idB\x07\n\x05_nameB\x07\n\x05_descB\x06\n\x04_tagB\x07\n\x05_buffB\x0e\n\x0c_insanity_idB\x08\n\x06_ownerB\t\n\x07_pre_id\"\x92\x01\n\x0eTalentCfgSheet\x12\x39\n\x08item_dic\x18\x01 \x03(\x0b\x32\'.DataConfig.TalentCfgSheet.ItemDicEntry\x1a\x45\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12$\n\x05value\x18\x02 \x01(\x0b\x32\x15.DataConfig.TalentCfg:\x02\x38\x01\x62\x06proto3'
)




_TALENTCFG = _descriptor.Descriptor(
  name='TalentCfg',
  full_name='DataConfig.TalentCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.TalentCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='name', full_name='DataConfig.TalentCfg.name', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='desc', full_name='DataConfig.TalentCfg.desc', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='tag', full_name='DataConfig.TalentCfg.tag', index=3,
      number=4, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='buff', full_name='DataConfig.TalentCfg.buff', index=4,
      number=5, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='insanity_id', full_name='DataConfig.TalentCfg.insanity_id', index=5,
      number=6, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='owner', full_name='DataConfig.TalentCfg.owner', index=6,
      number=7, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='pre_id', full_name='DataConfig.TalentCfg.pre_id', index=7,
      number=8, type=13, cpp_type=3, label=1,
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
      name='_cfg_id', full_name='DataConfig.TalentCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_name', full_name='DataConfig.TalentCfg._name',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_desc', full_name='DataConfig.TalentCfg._desc',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_tag', full_name='DataConfig.TalentCfg._tag',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_buff', full_name='DataConfig.TalentCfg._buff',
      index=4, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_insanity_id', full_name='DataConfig.TalentCfg._insanity_id',
      index=5, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_owner', full_name='DataConfig.TalentCfg._owner',
      index=6, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_pre_id', full_name='DataConfig.TalentCfg._pre_id',
      index=7, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=29,
  serialized_end=286,
)


_TALENTCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.TalentCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.TalentCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.TalentCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=366,
  serialized_end=435,
)

_TALENTCFGSHEET = _descriptor.Descriptor(
  name='TalentCfgSheet',
  full_name='DataConfig.TalentCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.TalentCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_TALENTCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=289,
  serialized_end=435,
)

_TALENTCFG.oneofs_by_name['_cfg_id'].fields.append(
  _TALENTCFG.fields_by_name['cfg_id'])
_TALENTCFG.fields_by_name['cfg_id'].containing_oneof = _TALENTCFG.oneofs_by_name['_cfg_id']
_TALENTCFG.oneofs_by_name['_name'].fields.append(
  _TALENTCFG.fields_by_name['name'])
_TALENTCFG.fields_by_name['name'].containing_oneof = _TALENTCFG.oneofs_by_name['_name']
_TALENTCFG.oneofs_by_name['_desc'].fields.append(
  _TALENTCFG.fields_by_name['desc'])
_TALENTCFG.fields_by_name['desc'].containing_oneof = _TALENTCFG.oneofs_by_name['_desc']
_TALENTCFG.oneofs_by_name['_tag'].fields.append(
  _TALENTCFG.fields_by_name['tag'])
_TALENTCFG.fields_by_name['tag'].containing_oneof = _TALENTCFG.oneofs_by_name['_tag']
_TALENTCFG.oneofs_by_name['_buff'].fields.append(
  _TALENTCFG.fields_by_name['buff'])
_TALENTCFG.fields_by_name['buff'].containing_oneof = _TALENTCFG.oneofs_by_name['_buff']
_TALENTCFG.oneofs_by_name['_insanity_id'].fields.append(
  _TALENTCFG.fields_by_name['insanity_id'])
_TALENTCFG.fields_by_name['insanity_id'].containing_oneof = _TALENTCFG.oneofs_by_name['_insanity_id']
_TALENTCFG.oneofs_by_name['_owner'].fields.append(
  _TALENTCFG.fields_by_name['owner'])
_TALENTCFG.fields_by_name['owner'].containing_oneof = _TALENTCFG.oneofs_by_name['_owner']
_TALENTCFG.oneofs_by_name['_pre_id'].fields.append(
  _TALENTCFG.fields_by_name['pre_id'])
_TALENTCFG.fields_by_name['pre_id'].containing_oneof = _TALENTCFG.oneofs_by_name['_pre_id']
_TALENTCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _TALENTCFG
_TALENTCFGSHEET_ITEMDICENTRY.containing_type = _TALENTCFGSHEET
_TALENTCFGSHEET.fields_by_name['item_dic'].message_type = _TALENTCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['TalentCfg'] = _TALENTCFG
DESCRIPTOR.message_types_by_name['TalentCfgSheet'] = _TALENTCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

TalentCfg = _reflection.GeneratedProtocolMessageType('TalentCfg', (_message.Message,), {
  'DESCRIPTOR' : _TALENTCFG,
  '__module__' : 'talent_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.TalentCfg)
  })
_sym_db.RegisterMessage(TalentCfg)

TalentCfgSheet = _reflection.GeneratedProtocolMessageType('TalentCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _TALENTCFGSHEET_ITEMDICENTRY,
    '__module__' : 'talent_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.TalentCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _TALENTCFGSHEET,
  '__module__' : 'talent_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.TalentCfgSheet)
  })
_sym_db.RegisterMessage(TalentCfgSheet)
_sym_db.RegisterMessage(TalentCfgSheet.ItemDicEntry)


_TALENTCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)