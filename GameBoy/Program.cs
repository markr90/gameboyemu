using System;
using GameBoy.DeviceComponents;
using GameBoy.Main;

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

            cpu.MemController.Write(0, 2);
            cpu.Step();

            cpu.PrintRegister();

            Console.WriteLine();

            byte x = 128;
            sbyte xx = (sbyte) x;
            ushort pc = 65535;
            ushort y = (ushort)unchecked(pc + xx);

            Console.WriteLine("y = pc + x = {0} + {1} = {2}", pc, xx , y);

            Console.ReadLine();
        }
    }
}
