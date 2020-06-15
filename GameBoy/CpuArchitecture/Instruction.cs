
using GameBoy.CpuArchitecture;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Instruction
    {
        private OpCode _opCode;
        public readonly byte[] Operands;
        public Instruction(OpCode opCode, byte[] operands)
        {
            _opCode = opCode;
            Operands = operands;
        }

        public byte Operand8 => Operands[0];
        public ushort Operand16 => BitConverter.ToUInt16(Operands, 0);

        public int Execute(CPU cpu) => _opCode.Perform(cpu, this);
    }
}
