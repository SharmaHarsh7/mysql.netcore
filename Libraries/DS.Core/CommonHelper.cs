using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DS.Core.ComponentModel;
using System.Linq;
using System.IO;
using System.ComponentModel.DataAnnotations;
using DS.Core.Attributes;
using DS.Core.Domain;

namespace DS.Core
{
    public partial class CommonHelper
    {
        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new DSException("Email is not valid.");
            }

            return output;
        }

        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str =>
            {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }

        public static bool ArraysEqual<T>(T[] a1, T[] a2)
        {
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < a1.Length; i++)
            {
                if (!comparer.Equals(a1[i], a2[i])) return false;
            }
            return true;
        }

        private static AspNetHostingPermissionLevel? _trustLevel;
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //set minimum
                _trustLevel = AspNetHostingPermissionLevel.None;

                //determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in new[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }

        public static void SetProperty(object instance, string propertyName, object value)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (propertyName == null) throw new ArgumentNullException("propertyName");

            Type instanceType = instance.GetType();
            PropertyInfo pi = instanceType.GetProperty(propertyName);
            if (pi == null)
                throw new DSException("No property '{0}' found on the instance of type '{1}'.", propertyName, instanceType);
            if (!pi.CanWrite)
                throw new DSException("The property '{0}' on the instance of type '{1}' does not have a setter.", propertyName, instanceType);
            if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
                value = To(value, pi.PropertyType);
            pi.SetValue(instance, value, new object[0]);
        }

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }

        public static object To(object value, Type destinationType)
        {
            return To(value, destinationType, CultureInfo.InvariantCulture);
        }

        public static object To(object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }

        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            int age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }


        public static List<KeyValuePair<string, int>> GetEnumList<T>()
        {
            var list = new List<KeyValuePair<string, int>>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                list.Add(new KeyValuePair<string, int>(e.ToString(), (int)e));
            }

            return list;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }


        public static string GetUniqFileName(string dirPath)
        {
            bool isUniqName = false;
            string fileName = string.Empty;
            var dirInfo = new System.IO.DirectoryInfo(dirPath);

            do
            {
                fileName = CommonHelper.RandomString(10);

                isUniqName = (from file in dirInfo.GetFiles()
                              where System.IO.Path.GetFileNameWithoutExtension(file.Name) == fileName
                              select file).Count() > 0;

            } while (isUniqName);

            return fileName;
        }

        public static Dictionary<string, ModelMetaData> GenerateModelValidation(Type type)
        {
            var name = type.Name;

            name = Char.ToLowerInvariant(name[0]) + name.Substring(1);

            var validationModel = new Dictionary<string, ModelMetaData>();

            foreach (var prop in type.GetProperties())
            {
                object[] attrs = prop.GetCustomAttributes(true);

                if (attrs == null || attrs.Length == 0)
                    continue;

                ModelMetaData conds = new ModelMetaData();

                foreach (Attribute attr in attrs)
                {
                    if (attr is DSFieldDescriptionAttribute)
                    {
                        conds.Type = (attr as DSFieldDescriptionAttribute).DataType;
                        conds.DefaultValue = (attr as DSFieldDescriptionAttribute).DefaultValue;
                        conds.MinLength = (attr as DSFieldDescriptionAttribute).MinLength;
                        conds.MaxLength = (attr as DSFieldDescriptionAttribute).MaxLength;
                        conds.Required = (attr as DSFieldDescriptionAttribute).Required;
                        conds.Description = (attr as DSFieldDescriptionAttribute).Description;
                    }
                }

                validationModel.Add(prop.Name, conds);
            }

            return validationModel;
        }

        public static Dictionary<string, ModelMetaData> GenerateModelValidation<T>()
        {
            return GenerateModelValidation(typeof(T));
        }

        public static string FileToBase64(string filePath)
        {
            if (File.Exists(filePath))
                 return System.Convert.ToBase64String(File.ReadAllBytes(filePath));

            return "";
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

    }
}
