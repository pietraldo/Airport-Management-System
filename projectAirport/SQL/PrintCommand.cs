﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projectAirport.SQL
{
    internal class PrintCommand
    {
        string[,] toPrint;
        string[] fieldsToDisplay;
        ExecuteCommand ex;
        public PrintCommand(ExecuteCommand executeCommand)
        {
            this.ex = executeCommand;
        }
        public bool Execute()
        {
            if (ex.operation != "display") return true;
            toPrint = ex.toPrint;
            fieldsToDisplay = ex.mc.fieldsToDisplay;
            
            Print();
            return true;
        }

        // prints array of strings,
        // first row is fieldsToDisplay 
        // second is dash line
        // and then rows
        private void Print()
        {
            int[] colLen = new int[toPrint.GetLength(1)];
            int sum = 0;

            Console.WriteLine();
            for (int i = 0; i < toPrint.GetLength(1); i++)
            {
                colLen[i] = fieldsToDisplay[i].Length;
                for (int j = 0; j < toPrint.GetLength(0); j++)
                {
                    if (toPrint[j, i].Length > colLen[i])
                        colLen[i] = toPrint[j, i].Length;
                }
                sum += colLen[i];
            }

            string dashline = "";
            for (int i = 0; i < fieldsToDisplay.Length; i++)
            {
                Console.Write(" "+fieldsToDisplay[i].PadRight(colLen[i]) + " ");
                if (i +1!= fieldsToDisplay.Length)
                    Console.Write("|");

                dashline += new string('-', colLen[i] + 2);
                if (i + 1 != fieldsToDisplay.Length)
                    dashline += "+";
                else
                    dashline += "-";
            }


            Console.WriteLine();
            Console.WriteLine(dashline);

            for (int i = 0; i < toPrint.GetLength(0); i++)
            {
                for (int j = 0; j < toPrint.GetLength(1); j++)
                {
                    Console.Write(" "+toPrint[i, j].ToString().PadLeft(colLen[j])+" ");
                    if(j+1!=toPrint.GetLength(1))
                        Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }
}
