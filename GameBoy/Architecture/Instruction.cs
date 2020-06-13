
using GameBoy.Architecture;
using System;

namespace GameBoy.Architecture
{
    public class Instruction
    {
        private OpCode _opCode;
        public Instruction(OpCode opCode)
        {
            _opCode = opCode;
        }

        public int Execute(CPU cpu) => _opCode.Operation(cpu);
    }
}
