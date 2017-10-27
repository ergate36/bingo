using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

namespace MarigoldGame.Common
{
    // type name 만으로 타입을 찾는다.
    // server와 client의 어셈블리 구조가 달라서 타입만으로 찾도록 수정함.
    class MarigoldSerializationBinder : SerializationBinder
    {
        //public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        //{
        //    assemblyName = null;
        //    typeName = serializedType.Name;
        //}

        //public Type BindToType(
        //string assemblyName,
        //string typeName
        //)
        //{
        //    return Type.GetType(typeName);
        //}

        public override Type BindToType(string assemblyName, string typeName)
        {
            //Debug.Log("assemblyName : " + assemblyName + ", typeName : " + typeName);

            if (typeName == "System.Collections.Generic.List`1[[MarigoldGame.Common.Card, MarigoldGame]]")
            {
                return typeof(System.Collections.Generic.List<MarigoldGame.Common.Card>);
            }

            if (typeName == "System.Collections.Generic.List`1[[MarigoldGame.Common.Square, MarigoldGame]]")
            {
                return typeof(System.Collections.Generic.List<MarigoldGame.Common.Square>);
            }

            if (typeName == "System.Collections.Generic.List`1[[System.String, mscorlib]]")
            {
                return typeof(System.Collections.Generic.List<System.String>);
            }

            if (typeName == "MarigoldGame.Common.Square")
            {
                return typeof(MarigoldGame.Common.Square);
            }
            
            //Debug.Log("return : " + Type.GetType(typeName).ToString());

            return Type.GetType(typeName);
        }
    }
}
