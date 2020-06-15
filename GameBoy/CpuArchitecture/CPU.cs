using GameBoy.CpuArchitecture;
using GameBoy.Device;
using System;

namespace GameBoy.CpuArchitecture
{
    public class CPU
    {
        private const double OfficalClockFrequency = 4194304;

        public Registers Registers { get; }
        public MemoryController MemController { get; }
        public Alu Alu { get; }

        private Disassembler disassembler;
        private Instruction _nextInstruction;

        public readonly double CyclesPerSecond = OfficalClockFrequency;
        private ulong _ticks;

        public CPU(GameBoyDevice device)
        {
            Registers = new Registers();
            MemController = new MemoryController(device);
            Alu = new Alu(Registers);
            disassembler = new Disassembler(MemController);
        }

        public void Step()
        {
            int cycles;
            try
            {
                _nextInstruction = NextInstruction();
                cycles = _nextInstruction.Execute(this);
            }
            catch
            {
                Console.WriteLine("Instruction not found!");
            }
        }

        public void Jump(ushort address)
        {
            Registers.PC = address;
        }

        public void Push(ushort value)
        {
            Registers.SP -= 2;
            MemController.WriteUInt16(Registers.SP, value);
        }

        private Instruction NextInstruction()
        {
            return disassembler.ReadInstruction(ref Registers.PC);
        }

        public void PrintRegister()
        {
            Console.WriteLine(Registers.ToString());
        }

        public void PrintRegisterAsBits()
        {
            Console.WriteLine(Registers.ToBitString());
        }
    }
}
