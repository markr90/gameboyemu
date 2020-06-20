
using GameBoy.CpuArchitecture;
using Xunit;
using static GameBoy.CpuArchitecture.RegisterFlags;

namespace GameBoyTests.AluTests
{
    public class IncTests
    {
        private Registers registers;
        private Alu alu;

        public IncTests()
        {
            registers = new Registers();
            alu = new Alu(registers);
        }


        // ushort increment
        [Fact]
        public void When_UshortIncCarry_ExpectFlagsUnchanged()
        {
            registers.Reset();
            var result = alu.Inc(0xFFFF);
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(None));
        }

        [Fact]
        public void When_UshortIncHalfCarry_ExpectFlagsUnchanged()
        {
            registers.Reset();
            var result = alu.Inc(0x0FFF);
            Assert.Equal(0x1000, result);
            Assert.True(registers.AreFlagsSet(None));
        }

        [Fact]
        public void When_UshortIncCalled_SetFlagsAreStillSet()
        {
            registers.Reset();
            registers.SetFlags(All);
            var result = alu.Inc(0x0FFF);
            Assert.Equal(0x1000, result);
            Assert.True(registers.AreFlagsSet(All));
        }

        // Byte increment
        [Fact]
        public void When_ByteIncCarry_ExpectCarryFlagUnchangedZset()
        {
            registers.Reset();
            var result = alu.Inc(0xFF);
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(Z));
            Assert.False(registers.AreFlagsSet(C));
            registers.Reset();
            registers.SetFlags(C);
            result = alu.Inc(0xFF);
            Assert.True(registers.AreFlagsSet(Z));
            Assert.True(registers.AreFlagsSet(C));
        }

        [Fact]
        public void When_ByteIncHalfCarry_ExpectHalfCarryFlagSet()
        {
            registers.Reset();
            var result = alu.Inc(0x0F);
            Assert.Equal(0x10, result);
            Assert.True(registers.AreFlagsSet(H));
        }

        [Fact]
        public void When_ByteIncCalled_ExpectNcleared()
        {
            registers.Reset();
            var result = alu.Inc(0x0F);
            Assert.Equal(0x10, result);
            Assert.False(registers.AreFlagsSet(N));
        }


    }
}
