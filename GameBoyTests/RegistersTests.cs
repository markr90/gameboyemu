using GameBoy.CpuArchitecture;
using System.Runtime.InteropServices;
using Xunit;
using static GameBoy.CpuArchitecture.RegisterFlags;

namespace GameBoyTests
{
    public class RegistersTests
    {
        Registers registers = new Registers();

        [Fact(DisplayName = "Two adjacent bytes (0xF0 and 0x0F) should combine to proper ushort (0xF00F)")]
        public void BytesCombineToCorrectUshort()
        {
            registers.A = 0xF0;
            registers.F = 0x0F;
            registers.B = 0xF0;
            registers.C = 0x0F;
            registers.D = 0xF0;
            registers.E = 0x0F;
            registers.H = 0xF0;
            registers.L = 0x0F;
            Assert.Equal(0xF00F, registers.AF);
            Assert.Equal(0xF00F, registers.BC);
            Assert.Equal(0xF00F, registers.DE);
            Assert.Equal(0xF00F, registers.HL);
        }

        // Individual flag tests

        [Fact]
        public void When_ZisSet_Fis10000000()
        {
            registers.Reset();
            registers.SetFlags(Z);
            Assert.Equal(1 << 7, registers.F);
        }
        [Fact]
        public void When_NisSet_Fis01000000()
        {
            registers.Reset();
            registers.SetFlags(N);
            Assert.Equal(1 << 6, registers.F);
        }
        [Fact]
        public void When_HisSet_Fis00100000()
        {
            registers.Reset();
            registers.SetFlags(H);
            Assert.Equal(1 << 5, registers.F);
        }
        [Fact]
        public void When_CisSet_Fis00010000()
        {
            registers.Reset();
            registers.SetFlags(C);
            Assert.Equal(1 << 4, registers.F);
        }

        [Fact]
        public void ClearZflagTest()
        {
            registers.Reset();
            registers.F = 1 << 7;
            registers.ClearFlags(Z);
            Assert.Equal(0, registers.F);
        }

        [Fact]
        public void ClearNflagTest()
        {
            registers.Reset();
            registers.F = 1 << 6;
            registers.ClearFlags(N);
            Assert.Equal(0, registers.F);
        }

        [Fact]
        public void ClearHflagTest()
        {
            registers.Reset();
            registers.F = 1 << 5;
            registers.ClearFlags(H);
            Assert.Equal(0, registers.F);
        }

        [Fact]
        public void ClearCflagTest()
        {
            registers.Reset();
            registers.F = 1 << 4;
            registers.ClearFlags(C);
            Assert.Equal(0, registers.F);
        }

        [Fact]
        public void When_AllFlagsCleared_Fis0()
        {
            registers.Reset();
            registers.F = 0b11110000;
            registers.ClearFlags(All);
            Assert.Equal(0, registers.F);
        }

        [Fact]
        public void When_NoneFlagsCleared_FisUnchanged()
        {
            registers.Reset();
            registers.F = 0b11110000;
            registers.ClearFlags(None);
            Assert.Equal(0b11110000, registers.F);
        }

        [Fact] 
        public void AreFlagsSetTests_Z()
        {
            registers.Reset();
            registers.F = 1 << 7;
            Assert.True(registers.AreFlagsSet(Z));
            registers.F = 0;
            Assert.False(registers.AreFlagsSet(Z));
        }

        [Fact]
        public void AreFlagsSetTests_N()
        {
            registers.Reset();
            registers.F = 1 << 6;
            Assert.True(registers.AreFlagsSet(N));
            registers.F = 0;
            Assert.False(registers.AreFlagsSet(N));
        }

        [Fact]
        public void AreFlagsSetTests_H()
        {
            registers.Reset();
            registers.F = 1 << 5;
            Assert.True(registers.AreFlagsSet(H));
            registers.F = 0;
            Assert.False(registers.AreFlagsSet(H));
        }

        [Fact]
        public void AreFlagsSetTests_C()
        {
            registers.Reset();
            registers.F = 1 << 4;
            Assert.True(registers.AreFlagsSet(C));
            registers.F = 0;
            Assert.False(registers.AreFlagsSet(C));
        }

        [Fact]
        public void When_AllAreSet_Fis11110000()
        {
            registers.Reset();
            registers.SetFlags(All);
            Assert.Equal(0b11110000, registers.F);
        }

        [Fact]
        public void When_NoneAreSet_FisUnchanged()
        {
            registers.Reset();
            registers.F = 0b11100000;
            registers.SetFlags(None);
            Assert.Equal(0b11100000, registers.F);
        }

        [Fact(DisplayName = "When all flags are set, does AreFlagsSet return true for multiple flag queries")]
        public void AreFlagsSetTestMultiple()
        {
            registers.Reset();
            registers.F = 0b11110000;
            Assert.True(registers.AreFlagsSet(H | Z));
            Assert.True(registers.AreFlagsSet(All));
            Assert.False(registers.AreFlagsSet(None));
        }

        [Fact(DisplayName = "When multiple Z and H are set F = 1010000")]
        public void MultipleSetFlagTest()
        {
            registers.Reset();
            registers.SetFlags(Z | H);
            Assert.Equal(0b10100000, registers.F);
        }

        [Fact(DisplayName = "When F = 11110000 and Z and H are cleared, F = 01010000")]
        public void MutliplerClearFlagsTest()
        {
            registers.Reset();
            registers.F = 0b11110000;
            registers.ClearFlags(Z | H);
            Assert.Equal(0b01010000, registers.F);
        }

        [Fact(DisplayName = "When F = 01110000 and Z and H are queried response is false")]
        public void MultipleQueryTest()
        {
            registers.Reset();
            registers.F = 0b01110000;
            Assert.False(registers.AreFlagsSet(Z | H));
        }

        [Fact(DisplayName = "When C is 1 invert flag sets C to 0")]
        public void CinversionTest()
        {
            registers.Reset();
            registers.F = 0b00010000;
            registers.InvertFlags(C);
            Assert.Equal(0b00000000, registers.F);
        }

        [Fact(DisplayName = "When Z and N are 1 invert Z | N sets Z and N to 0")]
        public void ZHinversionTest()
        {
            registers.Reset();
            registers.F = 0b11000000;
            registers.InvertFlags(Z | N);
            Assert.Equal(0b00000000, registers.F);
        }

        [Fact(DisplayName = "Inverting None leaves flags unchanged")]
        public void NoneInversionTest()
        {
            registers.Reset();
            registers.F = 0b11000000;
            registers.InvertFlags(None);
            Assert.Equal(0b11000000, registers.F);
        }

        [Fact(DisplayName = "Inverting all complements F")]
        public void AllInversionTest()
        {
            registers.Reset();
            registers.F = 0b10100000;
            registers.InvertFlags(All);
            Assert.Equal(0b01010000, registers.F);
        }
    }
}
