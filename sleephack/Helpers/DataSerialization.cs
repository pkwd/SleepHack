using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

namespace quantifiedself.Helpers
{
    public static class DataSerialization
    {
        public static string GetJson(Type type, object obj)
        {
            var stream = new MemoryStream();
            var jsonSer =
                new DataContractJsonSerializer(type);
            jsonSer.WriteObject(stream, obj);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            var json = sr.ReadToEnd();
            return json;
        }

        public static byte[] SerializeObject<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    var dcs = new DataContractSerializer(typeof(T));
                    dcs.WriteObject(writer, obj);
                    writer.Flush();
                    return ms.ToArray();
                }
            }
        }

        public static T DeserializeObject<T>(byte[] xml)
        {
            using (var memoryStream = new MemoryStream(xml))
            {
                using (var reader = XmlDictionaryReader.CreateBinaryReader(
                    memoryStream, XmlDictionaryReaderQuotas.Max))
                {
                    var dcs = new DataContractSerializer(typeof(T));
                    return (T)dcs.ReadObject(reader);
                }
            }
        }
    }
}
