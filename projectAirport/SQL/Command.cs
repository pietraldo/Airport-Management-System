using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class Command
    {
        string type; // display, add, delete, update
        string object_class;
        List<string> conditions;

        //public List<object> Command<T>(Func<T, T, bool> func, string type, T value, List<object> objs)
        //{
        //    List<object> correct = new List<object>();
        //    for (int i = 0; i < objs.Count; i++)
        //    {
        //        if (objs[i] is YourObjectType) // Replace YourObjectType with the actual type of your objects
        //        {
        //            var obj = (YourObjectType)objs[i]; // Replace YourObjectType
        //            switch (type)
        //            {
        //                case "age":
        //                    if (func(obj.Age, value))
        //                        correct.Add(obj);
        //                    break;
        //                case "name":
        //                    if (func(obj.Name, value))
        //                        correct.Add(obj);
        //                    break;
        //                // Add more cases for other types if needed
        //                default:
        //                    // Handle unsupported type
        //                    break;
        //            }
        //        }
        //    }
        //    return correct;
        //}
        //var resultGreaterThan = Command<int>((x, y) => x > y, "age", 20, list);
    }
}
