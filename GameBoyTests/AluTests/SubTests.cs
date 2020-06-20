
using GameBoy.CpuArchitecture;
using Xunit;

namespace GameBoyTests.AluTests
{
    public class SubTests
    {
        private Registers registers;
        private Alu alu;

        public SubTests()
        {
            registers = new Registers();
            alu = new Alu(registers);
        }

        // SUB tests

        [Fact(DisplayName = "When sub is called N flag set")]
        public void When_SubCalled_NflagSet()
        {
            registers.Reset();
            alu.Sub(10, 1);
            Assert.True(registers.AreFlagsSet(RegisterFlags.N));
        }

        [Fact(DisplayName = "When 10 - 10 is called Z flag set")]
        public void When_10sub10_ZflagSet()
        {
            registers.Reset();
            alu.Sub(10, 10);
            Assert.True(registers.AreFlagsSet(RegisterFlags.Z));
        }

        [Fact(DisplayName = "When byte2 & 0x0F > byte1 & 0x0F half carry flag should be set")]
        public void SubHalfCarryFlagTest()
        {
            registers.Reset();
            alu.Sub(0xF1, 0xE2);
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact(DisplayName = "When byte2 > byte1 carry flag should be set")]
        public void SubCarryFlagTest()
        {
            registers.Reset();
            alu.Sub(10, 20);
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }


        // SBC tests
        // SBC uses same flag setting algorithm as SUB so only need to test that carry flag affects addition

        [Fact(DisplayName = "When C is set 20 - 10 (-1) = 9")]
        public void SbcCarryFlagIsSetExpectExtraSubtraction()
        {
            registers.Reset();
            registers.SetFlags(RegisterFlags.C);
            var result = alu.Sbc(20, 10);
            Assert.Equal(9, result);
        }

        [Fact(DisplayName = "When C is clear 20 - 10 = 10")]
        public void SbcCarryFlagIsClearedExpectNormalSubtraction()
        {
            registers.Reset();
            var result = alu.Sbc(20, 10);
            Assert.Equal(10, result);
        }
    }
}
