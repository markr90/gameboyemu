
using GameBoy.CpuArchitecture;
using Xunit;
using static GameBoy.CpuArchitecture.RegisterFlags;

namespace GameBoyTests.AluTests
{
    public class DecTests
    {
        private Registers registers;
        private Alu alu;

        public DecTests()
        {
            registers = new Registers();
            alu = new Alu(registers);
        }

        // ushort decrement tests

        [Fact]
        public void When_UshortDecCarry_FlagsUnchanged()
        {
            registers.Reset();
            var result = alu.Dec((ushort) 0);
            Assert.Equal(0xFFFF, result);
            Assert.True(registers.AreFlagsSet(None));
        }

        [Fact]
        public void When_UshortHalfCarry_FlagsUnchanged()
        {
            registers.Reset();
            var result = alu.Dec(0x1000);
            Assert.Equal(0x0FFF, result);
            Assert.True(registers.AreFlagsSet(None));
        }

        [Fact]
        public void When_UshortDecZero_FlagsUnchanged()
        {
            registers.Reset();
            var result = alu.Dec((ushort) 1);
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(None));
        }

        // byte decrement tests
        [Fact]
        public void When_ByteDecZero_ZeroFlagSet()
        {
            registers.Reset();
            var result = alu.Dec((byte) 1);
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(Z));
        }

        // byte decrement tests
        [Fact]
        public void When_ByteDecHalfCarry_HalfCarryFlagSet()
        {
            registers.Reset();
            var result = alu.Dec((byte) 0x10);
            Assert.Equal(0x0F, result);
            Assert.True(registers.AreFlagsSet(H));
        }

        [Fact]
        public void When_ByteDecCalled_NflagSet()
        {
            registers.Reset();
            alu.Dec((byte)1);
            Assert.True(registers.AreFlagsSet(N));
        }

        [Fact]
        public void When_ByteDecCarry_CarryFlagStillClear()
        {
            registers.Reset();
            var result = alu.Dec((byte)0);
            Assert.Equal(0xFF, result);
            Assert.False(registers.AreFlagsSet(C));
        }

    }
}
