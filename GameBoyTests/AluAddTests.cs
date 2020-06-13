using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBoy.Architecture;
using Xunit;

namespace GameBoyTests
{
    public class AluAddTests
    {
        [Fact]
        public void When_HalfOverFlow_Expect_HalfCarryFlagSet()
        {
            Registers registers = new Registers();
            registers.A = 15;
            registers.B = 1;
            Alu alu = new Alu(registers);
            registers.A = alu.Add(registers.B);
            Assert.True(registers.AreFlagsSet(RegisterFlags.H));
        }

        [Fact]
        public void When_OverFlow_Expect_CarryFlagSet()
        {
            Registers registers = new Registers();
            registers.A = 255;
            registers.B = 1;
            Alu alu = new Alu(registers);
            registers.A = alu.Add(registers.B);
            Assert.True(registers.AreFlagsSet(RegisterFlags.C));
        }

        [Fact]
        public void When_ResultIsZero_Expect_ZeroFlagSet()
        {
            Registers registers = new Registers();
            registers.A = 255;
            registers.B = 1;
            Alu alu = new Alu(registers);
            registers.A = alu.Add(registers.B);
            Assert.True(registers.AreFlagsSet(RegisterFlags.Z));
        }
    }
}
