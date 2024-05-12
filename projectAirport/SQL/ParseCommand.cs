using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    public enum operations {display, update, delete, add };
    internal class ParseCommand
    {
        string command;
        public operations operation;
        public string objectClass;
        public string[] fieldsToDisplay;
        public ConditionParse[]? conditions;

        public ParseCommand(string command)
        {
            this.command = command;
        }

        public bool Execute()
        {
            SetOperation(command);
            objectClass = GetObjectClass(command);
            fieldsToDisplay = GetFieldsToDisplay(command);
            conditions = GetConditions(command);
            return true;
        }

        private bool SetOperation(string command)
        {
            switch(command.Split(' ')[0])
            {
                case "display":
                    operation = operations.display;
                    return true;
                case "update":
                    operation = operations.update;
                    return true;
                case "add":
                    operation = operations.add;
                    return true;
                case "delete":
                    operation = operations.delete;
                    return true;
            }
            return false;
        }
        private string? GetObjectClass(string command)
        {
            string[] commands = command.Split(' ');
            string? object_class = null;
            for (int i = 0; i < commands.Length - 1; i++)
                if (string.Equals(commands[i], "from"))
                    object_class = commands[i + 1];

            return object_class;
        }
        private string[]? GetFieldsToDisplay(string command)
        {
            int startIndex = command.IndexOf("display")+7;
            int endIndex = command.IndexOf("from");

            if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex) return null;
            

            string[] fields= command.Substring(startIndex, endIndex - startIndex).Trim().Split(',', StringSplitOptions.TrimEntries);

            return fields;
        }
        private ConditionParse[]? GetConditions(string command)
        {
            string[] cond = command.Split(" where ");
            if (cond.Length != 2) return null;

            string[] splited = cond[1].Split(new string[] { " and ", " or " }, StringSplitOptions.None);

            ConditionParse[] conds = new ConditionParse[splited.Length];
            int len = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                string[] compers = new string[] { "<=", ">=", "!=", "=", ">", "<" };
                bool ok = false;
                
                for (int j = 0; j < compers.Length; j++)
                {
                    if (splited[i].Contains(compers[j]))
                    {
                        ok = true;
                        string[] data = splited[i].Split(compers[j], StringSplitOptions.TrimEntries);
                        if (data.Length != 2) return null;

                        conds[i] = new ConditionParse();
                        conds[i].value1 = data[0];
                        conds[i].value2= data[1];
                        conds[i].compare= compers[j];

                        
                        if (i == 0)
                            conds[i].andOr = "or";
                        else
                        {

                            if (cond[1][len+2-1]=='a')
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
                len += splited[i].Length;
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
                Console.WriteLine($"[{cond.value1}, {cond.value2}, {cond.andOr}, {cond.compare}]");
        }

       
    }
    internal struct ConditionParse
    {
        public string value1;
        public string value2;
        public string andOr;
        public string compare;
    }


}
