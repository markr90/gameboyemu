
using GameBoy.CpuArchitecture;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Instruction
    {
        public readonly OpCode OpCode;
        public readonly byte[] Operands;
        public Instruction(OpCode opCode, byte[] operands)
        {
            OpCode = opCode;
            Operands = operands;
        }

        public byte Operand8 => Operands[0];
        public ushort Operand16 => BitConverter.ToUInt16(Operands, 0);

        public int Execute(CPU cpu) => OpCode.Perform(cpu, this);
    }
}
