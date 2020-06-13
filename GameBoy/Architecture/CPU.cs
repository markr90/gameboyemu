using GameBoy.Architecture;
using System;
using System.Diagnostics.Contracts;
using System.Dynamic;

namespace GameBoy.Architecture
{
    public class CPU
    {
        public Registers registers { get; }
        public Memory memory { get; }
        private Disassembler disassembler;
        private Instruction _nextInstruction;
        public readonly Alu Alu;

        public CPU()
        {
            registers = new Registers();
            memory = new Memory();
            Alu = new Alu(registers);
            disassembler = new Disassembler(memory);
        }

        public void Step()
        {
            try
            {
                _nextInstruction = NextInstruction();
                _nextInstruction.Execute(this);
            }
            catch
            {
                Console.WriteLine("Instruction not found!");
            }
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
