
using GameBoy.CpuArchitecture;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Instruction
    {
        public OpCode OpCode { get; private set; }
        private byte[] operands;

        public void Set(OpCode opcode, byte[] operandBuffer)
        {
            operands = operandBuffer;
            OpCode = opcode;
        }

        public byte Operand8 => operands[0];
        public ushort Operand16 => BitConverter.ToUInt16(operands, 0);
        public int Execute(CPU cpu) => OpCode.Perform(cpu, this);
    }
}
