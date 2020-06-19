
using GameBoy.CpuArchitecture;
using System.Runtime.InteropServices;
using System;

namespace GameBoy.CpuArchitecture
{
    [StructLayout(LayoutKind.Explicit)]
    public class Operands
    {
        [FieldOffset(0)] public byte operandPlaceholder;
        [FieldOffset(1)] public byte operand8;
        [FieldOffset(0)] public ushort operand16;
    }

    public class Instruction
    {
        private Operands operands = new Operands();

        public OpCode OpCode { get; private set; }

        public void Set(OpCode opcode, byte operand1, byte operand2)
        {
            OpCode = opcode;
            operands.operand8 = operand1;
            operands.operandPlaceholder = operand2;
        }

        public byte Operand8 => operands.operand8;
        public ushort Operand16 => operands.operand16;
        public int Execute(CPU cpu) => OpCode.Perform(cpu, this);
    }
}
