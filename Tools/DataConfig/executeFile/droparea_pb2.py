# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: droparea.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='droparea.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x0e\x64roparea.proto\x12\nDataConfig\"\xc5\x01\n\x0b\x44ropAreaCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x19\n\x0c\x64rop_area_id\x18\x02 \x01(\rH\x01\x88\x01\x01\x12\x19\n\x0cgroup_cfg_id\x18\x03 \x01(\rH\x02\x88\x01\x01\x12\x11\n\x04name\x18\x04 \x01(\tH\x03\x88\x01\x01\x12\x15\n\x08\x64rop_num\x18\x05 \x01(\rH\x04\x88\x01\x01\x42\t\n\x07_cfg_idB\x0f\n\r_drop_area_idB\x0f\n\r_group_cfg_idB\x07\n\x05_nameB\x0b\n\t_drop_num\"\x98\x01\n\x10\x44ropAreaCfgSheet\x12;\n\x08item_dic\x18\x01 \x03(\x0b\x32).DataConfig.DropAreaCfgSheet.ItemDicEntry\x1aG\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12&\n\x05value\x18\x02 \x01(\x0b\x32\x17.DataConfig.DropAreaCfg:\x02\x38\x01\x62\x06proto3'
)




_DROPAREACFG = _descriptor.Descriptor(
  name='DropAreaCfg',
  full_name='DataConfig.DropAreaCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.DropAreaCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='drop_area_id', full_name='DataConfig.DropAreaCfg.drop_area_id', index=1,
      number=2, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='group_cfg_id', full_name='DataConfig.DropAreaCfg.group_cfg_id', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='name', full_name='DataConfig.DropAreaCfg.name', index=3,
      number=4, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='drop_num', full_name='DataConfig.DropAreaCfg.drop_num', index=4,
      number=5, type=13, cpp_type=3, label=1,
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
      name='_cfg_id', full_name='DataConfig.DropAreaCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_drop_area_id', full_name='DataConfig.DropAreaCfg._drop_area_id',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_group_cfg_id', full_name='DataConfig.DropAreaCfg._group_cfg_id',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_name', full_name='DataConfig.DropAreaCfg._name',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_drop_num', full_name='DataConfig.DropAreaCfg._drop_num',
      index=4, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=31,
  serialized_end=228,
)


_DROPAREACFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.DropAreaCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.DropAreaCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.DropAreaCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=312,
  serialized_end=383,
)

_DROPAREACFGSHEET = _descriptor.Descriptor(
  name='DropAreaCfgSheet',
  full_name='DataConfig.DropAreaCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.DropAreaCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_DROPAREACFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=231,
  serialized_end=383,
)

_DROPAREACFG.oneofs_by_name['_cfg_id'].fields.append(
  _DROPAREACFG.fields_by_name['cfg_id'])
_DROPAREACFG.fields_by_name['cfg_id'].containing_oneof = _DROPAREACFG.oneofs_by_name['_cfg_id']
_DROPAREACFG.oneofs_by_name['_drop_area_id'].fields.append(
  _DROPAREACFG.fields_by_name['drop_area_id'])
_DROPAREACFG.fields_by_name['drop_area_id'].containing_oneof = _DROPAREACFG.oneofs_by_name['_drop_area_id']
_DROPAREACFG.oneofs_by_name['_group_cfg_id'].fields.append(
  _DROPAREACFG.fields_by_name['group_cfg_id'])
_DROPAREACFG.fields_by_name['group_cfg_id'].containing_oneof = _DROPAREACFG.oneofs_by_name['_group_cfg_id']
_DROPAREACFG.oneofs_by_name['_name'].fields.append(
  _DROPAREACFG.fields_by_name['name'])
_DROPAREACFG.fields_by_name['name'].containing_oneof = _DROPAREACFG.oneofs_by_name['_name']
_DROPAREACFG.oneofs_by_name['_drop_num'].fields.append(
  _DROPAREACFG.fields_by_name['drop_num'])
_DROPAREACFG.fields_by_name['drop_num'].containing_oneof = _DROPAREACFG.oneofs_by_name['_drop_num']
_DROPAREACFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _DROPAREACFG
_DROPAREACFGSHEET_ITEMDICENTRY.containing_type = _DROPAREACFGSHEET
_DROPAREACFGSHEET.fields_by_name['item_dic'].message_type = _DROPAREACFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['DropAreaCfg'] = _DROPAREACFG
DESCRIPTOR.message_types_by_name['DropAreaCfgSheet'] = _DROPAREACFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

DropAreaCfg = _reflection.GeneratedProtocolMessageType('DropAreaCfg', (_message.Message,), {
  'DESCRIPTOR' : _DROPAREACFG,
  '__module__' : 'droparea_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.DropAreaCfg)
  })
_sym_db.RegisterMessage(DropAreaCfg)

DropAreaCfgSheet = _reflection.GeneratedProtocolMessageType('DropAreaCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _DROPAREACFGSHEET_ITEMDICENTRY,
    '__module__' : 'droparea_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.DropAreaCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _DROPAREACFGSHEET,
  '__module__' : 'droparea_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.DropAreaCfgSheet)
  })
_sym_db.RegisterMessage(DropAreaCfgSheet)
_sym_db.RegisterMessage(DropAreaCfgSheet.ItemDicEntry)


_DROPAREACFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)