# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: npcproperty.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='npcproperty.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x11npcproperty.proto\x12\nDataConfig\"\xb8\x01\n\x0eNpcPropertyCfg\x12\x1a\n\rNPCPropertyID\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x18\n\x0bNPCProperty\x18\x02 \x01(\tH\x01\x88\x01\x01\x12\x1c\n\x0fNPCPropertyType\x18\x03 \x01(\rH\x02\x88\x01\x01\x12\x12\n\x05Notes\x18\x04 \x01(\tH\x03\x88\x01\x01\x42\x10\n\x0e_NPCPropertyIDB\x0e\n\x0c_NPCPropertyB\x12\n\x10_NPCPropertyTypeB\x08\n\x06_Notes\"\xa1\x01\n\x13NpcPropertyCfgSheet\x12>\n\x08item_dic\x18\x01 \x03(\x0b\x32,.DataConfig.NpcPropertyCfgSheet.ItemDicEntry\x1aJ\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12)\n\x05value\x18\x02 \x01(\x0b\x32\x1a.DataConfig.NpcPropertyCfg:\x02\x38\x01\x62\x06proto3'
)




_NPCPROPERTYCFG = _descriptor.Descriptor(
  name='NpcPropertyCfg',
  full_name='DataConfig.NpcPropertyCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='NPCPropertyID', full_name='DataConfig.NpcPropertyCfg.NPCPropertyID', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='NPCProperty', full_name='DataConfig.NpcPropertyCfg.NPCProperty', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='NPCPropertyType', full_name='DataConfig.NpcPropertyCfg.NPCPropertyType', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='Notes', full_name='DataConfig.NpcPropertyCfg.Notes', index=3,
      number=4, type=9, cpp_type=9, label=1,
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
      name='_NPCPropertyID', full_name='DataConfig.NpcPropertyCfg._NPCPropertyID',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_NPCProperty', full_name='DataConfig.NpcPropertyCfg._NPCProperty',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_NPCPropertyType', full_name='DataConfig.NpcPropertyCfg._NPCPropertyType',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_Notes', full_name='DataConfig.NpcPropertyCfg._Notes',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=34,
  serialized_end=218,
)


_NPCPROPERTYCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.NpcPropertyCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.NpcPropertyCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.NpcPropertyCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=308,
  serialized_end=382,
)

_NPCPROPERTYCFGSHEET = _descriptor.Descriptor(
  name='NpcPropertyCfgSheet',
  full_name='DataConfig.NpcPropertyCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.NpcPropertyCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_NPCPROPERTYCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=221,
  serialized_end=382,
)

_NPCPROPERTYCFG.oneofs_by_name['_NPCPropertyID'].fields.append(
  _NPCPROPERTYCFG.fields_by_name['NPCPropertyID'])
_NPCPROPERTYCFG.fields_by_name['NPCPropertyID'].containing_oneof = _NPCPROPERTYCFG.oneofs_by_name['_NPCPropertyID']
_NPCPROPERTYCFG.oneofs_by_name['_NPCProperty'].fields.append(
  _NPCPROPERTYCFG.fields_by_name['NPCProperty'])
_NPCPROPERTYCFG.fields_by_name['NPCProperty'].containing_oneof = _NPCPROPERTYCFG.oneofs_by_name['_NPCProperty']
_NPCPROPERTYCFG.oneofs_by_name['_NPCPropertyType'].fields.append(
  _NPCPROPERTYCFG.fields_by_name['NPCPropertyType'])
_NPCPROPERTYCFG.fields_by_name['NPCPropertyType'].containing_oneof = _NPCPROPERTYCFG.oneofs_by_name['_NPCPropertyType']
_NPCPROPERTYCFG.oneofs_by_name['_Notes'].fields.append(
  _NPCPROPERTYCFG.fields_by_name['Notes'])
_NPCPROPERTYCFG.fields_by_name['Notes'].containing_oneof = _NPCPROPERTYCFG.oneofs_by_name['_Notes']
_NPCPROPERTYCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _NPCPROPERTYCFG
_NPCPROPERTYCFGSHEET_ITEMDICENTRY.containing_type = _NPCPROPERTYCFGSHEET
_NPCPROPERTYCFGSHEET.fields_by_name['item_dic'].message_type = _NPCPROPERTYCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['NpcPropertyCfg'] = _NPCPROPERTYCFG
DESCRIPTOR.message_types_by_name['NpcPropertyCfgSheet'] = _NPCPROPERTYCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

NpcPropertyCfg = _reflection.GeneratedProtocolMessageType('NpcPropertyCfg', (_message.Message,), {
  'DESCRIPTOR' : _NPCPROPERTYCFG,
  '__module__' : 'npcproperty_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NpcPropertyCfg)
  })
_sym_db.RegisterMessage(NpcPropertyCfg)

NpcPropertyCfgSheet = _reflection.GeneratedProtocolMessageType('NpcPropertyCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _NPCPROPERTYCFGSHEET_ITEMDICENTRY,
    '__module__' : 'npcproperty_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.NpcPropertyCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _NPCPROPERTYCFGSHEET,
  '__module__' : 'npcproperty_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.NpcPropertyCfgSheet)
  })
_sym_db.RegisterMessage(NpcPropertyCfgSheet)
_sym_db.RegisterMessage(NpcPropertyCfgSheet.ItemDicEntry)


_NPCPROPERTYCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)