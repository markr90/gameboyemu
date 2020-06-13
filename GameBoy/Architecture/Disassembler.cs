using GameBoy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Architecture
{
    public class Disassembler
    {
        public delegate Instruction InstructionCreator(CPU cpu);
        private readonly Memory _memory;
        public Disassembler(Memory memory)
        {
            _memory = memory;
        }

        public Instruction ReadInstruction(ushort location)
        {
            byte code = _memory.ReadByte(location);
            OpCode opCode = GetOpCode(code);
            return new Instruction(opCode);
        }

        private OpCode GetOpCode(byte code)
        {
            return OpCodes.SingleByteOpCodes[code];
        }
    }
}
