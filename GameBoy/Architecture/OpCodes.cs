using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Architecture
{
    public static class OpCodes
    {
        public static readonly OpCode[] SingleByteOpCodes =
        {

            // 0x80 - 0x8F
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.B)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.C)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.D)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.E)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.H)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.L)),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.memory.ReadByte(cpu.registers.HL))),
            new OpCode(cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.A)),

        };
    }
}
