using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.Serialization;

namespace MarigoldGame.Common
{
    // type name 만으로 타입을 찾는다.
    // server와 client의 어셈블리 구조가 달라서 타입만으로 찾도록 수정함.
    // 클라이언트와 서버가 같은 파일을 사용할 필요가 있음.
    class MarigoldSerializationBinder : SerializationBinder
    {
        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.FullName;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            return Type.GetType(typeName);
        }
    }
}
