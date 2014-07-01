using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Voodoo
{
    public static class Objectifyer
    {
        public static TObject Clone<TObject>(TObject o) where TObject : class
        {
            var info = o.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            var clone = (TObject) info.Invoke(o, new object[] {});
            return clone;
        }

        public static string Base64Encode(string data)
        {
            var byteData = Encoding.UTF8.GetBytes(data);
            var encodedData = Convert.ToBase64String(byteData);
            return encodedData;
        }

        public static string Base64Decode(string data)
        {
            var encoder = new UTF8Encoding();
            var utf8Decode = encoder.GetDecoder();

            var toDecodeBytes = Convert.FromBase64String(data);
            var charCount = utf8Decode.GetCharCount(toDecodeBytes, 0, toDecodeBytes.Length);
            var decodedChar = new char[charCount];
            utf8Decode.GetChars(toDecodeBytes, 0, toDecodeBytes.Length, decodedChar, 0);
            var result = new String(decodedChar);
            return result;
        }

        [DebuggerStepThrough]
        public static T FromXml<T>(string xml) where T : class, new()
        {
            return FromXml<T>(xml, null);
        }

        [DebuggerStepThrough]
        public static T FromXml<T>(string xml, Type[] extraTypes) where T : class, new()
        {
            var type = typeof (T);
            var xmlSerializer = extraTypes == null ? new XmlSerializer(type) : new XmlSerializer(type, extraTypes);
            var stringReader = new StringReader(xml);
            var xmlReader = new XmlTextReader(stringReader);
            var result = xmlSerializer.Deserialize(xmlReader) as T;
            xmlReader.Close();
            stringReader.Close();
            return result;
        }


        [DebuggerStepThrough]
        public static string ToDataContractXml(Object @object)
        {
            return ToDataContractXml(@object, @object.GetType(), null);
        }

        [DebuggerStepThrough]
        public static string ToDataContractXml(object @object, Type type, Type[] extraTypes)
        {
            var serializer = extraTypes == null
                ? new DataContractSerializer(type)
                : new DataContractSerializer(type, extraTypes);
            var memStream = new MemoryStream();
            var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8) {Namespaces = true};
            serializer.WriteObject(xmlWriter, @object);

            xmlWriter.Close();
            memStream.Close();
            var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            return xml;
        }


        [DebuggerStepThrough]
        public static string ToXml(Object @object)
        {
            return ToXml(@object, @object.GetType(), null, false);
        }

        [DebuggerStepThrough]
        public static string ToXml(Object @object, bool omitNamespaces)
        {
            return ToXml(@object, @object.GetType(), null, omitNamespaces);
        }

        [DebuggerStepThrough]
        public static string ToXml(object @object, Type type, Type[] extraTypes, bool omitNamespaces)
        {
            XmlSerializer serializer;
            if (extraTypes == null)
                serializer = new XmlSerializer(type);
            else
                serializer = new XmlSerializer(type, extraTypes);
            var memStream = new MemoryStream();
            var xmlWriter = new XmlTextWriter(memStream, new UTF8Encoding());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            if (omitNamespaces)
                serializer.Serialize(xmlWriter, @object, ns);
            else
                serializer.Serialize(xmlWriter, @object);
            xmlWriter.Close();
            memStream.Close();
            var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            try
            {
                var closingTag = xml.LastIndexOf('>');
                xml = xml.Substring(0, closingTag + 1);
            }
            catch
            {
            }
            return xml;
        }
    }
}