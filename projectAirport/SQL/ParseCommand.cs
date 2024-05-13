using HarfBuzzSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport.SQL
{
    internal class ParseCommand
    {
        string command;
        public string operation;
        public string objectClass;
        public string[] fieldsToDisplay;
        public (string field, string value)[] fieldsToSet;
        public ConditionParse[] conditions= Array.Empty<ConditionParse>();

        public ParseCommand(string command)
        {
            this.command = command;
        }

        public bool Execute()
        {
            if (!SetOperation(command)) return false;
            if (!SetObjectClass(command)) return false;
            if (!SetFieldsToDisplay(command)) return false;
            if (!SetFieldsToSet(command)) return false;
            if (!SetConditions(command)) return false;
            
            return true;
        }

        private bool SetOperation(string command)
        {
            operation = command.Split(' ')[0];
            if (operation == "display" || operation == "update" || operation == "add" || operation == "delete")
                return true;
            Console.WriteLine($"Operation {operation} not known");
            return false;
        }
        private bool SetObjectClass(string command)
        {
            string[] commands = command.Split(' ');

            if (operation != "display")
            {
                if (commands.Length <= 1)
                {
                    Console.WriteLine("Missing objectClass");
                    return false;
                }

                objectClass = commands[1];
                return true;
            }

            string? object_class = null;
            for (int i = 0; i < commands.Length - 1; i++)
                if (commands[i] == "from")
                    object_class = commands[i + 1];
            if (object_class == null)
            {
                Console.WriteLine("Missing objectClass");
                return false;
            }

            objectClass = object_class;
            return true;
        }
        private bool SetFieldsToDisplay(string command)
        {
            if (operation != "display") return true;

            int startIndex = command.IndexOf("display") + 7;
            int endIndex = command.IndexOf("from");

            if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex)
            {
                Console.WriteLine("Wrong fields to display");
                return false;
            }


            fieldsToDisplay = command.Substring(startIndex, endIndex - startIndex).Trim().Split(',', StringSplitOptions.TrimEntries);
            if (fieldsToDisplay.Length == 0 || fieldsToDisplay[0] == "")
            {
                Console.WriteLine("Missing fields to display");
                return false;
            }

            return true;
        }
        private bool SetFieldsToSet(string command)
        {
            if (operation != "update") return true;

            if (command.Split(" ")[2]!="set")
            {
                Console.WriteLine("No set word in update command");
                return false;
            }

            int startIndex = command.IndexOf("(");
            int endIndex = command.IndexOf(")");

            if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex)
            {
                Console.WriteLine("Syntax error");
                return false;
            }

            string[] setts = command.Substring(startIndex+1, endIndex - startIndex-1).Trim().Split(',', StringSplitOptions.TrimEntries);
            if (setts.Length == 0 || setts[0] == "")
            {
                Console.WriteLine("Missing fields to set");
                return false;
            }

            fieldsToSet = new (string, string)[setts.Length];
            for(int i =0; i<setts.Length; i++)
            {
                string[] s = setts[i].Split("=", StringSplitOptions.TrimEntries);
                if(s.Length!=2)
                {
                    Console.WriteLine("Wrong set field: " + setts[i]);
                    return false;
                }
                fieldsToSet[i] = (s[0], s[1]);
            }

            return true;
        }
        private bool SetConditions(string command)
        {
            if (operation == "add") return true;

            string[] parts = command.Split(" where ");

            if (parts.Length == 1) return true;
            if(parts.Length > 2)
            {
                Console.WriteLine("Syntax Error: to many where");
                return false;
            }

            string[] splited = parts[1].Split(new string[] { " and ", " or " }, StringSplitOptions.None);
            conditions = new ConditionParse[splited.Length];

            int len = 0;
            for (int i = 0; i < splited.Length; i++)
            {
                string[] compers = ["<=", ">=", "!=", "=", ">", "<"];
                bool ok = false;

                for (int j = 0; j < compers.Length; j++)
                {
                    if (splited[i].Contains(compers[j]))
                    {
                        ok = true;
                        string[] data = splited[i].Split(compers[j], StringSplitOptions.TrimEntries);
                        if (data.Length != 2)
                        {
                            Console.WriteLine("Wrong condition: " + splited[i]);
                            return false;
                        }

                        conditions[i] = new ConditionParse();
                        conditions[i].value1 = data[0];
                        conditions[i].value2 = data[1];
                        conditions[i].compare = compers[j];


                        if (i == 0)
                            conditions[i].andOr = "or";
                        else
                        {

                            if (parts[1][len + 2 - 1] == 'a')
                            {
                                len += 5;
                                conditions[i].andOr = "and";
                            }
                            else
                            {
                                len += 4;
                                conditions[i].andOr = "or";
                            }
                        }
                        break;
                    }
                }
                if (ok == false)
                {
                    Console.WriteLine("Wrong condition: " + splited[i]);
                    return false;
                }
                len += splited[i].Length;
            }


            return true;
        }


        public void Show()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Operation: {operation}");
            Console.WriteLine($"Object Class: {objectClass}");

            if(operation=="display")
            {
                Console.WriteLine($"Fields to display: ");
                if (fieldsToDisplay != null)
                    foreach (var field in fieldsToDisplay)
                        Console.Write(field + ", ");
            }
            if(operation!="add")
            {
                Console.WriteLine($"Conditions: ");
                if (conditions != null)
                    foreach (var cond in conditions)
                        Console.WriteLine($"[{cond.value1}, {cond.value2}, {cond.andOr}, {cond.compare}]");
            }
            if(operation=="update")
            {
                Console.WriteLine($"Setts: ");
                foreach (var set in fieldsToSet)
                    Console.WriteLine($"[{set.field}, {set.value}]");
            }
           
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
