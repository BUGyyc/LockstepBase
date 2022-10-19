using System.Collections.Generic;
using System;
using GameCore;
using Google.Protobuf;

/// <summary>
/// ????????,???????????Tools\DataConfig\Template\DataConfigList.cs
/// </summary>
namespace DataConfig
{
    public class DataConfigList
    {
        public static Dictionary<Type,MessageParser> _configTypeDic;

        public static Dictionary<Type,MessageParser> ConfigTypeDic
        {
            get
            {
                if (_configTypeDic == null)
                {
                    InitList();
                }

                return _configTypeDic;
            }
        }

        private static void InitList()
        {
            _configTypeDic = new Dictionary<Type, MessageParser>();

            //Add Element
        }

		//Add GetMethods
    }
}