using GameBoy.Architecture;
using System;
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

        public void PrintRegister()
        {
            Console.WriteLine("A = " + Convert.ToString(registers.A, 2).PadLeft(8, '0'));
            Console.WriteLine("F = " + Convert.ToString(registers.F, 2).PadLeft(8, '0'));
            Console.WriteLine("B = " + Convert.ToString(registers.B, 2).PadLeft(8, '0'));
            Console.WriteLine("C = " + Convert.ToString(registers.C, 2).PadLeft(8, '0'));
            Console.WriteLine("D = " + Convert.ToString(registers.D, 2).PadLeft(8, '0'));
            Console.WriteLine("E = " + Convert.ToString(registers.E, 2).PadLeft(8, '0'));
            Console.WriteLine("H = " + Convert.ToString(registers.H, 2).PadLeft(8, '0'));
            Console.WriteLine("L = " + Convert.ToString(registers.L, 2).PadLeft(8, '0'));
        }
    }
}
