using GameBoy.CpuArchitecture;
using GameBoy.Device;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Disassembler
    {
        public delegate Instruction InstructionCreator(CPU cpu);
        private readonly MemoryController _memController;
        public Disassembler(MemoryController memController)
        {
            _memController = memController;
        }

        public Instruction ReadInstruction(ref ushort location)
        {
            byte code = _memController.ReadByte(location++);
            Console.WriteLine("Trying to read code: {0:x2}", code);
            OpCode opCode = code == OpCodes.ExtendedTableOpCode
                ? OpCodes.PrefixedOpCodes[_memController.ReadByte(location++)]
                : OpCodes.SingleByteOpCodes[code];

            byte[] operands = _memController.ReadBytes(location, opCode.OperandLength);
            location += (ushort) opCode.OperandLength;

            return new Instruction(opCode, operands);
        }
    }
}
