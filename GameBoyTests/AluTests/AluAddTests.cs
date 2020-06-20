
using GameBoy.CpuArchitecture;
using Xunit;

namespace GameBoyTests
{
    public class AluAddTests
    {
        private Registers registers;
        private Alu alu;

        public AluAddTests()
        {
            registers = new Registers();
            alu = new Alu(registers);
        }

        // byte + byte = byte tests
        [Fact]
        public void When_ByteHalfOverFlow_Expect_HalfCarryFlagSet()
        {
            registers.Reset();
            alu.Add((byte)15, (byte)1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact]
        public void When_ByteOverFlow_Expect_CarryFlagSet()
        {
            registers.Reset();
            alu.Add((byte)255, (byte)1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }

        [Fact]
        public void When_ByteResultIsZero_Expect_ZeroAndCarryFlagSet()
        {
            registers.Reset();
            var result = alu.Add((byte)255, (byte)1);
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(RegisterFlags.Z));
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }

        [Fact]
        public void When_ByteAddCalled_NflagIsCleared()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.N);
            alu.Add((byte)1, (byte)1);
            Assert.False(registers.AreFlagsSet(RegisterFlags.N));
        }

        // ushort + ushort = ushort tests

        [Fact]
        public void When_UshortHalfOverflow_HalfCarrySet()
        {
            registers.Reset();
            // Half carry for 16 bit is only considered in the upper byte nibbles
            registers.ClearFlags(RegisterFlags.H);
            alu.Add((ushort)(1 << 11), (ushort)(1 << 11));
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact]
        public void When_UshortOverflow_CarrySet()
        {
            registers.Reset();
            registers.ClearFlags(RegisterFlags.C);
            var result = alu.Add((ushort)(1 << 15), (ushort)(1 << 15));
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }

        [Fact]
        public void When_UshortResultZero_ZeroFlagUnchanged()
        {
            registers.Reset();
            // 16 bit add should not change Z flag
            registers.ClearFlags(RegisterFlags.Z);
            var result = alu.Add((ushort)(1 << 15), (ushort)(1 << 15));
            Assert.Equal(0, result);
            Assert.False(registers.AreFlagsSet(RegisterFlags.Z));

            registers.SetFlags(RegisterFlags.Z);
            result = alu.Add((ushort)(1 << 15), (ushort)(1 << 15));
            Assert.Equal(0, result);
            Assert.True(registers.AreFlagsSet(RegisterFlags.Z));
        }

        [Fact]
        public void When_UshortAddCalled_NisCleared()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.N);
            alu.Add((ushort) 1, (ushort) 1);
            Assert.False(registers.AreFlagsSet(RegisterFlags.N));
        }
    }
}
