
namespace GameBoy.Architecture
{
    public static class OpCodes
    {
        public static readonly OpCode[] SingleByteOpCodes =
        {
            // 0x00 - 0x0F
            new OpCode(4, cpu => { }),
            new OpCode(12, cpu => cpu.registers.BC = cpu.ReadImmediate16()),

            // 0x80 - 0x8F
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.B)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.C)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.D)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.E)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.H)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.L)),
            new OpCode(8, cpu => cpu.registers.A = cpu.Alu.Add(cpu.memory.ReadByte(cpu.registers.HL))),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Add(cpu.registers.A)),

            // 0x90 .. 0x9F
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.B)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.C)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.D)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.E)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.H)),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.L)),
            new OpCode(8, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.memory.ReadByte(cpu.registers.HL))),
            new OpCode(4, cpu => cpu.registers.A = cpu.Alu.Sub(cpu.registers.A)),

        };
    }
}
