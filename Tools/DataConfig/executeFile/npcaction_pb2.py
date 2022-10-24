# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: npcaction.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='npcaction.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x0fnpcaction.proto\x12\nDataConfig\"\\\n\x0cNPCActionCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x11\n\x04name\x18\x02 \x01(\tH\x01\x88\x01\x01\x12\x10\n\x08\x62ind_ids\x18\x03 \x03(\tB\t\n\x07_cfg_idB\x07\n\x05_name\"\x9b\x01\n\x11NPCActionCfgSheet\x12<\n\x08item_dic\x18\x01 \x03(\x0b\x32*.DataConfig.NPCActionCfgSheet.ItemDicEntry\x1aH\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12\'\n\x05value\x18\x02 \x01(\x0b\x32\x18.DataConfig.NPCActionCfg:\x02\x38\x01\x62\x06proto3'
)




_NPCACTIONCFG = _descriptor.Descriptor(
  name='NPCActionCfg',
  full_name='DataConfig.NPCActionCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.NPCActionCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='name', full_name='DataConfig.NPCActionCfg.name', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='bind_ids', full_name='DataConfig.NPCActionCfg.bind_ids', index=2,
      number=3, type=9, cpp_type=9, label=3,
      has_default_value=False, default_value=[],
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
      name='_cfg_id', full_name='DataConfig.NPCActionCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_name', full_name='DataConfig.NPCActionCfg._name',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=31,
  serialized_end=123,
)


_NPCACTIONCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.NPCActionCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.NPCActionCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.NPCActionCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=209,
  serialized_end=281,
)

_NPCACTIONCFGSHEET = _descriptor.Descriptor(
  name='NPCActionCfgSheet',
  full_name='DataConfig.NPCActionCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.NPCActionCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_NPCACTIONCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=126,
  serialized_end=281,
)

_NPCACTIONCFG.oneofs_by_name['_cfg_id'].fields.append(
  _NPCACTIONCFG.fields_by_name['cfg_id'])
_NPCACTIONCFG.fields_by_name['cfg_id'].containing_oneof = _NPCACTIONCFG.oneofs_by_name['_cfg_id']
_NPCACTIONCFG.oneofs_by_name['_name'].fields.append(
  _NPCACTIONCFG.fields_by_name['name'])
_NPCACTIONCFG.fields_by_name['name'].containing_oneof = _NPCACTIONCFG.oneofs_by_name['_name']
_NPCACTIONCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _NPCACTIONCFG
_NPCACTIONCFGSHEET_ITEMDICENTRY.containing_type = _NPCACTIONCFGSHEET
_NPCACTIONCFGSHEET.fields_by_name['item_dic'].message_type = _NPCACTIONCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['NPCActionCfg'] = _NPCACTIONCFG
DESCRIPTOR.message_types_by_name['NPCActionCfgSheet'] = _NPCACTIONCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

NPCActionCfg = _reflection.GeneratedProtocolMessageType('NPCActionCfg', (_message.Message,), {
  'DESCRIPTOR' : _NPCACTIONCFG,
  '__module__' : 'npcaction_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NPCActionCfg)
  })
_sym_db.RegisterMessage(NPCActionCfg)

NPCActionCfgSheet = _reflection.GeneratedProtocolMessageType('NPCActionCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _NPCACTIONCFGSHEET_ITEMDICENTRY,
    '__module__' : 'npcaction_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.NPCActionCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _NPCACTIONCFGSHEET,
  '__module__' : 'npcaction_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NPCActionCfgSheet)
  })
_sym_db.RegisterMessage(NPCActionCfgSheet)
_sym_db.RegisterMessage(NPCActionCfgSheet.ItemDicEntry)


_NPCACTIONCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)