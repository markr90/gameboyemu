using GameBoy.CpuArchitecture;
using GameBoy.Device;
using System;

namespace GameBoy.CpuArchitecture
{
    public class Disassembler
    {
        public delegate Instruction InstructionCreator(CPU cpu);
        private readonly MemoryController _memController;
        private byte[] operandBuffer = new byte[2];


        public Disassembler(MemoryController memController)
        {
            _memController = memController;
        }

        private void ReadOperandBuffer(ushort location)
        {
            operandBuffer[0] = _memController.Read(location++);
            operandBuffer[1] = _memController.Read(location++);
        }

        public void FetchInstruction(ref ushort location, ref Instruction instr)
        {
            byte code = _memController.Read(location++);
            Console.WriteLine("Trying to read code: {0:x2}", code);

            OpCode opcode = code == OpCodes.ExtendedTableOpCode
                ? OpCodes.PrefixedOpCodes[location++]
                : OpCodes.SingleByteOpCodes[code];

            ReadOperandBuffer(location);

            instr.Set(opcode, operandBuffer[0], operandBuffer[2]);
            location += opcode.OperandLength;
        }
    }
}
