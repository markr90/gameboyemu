
using GameBoy.CpuArchitecture;
using Xunit;

namespace GameBoyTests.AluTests
{
    public class AddTests
    {
        private Registers registers;
        private Alu alu;

        public AddTests()
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

        // ushort + sbyte tests

        [Fact(DisplayName = "When 255 + 1 C flag is set")]
        public void SbyteCarryFlagTest()
        {
            registers.Reset();
            alu.Add((ushort)255, (sbyte)1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }

        [Fact(DisplayName = "When 15 + 1 expect H flag set")]
        public void SbyteHalfCarryFlagTest()
        {
            registers.Reset();
            alu.Add((ushort)15, (sbyte)1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact]
        public void When_SbyteAddCalled_ExpectNcleared()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.N);
            alu.Add((ushort)1, (sbyte)1);
            Assert.False(registers.AreFlagsSet(RegisterFlags.N));
        }
        [Fact]
        public void When_SbyteAddCalled_ExpectZcleared()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.Z);
            alu.Add((ushort)1, (sbyte)1);
            Assert.False(registers.AreFlagsSet(RegisterFlags.Z));
        }

        // ADC tests
        [Fact]
        public void When_CisCleared_Expect10plus10is20()
        {
            registers.Reset();
            var result = alu.Adc(10, 10);
            Assert.Equal(20, result);
        }
        [Fact]
        public void When_CisSetExpect10plus10is21()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.C);
            var result = alu.Adc(10, 10);
            Assert.Equal(21, result);
        }

        [Fact]
        public void When_ADC_expectNcleared()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.N);
            alu.Adc(10, 10);
            Assert.False(registers.AreFlagsSet(RegisterFlags.N));
        }

        [Fact(DisplayName = "When C is set 14 + 1 (+1) should set half carry flag")]
        public void When_CisSet_HalfCarryFlagSetFor14plus1()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.C);
            alu.Adc(14, 1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact(DisplayName = "When C is set 254 + 1 (+1) should set zero and carry flag")]
        public void When_CisSet_ZeroFlagSetFor254plus1()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.C);
            alu.Adc(254, 1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.Z));
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }
    }
}
