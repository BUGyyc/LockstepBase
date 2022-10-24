# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: task.proto
"""Generated protocol buffer code."""
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='task.proto',
  package='DataConfig',
  syntax='proto3',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\ntask.proto\x12\nDataConfig\"\xb9\x04\n\x07TaskCfg\x12\x13\n\x06\x63\x66g_id\x18\x01 \x01(\rH\x00\x88\x01\x01\x12\x1c\n\x0ftask_scope_type\x18\x02 \x01(\rH\x01\x88\x01\x01\x12\x1a\n\robtain_npc_id\x18\x03 \x01(\rH\x02\x88\x01\x01\x12\x18\n\x0bobtain_type\x18\x04 \x01(\rH\x03\x88\x01\x01\x12\x1a\n\robtain_params\x18\x05 \x01(\rH\x04\x88\x01\x01\x12\x14\n\x07\x64rop_id\x18\x06 \x01(\rH\x05\x88\x01\x01\x12\x1a\n\rreward_npc_id\x18\x07 \x01(\rH\x06\x88\x01\x01\x12\x11\n\x04\x64\x65sc\x18\x08 \x01(\tH\x07\x88\x01\x01\x12\x1d\n\x10task_detail_type\x18\t \x01(\rH\x08\x88\x01\x01\x12W\n\x16task_detail_param_list\x18\n \x03(\x0b\x32\x37.DataConfig.TaskCfg.InternalType_task_detail_param_list\x1a]\n#InternalType_task_detail_param_list\x12\x10\n\x08param_id\x18\x01 \x03(\r\x12\x16\n\tparam_num\x18\x02 \x01(\rH\x00\x88\x01\x01\x42\x0c\n\n_param_numB\t\n\x07_cfg_idB\x12\n\x10_task_scope_typeB\x10\n\x0e_obtain_npc_idB\x0e\n\x0c_obtain_typeB\x10\n\x0e_obtain_paramsB\n\n\x08_drop_idB\x10\n\x0e_reward_npc_idB\x07\n\x05_descB\x13\n\x11_task_detail_type\"\x8c\x01\n\x0cTaskCfgSheet\x12\x37\n\x08item_dic\x18\x01 \x03(\x0b\x32%.DataConfig.TaskCfgSheet.ItemDicEntry\x1a\x43\n\x0cItemDicEntry\x12\x0b\n\x03key\x18\x01 \x01(\r\x12\"\n\x05value\x18\x02 \x01(\x0b\x32\x13.DataConfig.TaskCfg:\x02\x38\x01\x62\x06proto3'
)




_TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST = _descriptor.Descriptor(
  name='InternalType_task_detail_param_list',
  full_name='DataConfig.TaskCfg.InternalType_task_detail_param_list',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='param_id', full_name='DataConfig.TaskCfg.InternalType_task_detail_param_list.param_id', index=0,
      number=1, type=13, cpp_type=3, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='param_num', full_name='DataConfig.TaskCfg.InternalType_task_detail_param_list.param_num', index=1,
      number=2, type=13, cpp_type=3, label=1,
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
      name='_param_num', full_name='DataConfig.TaskCfg.InternalType_task_detail_param_list._param_num',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=360,
  serialized_end=453,
)

_TASKCFG = _descriptor.Descriptor(
  name='TaskCfg',
  full_name='DataConfig.TaskCfg',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='cfg_id', full_name='DataConfig.TaskCfg.cfg_id', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='task_scope_type', full_name='DataConfig.TaskCfg.task_scope_type', index=1,
      number=2, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='obtain_npc_id', full_name='DataConfig.TaskCfg.obtain_npc_id', index=2,
      number=3, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='obtain_type', full_name='DataConfig.TaskCfg.obtain_type', index=3,
      number=4, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='obtain_params', full_name='DataConfig.TaskCfg.obtain_params', index=4,
      number=5, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='drop_id', full_name='DataConfig.TaskCfg.drop_id', index=5,
      number=6, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='reward_npc_id', full_name='DataConfig.TaskCfg.reward_npc_id', index=6,
      number=7, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='desc', full_name='DataConfig.TaskCfg.desc', index=7,
      number=8, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=b"".decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='task_detail_type', full_name='DataConfig.TaskCfg.task_detail_type', index=8,
      number=9, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='task_detail_param_list', full_name='DataConfig.TaskCfg.task_detail_param_list', index=9,
      number=10, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
    _descriptor.OneofDescriptor(
      name='_cfg_id', full_name='DataConfig.TaskCfg._cfg_id',
      index=0, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_task_scope_type', full_name='DataConfig.TaskCfg._task_scope_type',
      index=1, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_obtain_npc_id', full_name='DataConfig.TaskCfg._obtain_npc_id',
      index=2, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_obtain_type', full_name='DataConfig.TaskCfg._obtain_type',
      index=3, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_obtain_params', full_name='DataConfig.TaskCfg._obtain_params',
      index=4, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_drop_id', full_name='DataConfig.TaskCfg._drop_id',
      index=5, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_reward_npc_id', full_name='DataConfig.TaskCfg._reward_npc_id',
      index=6, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_desc', full_name='DataConfig.TaskCfg._desc',
      index=7, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
    _descriptor.OneofDescriptor(
      name='_task_detail_type', full_name='DataConfig.TaskCfg._task_detail_type',
      index=8, containing_type=None,
      create_key=_descriptor._internal_create_key,
    fields=[]),
  ],
  serialized_start=27,
  serialized_end=596,
)


_TASKCFGSHEET_ITEMDICENTRY = _descriptor.Descriptor(
  name='ItemDicEntry',
  full_name='DataConfig.TaskCfgSheet.ItemDicEntry',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='key', full_name='DataConfig.TaskCfgSheet.ItemDicEntry.key', index=0,
      number=1, type=13, cpp_type=3, label=1,
      has_default_value=False, default_value=0,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='value', full_name='DataConfig.TaskCfgSheet.ItemDicEntry.value', index=1,
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
  serialized_start=672,
  serialized_end=739,
)

_TASKCFGSHEET = _descriptor.Descriptor(
  name='TaskCfgSheet',
  full_name='DataConfig.TaskCfgSheet',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='item_dic', full_name='DataConfig.TaskCfgSheet.item_dic', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[_TASKCFGSHEET_ITEMDICENTRY, ],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=599,
  serialized_end=739,
)

_TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST.containing_type = _TASKCFG
_TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST.oneofs_by_name['_param_num'].fields.append(
  _TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST.fields_by_name['param_num'])
_TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST.fields_by_name['param_num'].containing_oneof = _TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST.oneofs_by_name['_param_num']
_TASKCFG.fields_by_name['task_detail_param_list'].message_type = _TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST
_TASKCFG.oneofs_by_name['_cfg_id'].fields.append(
  _TASKCFG.fields_by_name['cfg_id'])
_TASKCFG.fields_by_name['cfg_id'].containing_oneof = _TASKCFG.oneofs_by_name['_cfg_id']
_TASKCFG.oneofs_by_name['_task_scope_type'].fields.append(
  _TASKCFG.fields_by_name['task_scope_type'])
_TASKCFG.fields_by_name['task_scope_type'].containing_oneof = _TASKCFG.oneofs_by_name['_task_scope_type']
_TASKCFG.oneofs_by_name['_obtain_npc_id'].fields.append(
  _TASKCFG.fields_by_name['obtain_npc_id'])
_TASKCFG.fields_by_name['obtain_npc_id'].containing_oneof = _TASKCFG.oneofs_by_name['_obtain_npc_id']
_TASKCFG.oneofs_by_name['_obtain_type'].fields.append(
  _TASKCFG.fields_by_name['obtain_type'])
_TASKCFG.fields_by_name['obtain_type'].containing_oneof = _TASKCFG.oneofs_by_name['_obtain_type']
_TASKCFG.oneofs_by_name['_obtain_params'].fields.append(
  _TASKCFG.fields_by_name['obtain_params'])
_TASKCFG.fields_by_name['obtain_params'].containing_oneof = _TASKCFG.oneofs_by_name['_obtain_params']
_TASKCFG.oneofs_by_name['_drop_id'].fields.append(
  _TASKCFG.fields_by_name['drop_id'])
_TASKCFG.fields_by_name['drop_id'].containing_oneof = _TASKCFG.oneofs_by_name['_drop_id']
_TASKCFG.oneofs_by_name['_reward_npc_id'].fields.append(
  _TASKCFG.fields_by_name['reward_npc_id'])
_TASKCFG.fields_by_name['reward_npc_id'].containing_oneof = _TASKCFG.oneofs_by_name['_reward_npc_id']
_TASKCFG.oneofs_by_name['_desc'].fields.append(
  _TASKCFG.fields_by_name['desc'])
_TASKCFG.fields_by_name['desc'].containing_oneof = _TASKCFG.oneofs_by_name['_desc']
_TASKCFG.oneofs_by_name['_task_detail_type'].fields.append(
  _TASKCFG.fields_by_name['task_detail_type'])
_TASKCFG.fields_by_name['task_detail_type'].containing_oneof = _TASKCFG.oneofs_by_name['_task_detail_type']
_TASKCFGSHEET_ITEMDICENTRY.fields_by_name['value'].message_type = _TASKCFG
_TASKCFGSHEET_ITEMDICENTRY.containing_type = _TASKCFGSHEET
_TASKCFGSHEET.fields_by_name['item_dic'].message_type = _TASKCFGSHEET_ITEMDICENTRY
DESCRIPTOR.message_types_by_name['TaskCfg'] = _TASKCFG
DESCRIPTOR.message_types_by_name['TaskCfgSheet'] = _TASKCFGSHEET
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

TaskCfg = _reflection.GeneratedProtocolMessageType('TaskCfg', (_message.Message,), {

  'InternalType_task_detail_param_list' : _reflection.GeneratedProtocolMessageType('InternalType_task_detail_param_list', (_message.Message,), {
    'DESCRIPTOR' : _TASKCFG_INTERNALTYPE_TASK_DETAIL_PARAM_LIST,
    '__module__' : 'task_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.TaskCfg.InternalType_task_detail_param_list)
    })
  ,
  'DESCRIPTOR' : _TASKCFG,
  '__module__' : 'task_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.TaskCfg)
  })
_sym_db.RegisterMessage(TaskCfg)
_sym_db.RegisterMessage(TaskCfg.InternalType_task_detail_param_list)

TaskCfgSheet = _reflection.GeneratedProtocolMessageType('TaskCfgSheet', (_message.Message,), {

  'ItemDicEntry' : _reflection.GeneratedProtocolMessageType('ItemDicEntry', (_message.Message,), {
    'DESCRIPTOR' : _TASKCFGSHEET_ITEMDICENTRY,
    '__module__' : 'task_pb2'
    # @@protoc_insertion_point(class_scope:DataConfig.TaskCfgSheet.ItemDicEntry)
    })
  ,
  'DESCRIPTOR' : _TASKCFGSHEET,
  '__module__' : 'task_pb2'
  # @@protoc_insertion_point(class_scope:DataConfig.TaskCfgSheet)
  })
_sym_db.RegisterMessage(TaskCfgSheet)
_sym_db.RegisterMessage(TaskCfgSheet.ItemDicEntry)


_TASKCFGSHEET_ITEMDICENTRY._options = None
# @@protoc_insertion_point(module_scope)