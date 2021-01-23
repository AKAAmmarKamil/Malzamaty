using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Malzamaty.Utils {
    public static class Util {
        public const int PageSize = 10;

        public static T Clone<T>(this T source) {
            if (!typeof(T).IsSerializable) {
                throw new ArgumentException("The type must be serializable. ", nameof(source));
            }

            if (ReferenceEquals(source, null)) {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            using Stream stream = new MemoryStream();
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T) formatter.Deserialize(stream);
        }
    }
}