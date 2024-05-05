using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class ParseCommand
    {
        private string operation;
        private string? objectClass;
        private string[]? fieldsToDisplay;
        private Condition[]? conditions;

        public ParseCommand(string command)
        {
            command = command.ToLower();
            operation = GetOperation(command);
            objectClass = GetObjectClass(command);
            fieldsToDisplay = GetFieldsToDisplay(command);
            conditions = GetConditions(command);
        }

        private string GetOperation(string command)
        {
            return command.Split(' ')[0];
        }
        private string? GetObjectClass(string command)
        {
            string[] commands = command.Split(' ');
            string? object_class = null;
            for (int i = 0; i < commands.Length - 1; i++)
                if (string.Equals(commands[i], "from", StringComparison.OrdinalIgnoreCase))
                    object_class = commands[i + 1];

            return object_class;
        }
        private string[]? GetFieldsToDisplay(string command)
        {
            int startIndex = command.IndexOf("select")+6 ;
            int endIndex = command.IndexOf("from");

            if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex) return null;
            

            string[] fields= command.Substring(startIndex, endIndex - startIndex).Trim().Split(',', StringSplitOptions.TrimEntries);

            return fields;
        }
        private Condition[]? GetConditions(string command)
        {
            string[] cond = command.Split(" where ");
            if (cond.Length != 2) return null;

            string[] splited = cond[1].Split(new string[] { " and ", " or " }, StringSplitOptions.None);

            Condition[] conds = new Condition[splited.Length];
            for (int i = 0; i < splited.Length; i++)
            {
                string[] compers = new string[] { "<=", ">=", "!=", "=", ">", "<" };
                bool ok = false;
                int len = 0;
                for (int j = 0; j < compers.Length; j++)
                {
                    if (splited[i].Contains(compers[j]))
                    {
                        ok = true;
                        string[] data = splited[i].Split(compers[j], StringSplitOptions.TrimEntries);
                        if (data.Length != 2) return null;

                        conds[i] = new Condition();
                        conds[i].value = data[0];
                        conds[i].field= data[1];
                        conds[i].compare= compers[j];

                        len += splited[i].Length;
                        if (i == 0)
                            conds[i].andOr = "and";
                        else
                        {

                            if (cond[1][len+2]=='a')
                            {
                                len += 5;
                                conds[i].andOr = "and";
                            }
                            else
                            {
                                len += 4;
                                conds[i].andOr = "or";
                            }
                        }
                        break;
                    }
                }
                if (ok == false) return null;

            }
            

            return conds;
        }


        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Operation: {operation}");
            Console.WriteLine($"Object Class: {objectClass}");
            Console.WriteLine($"Fields to display: ");
            if(fieldsToDisplay!= null)
            foreach (var field in fieldsToDisplay)
                Console.Write(field+", ");
            Console.WriteLine($"Conditions: ");
            if(conditions!=null)
            foreach (var cond in conditions)
                Console.WriteLine($"[{cond.value}, {cond.field}, {cond.andOr}, {cond.compare}]");
        }
    }

    internal struct Condition
    {
        public string value;
        public string andOr;
        public string compare;
        public string field;
    }
}
