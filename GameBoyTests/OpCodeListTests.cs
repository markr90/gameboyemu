using System;
using GameBoy.CpuArchitecture;
using Xunit;

namespace GameBoyTests
{
    public class OpCodeListTests
    {
        [Fact]
        public void SingleByteOpcodes_0x00_to_0xFF_are_set()
        {
            for (int i = 0; i < 0xFF; i++)
            {
                OpCode opcode = OpCodes.SingleByteOpCodes[i];
                Assert.Equal(i, opcode.Code);
            }
        }
        [Fact]
        public void PrefixByteOpcodes_0x00_to_0xFF_are_set()
        {
            for (int i = 0; i < 0xFF; i++)
            {
                OpCode opcode = OpCodes.PrefixedOpCodes[i];
                Assert.Equal(i, opcode.Code);
            }
        }
    }
}
