using System;
using GameBoy.CpuArchitecture;
using GameBoy.Device;

namespace GameBoy
{
    class Program
    {
        static void Main(string[] args)
        {
            GameBoyDevice gb = new GameBoyDevice();
            CPU cpu = gb.Cpu;
            cpu.Registers.A = 15;
            cpu.Registers.B = 1;

            cpu.Registers.C = 0xF0;
            cpu.PrintRegisterAsBits();
            Console.WriteLine();            
            
            cpu.Registers.C = cpu.Alu.Rlc(cpu.Registers.C);
            cpu.PrintRegisterAsBits();
            Console.WriteLine();

            Console.WriteLine();
            cpu.PrintRegister();

            cpu.MemController.WriteByte(0, 2);
            cpu.Step();

            cpu.PrintRegister();

            Console.ReadLine();
        }
    }
}
