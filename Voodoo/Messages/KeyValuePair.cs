﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.Messages
{
    public class KeyValuePair : IKeyValuePair
    {
        public KeyValuePair()
        {
        }

        public KeyValuePair(string name, string value)
        {
            Key = name;
            Value = value;
        }

        /// <summary>
        /// Typically the string component such as code or description
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Typically the numeric component such as the Id
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}