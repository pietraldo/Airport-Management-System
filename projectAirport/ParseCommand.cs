using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    internal class ParseCommand
    {

        public ParseCommand(string command)
        {

            //string[] words= command.Split(' ');
            //if (words[0]=="display")
            //{
            //    parseDisplay(words);
            //}

            //Console.WriteLine(GetObjectClass(words));

            GetConditions(command);
        }

        public string? GetObjectClass(string[] command)
        {
            string? object_class=null;
            for (int i = 0; i < command.Length - 1; i++)
                if (string.Equals(command[i], "from", StringComparison.OrdinalIgnoreCase))
                    object_class = command[i + 1];

            return object_class;
        }
        public string[]? GetFieldsToDisplay(string[] command)
        {
            List<string> fields= new List<string>();

            int index=-1;
            for (int i = 0; i < command.Length - 1; i++)
                if (string.Equals(command[i], "from", StringComparison.OrdinalIgnoreCase))
                    index=i;

            if (index == -1)
                return null;

            for (int i = 1; i < index; i++)
                fields.Add(command[i]);

            return fields.ToArray();
        }
        public string[]? GetConditions(string command)
        {
            string[] cond=command.Split(" where ");
            if (cond.Length != 2) return null;

            string[] conditions = cond[1].Split(new string[]{" and ", "or" }, StringSplitOptions.None);

            (string, string, string)[] conds = new (string, string, string)[conditions.Length];
            for(int i=0; i< conditions.Length; i++)
            {
                string[] compers = new string[] { "<=", ">=",  "!=", "=", ">", "<"};
                bool ok = false;
                for(int j=0; j<compers.Length; j++)
                {
                    if (conditions[i].Contains(compers[j]))
                    {
                        ok = true;
                        string[] data = conditions[i].Split(compers[j],StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length != 2) return null;

                        conds[i] = (data[0], compers[j], data[1]);
                        break;
                    }
                }
                if (ok == false) return null;
               
            }
           for(int i=0; i<conds.Length; i++)
            {
                Console.WriteLine($"[{conds[i].Item1}], [{conds[i].Item2}], [{conds[i].Item3}]");
            }

            return conditions;
        }

        public void parseDisplay(string[] command)
        {
            
        }
    }
}
