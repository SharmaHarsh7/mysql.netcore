using System;
using DS.Core.Enums;

namespace DS.Core.Attributes
{
    public class DSFieldDescriptionAttribute : Attribute
    {
        private DataType _dataType;
        public DataType DataType { get { return _dataType; } }

        private bool _required;
        public bool Required { get { return _required; } }

        private int _minLength;
        public int MinLength { get { return _minLength; } }

        private int _maxLength;
        public int MaxLength { get { return _maxLength; } }

        private string _description;
        public string Description { get { return _description; } }


        private object _defaultValue;
        public object DefaultValue { get { return _defaultValue; } }


        public DSFieldDescriptionAttribute(DataType DataType = DataType.String, string Description = "", bool Required = false, int MinLength = -1, int MaxLength = -1, object DefaultValue = null)
        {
            _description = Description;
            _dataType = DataType;
            _required = Required;
            _minLength = MinLength;
            _maxLength = MaxLength;

            if((DataType == DataType.DateTime) && (DefaultValue == null))
                _defaultValue = DateTime.Now;
            else if ((DataType == DataType.Boolean) && (DefaultValue == null))
                _defaultValue = false;
            else
                _defaultValue = DefaultValue;

        }
    }
}
