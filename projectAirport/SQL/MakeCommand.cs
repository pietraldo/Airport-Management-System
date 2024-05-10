using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class MakeCommand
    {

        public ConditionsMakeComand<object>[]? conditions;
        public string[] fieldsToDisplay;
        public string objectClass;


        private Dictionary<string, string[]> fields = new Dictionary<string, string[]>() {
            { "flight", new string[] { "id", "origin", "target", "takeoftime", "landingtime", "worldposition","amsl", "plane" } },
            { "airport", new string[] { "id", "name", "code", "worldposition", "amsl", "countrycode"} },
            { "passangerplane", new string[]{ "id", "serial", "countrycode", "model"} },
            { "cargoplane", new string[]{ "id", "serial", "countrycode", "model", "maxload"} },
            { "cargo", new string[]{ "id", "weight", "code", "description"} },
            { "passanger", new string[]{ "id", "name", "age", "phone", "email", "class", "miles"}},
            { "crew", new string[]{ "id", "name", "age", "phone", "email", "practise", "role" } }
        };

        private Dictionary<(string, string), string> type = new Dictionary<(string, string), string>()
        {
            { ("flight","id"), "uint"},
            { ("flight","origin"), "struct"},
            { ("flight","target"), "struct"},
            { ("flight","landingtime"), "datetime"},
            { ("flight","amsl"), "float"},
        };

        public MakeCommand(ParseCommand pc)
        {
            if (!fields.ContainsKey(pc.objectClass))
                return; // nie ma takiej tablicy

            if (pc.operation == operations.display)
            {

                if (pc.fieldsToDisplay.Count() == 1 && pc.fieldsToDisplay[0] == "*")
                {
                    fieldsToDisplay = fields[pc.objectClass];
                }
                else
                {
                    for (int i = 0; i < pc.fieldsToDisplay.Count(); i++)
                        if (!fields[pc.objectClass].Contains(pc.fieldsToDisplay[i]))
                            return; // zle pole do wyswietlenia

                    fieldsToDisplay = pc.fieldsToDisplay;
                }

            }
            objectClass = pc.objectClass;
            MakeConditions(pc);
        }

        private bool MakeConditions(ParseCommand pc)
        {
            if (pc.conditions == null)
                return false;


            conditions = new ConditionsMakeComand<object>[pc.conditions.Length];

            for (int i = 0; i < pc.conditions.Length; i++)
            {
                if (fields[pc.objectClass].Contains(pc.conditions[i].value1) && fields[pc.objectClass].Contains(pc.conditions[i].value2))
                    return false;
                if (!fields[pc.objectClass].Contains(pc.conditions[i].value1) && !fields[pc.objectClass].Contains(pc.conditions[i].value2))
                    return false;
                if (fields[pc.objectClass].Contains(pc.conditions[i].value1))
                {
                    conditions[i].field = pc.conditions[i].value1;
                    conditions[i].value = pc.conditions[i].value2;
                }
                else
                {
                    conditions[i].field = pc.conditions[i].value2;
                    conditions[i].value = pc.conditions[i].value1;
                }

                conditions[i].andOr = pc.conditions[i].andOr;

                Comparer<int> comparer1 = new Comparer<int>();
                Comparer<string> comparer2 = new Comparer<string>();


                //conditions[i].Comparison = comparer1.Less;
            }

            return true;
        }
    }
    internal struct ConditionsMakeComand<T>
    {
        public string field;
        public T value;
        public string andOr;
        public Func<T, T, bool> Comparison;
    }

    internal class Comparer<T> where T : IComparable<T>
    {
        public bool Less(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        public bool Equal(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }
    }

}
