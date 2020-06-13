using System;
using GameBoy.Architecture;

namespace GameBoy
{
    class Program
    {
        static void Main(string[] args)
        {
            CPU cpu = new CPU();
            cpu.registers.A = 16;
            cpu.registers.SetFlags(RegisterFlags.H);

            cpu.PrintRegister();
            Console.WriteLine();

            //cpu.SetPC(2);
            //cpu.Step();

            cpu.PrintRegister();

            Console.ReadLine();
        }
    }
}
