
using GameBoy.CpuArchitecture;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Instruction
    {
        public OpCode OpCode { get; private set; }

        public void Set(OpCode opcode)
        {
            OpCode = opcode;
        }
        public int Execute(CPU cpu) => OpCode.Execute(cpu);
    }
}
