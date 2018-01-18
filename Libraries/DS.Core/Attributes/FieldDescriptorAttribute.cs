using System;
using DS.Core;
using DS.Core.Enums;

namespace DS.Core.Attributes
{


    public class FieldDescriptorAttribute : Attribute
    {
        private string _displayName;

        public string DisplayName { get { return _displayName; } }

        private int _maxLength;

        public int MaxLength { get { return _maxLength; } }

        private int _minLength;

        public int MinLength { get { return _minLength; } }

        private DataType _dataType;

        public DataType DataType { get { return _dataType; } }


        public FieldDescriptorAttribute(string description, DataType dataType =DataType.String, int maxLength = 100, int minLength = 1)
        {
            _displayName = description;
            _maxLength = maxLength;
            _minLength = minLength;
            _dataType = dataType;
        }
    }
}
