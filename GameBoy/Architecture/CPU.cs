using GameBoy.Architecture;
using System;
using System.Diagnostics.Contracts;
using System.Dynamic;

namespace GameBoy.Architecture
{
    public class CPU
    {
        private const double OfficalClockFrequency = 4194304;

        public Registers registers { get; }
        public Memory memory { get; }
        private Disassembler disassembler;
        private Instruction _nextInstruction;
        public readonly Alu Alu;

        public readonly double CyclesPerSecond = OfficalClockFrequency;
        private ulong _ticks;

        public CPU()
        {
            registers = new Registers();
            memory = new Memory();
            Alu = new Alu(registers);
            disassembler = new Disassembler(memory);
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

        public void SetPC(ushort pc)
        {
            registers.PC = pc;
        }

        private Instruction NextInstruction()
        {
            registers.PC++;
            return disassembler.ReadInstruction(registers.PC);
        }

        public ushort ReadImmediate16()
        {
            return BitConverter.ToUInt16(ReadImmediateBytes(2), 0);
        }

        public byte ReadImmediate8()
        {
            return ReadImmediateBytes(1)[0];
        }

        private byte[] ReadImmediateBytes(int length)
        {
            byte[] buffer = memory.ReadBytes(registers.PC, 2);
            registers.PC += (ushort) length;
            return buffer;
        }

        public void PrintRegister()
        {
            Console.WriteLine(registers.ToString());
        }
    }
}
