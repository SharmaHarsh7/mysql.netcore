using Newtonsoft.Json.Converters;

namespace DS.Core.Attributes
{
    public class DateTimeConverter: IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            base.DateTimeFormat = "dd-MM-yyyy HH:mm:ss";
        }
    }

    public class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            base.DateTimeFormat = "MM/dd/yyyy";
        }
    }
}
