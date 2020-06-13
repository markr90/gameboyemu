using System;
using System.Collections;
using System.Collections.Generic;
using GameBoy.Architecture;

namespace GameBoy
{
    class Program
    {
        static void Main(string[] args)
        {
            CPU cpu = new CPU();
            cpu.registers.A = 16;
            cpu.registers.B = 16;

            cpu.PrintRegister();
            Console.WriteLine();

            cpu.Step();

            cpu.PrintRegister();

            Console.ReadLine();
        }

        static void WriteBits(int i)
        {
            Console.WriteLine(Convert.ToString(i, 2).PadLeft(8, '0'));
        }
    }
}
