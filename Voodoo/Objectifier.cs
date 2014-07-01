using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Voodoo
{
    public static class Objectifyer
    {
        public static TObject ShallowCopy<TObject>(TObject o) where TObject : class
        {
            var info = o.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
            var clone = (TObject)info.Invoke(o, new object[] { });
            return clone;
        }
        public static TObject DeepCopy<TObject>(TObject o) where TObject : class
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                stream.Position = 0;
                return (TObject)formatter.Deserialize(stream);
            }
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
            var type = typeof(T);
            var xmlSerializer = extraTypes == null ? new XmlSerializer(type) : new XmlSerializer(type, extraTypes);
            var stringReader = new StringReader(xml);
            var xmlReader = new XmlTextReader(stringReader);
            var result = xmlSerializer.Deserialize(xmlReader) as T;
            xmlReader.Close();
            stringReader.Close();
            return result;
        }


        [DebuggerStepThrough]
        public static string ToDataContractXml<TObject>(TObject @object)
        {
            return ToDataContractXml(@object, null);
        }

        [DebuggerStepThrough]
        public static string ToDataContractXml<TObject>(TObject @object, Type[] extraTypes)
        {
            Type type = typeof(TObject);
            var serializer = extraTypes == null
                ? new DataContractSerializer(type)
                : new DataContractSerializer(type, extraTypes);
            var memStream = new MemoryStream();
            var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8) { Namespaces = true };
            serializer.WriteObject(xmlWriter, @object);

            xmlWriter.Close();
            memStream.Close();
            var xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            return xml;
        }

        [DebuggerStepThrough]
        public static string ToXml<TObject>(TObject @object)
        {
            return ToXml(@object, null, false);
        }

        [DebuggerStepThrough]
        public static string ToXml<TObject>(TObject @object, bool omitNamespaces)
        {
            return ToXml(@object, null, omitNamespaces);
        }

        [DebuggerStepThrough]
        public static string ToXml<TObject>(TObject @object, Type[] extraTypes, bool omitNamespaces)
        {
            var type = typeof(TObject);
            var serializer = extraTypes == null ?
                new XmlSerializer(type) : new XmlSerializer(type, extraTypes);
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
            var closingTag = xml.LastIndexOf('>');
            xml = xml.Substring(0, closingTag + 1);
            return xml;
        }
    }
}