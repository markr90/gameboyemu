﻿
namespace GameBoy.CpuArchitecture
{
    public static class OpCodes
    {
        public const byte ExtendedTableOpCode = 0xCB;

        public static readonly OpCode[] SingleByteOpCodes =
        {
            // 0x00 - 0x0F
            new OpCode(0x00, 4,  0, (cpu, i) => { }),
            new OpCode(0x01, 12, 2, (cpu, i) => cpu.Registers.BC = i.Operand16 ),
            new OpCode(0x02, 8,  0, (cpu, i) => cpu.MemController.WriteByte(cpu.Registers.BC, cpu.Registers.A)),
            new OpCode(0x03, 8,  0, (cpu, i) => cpu.Registers.BC = cpu.Alu.Increment(cpu.Registers.BC)),
            new OpCode(0x04, 4,  0, (cpu, i) => cpu.Registers.B = cpu.Alu.Increment(cpu.Registers.B)),
            new OpCode(0x05, 4,  0, (cpu, i) => cpu.Registers.B = cpu.Alu.Decrement(cpu.Registers.B)),
            new OpCode(0x06, 8,  1, (cpu, i) => cpu.Registers.B = i.Operand8),
            new OpCode(0x07, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.RlcA()),
            new OpCode(0x08, 20, 2, (cpu, i) => cpu.MemController.WriteUInt16(i.Operand16, cpu.Registers.SP)),
            new OpCode(0x09, 8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.Registers.BC)),
            new OpCode(0x0A, 8,  0, (cpu, i) => cpu.Registers.A = cpu.MemController.ReadByte(cpu.Registers.BC)),
            new OpCode(0x0B, 8,  0, (cpu, i) => cpu.Registers.BC = cpu.Alu.Decrement(cpu.Registers.BC)),
            new OpCode(0x0C, 4,  0, (cpu, i) => cpu.Registers.C = cpu.Alu.Increment(cpu.Registers.C)),
            new OpCode(0x0D, 4,  0, (cpu, i) => cpu.Registers.C = cpu.Alu.Decrement(cpu.Registers.C)),
            new OpCode(0x0E, 8,  1, (cpu, i) => cpu.Registers.C = i.Operand8),
            // TODO: Implement 0x0F RRCA

            // 0x10 - 0x1F
            new OpCode(0x10, 4,  0, (cpu, i) => cpu.Stop()),
            new OpCode(0x11, 12, 2, (cpu, i) => cpu.Registers.DE = i.Operand16),
            new OpCode(0x12, 8,  0, (cpu, i) => cpu.MemController.WriteByte(cpu.Registers.DE, cpu.Registers.A)),
            new OpCode(0x13, 8,  0, (cpu, i) => cpu.Registers.DE = cpu.Alu.Increment(cpu.Registers.DE)),
            new OpCode(0x14, 4,  0, (cpu, i) => cpu.Registers.D = cpu.Alu.Increment(cpu.Registers.D)),
            new OpCode(0x15, 4,  0, (cpu, i) => cpu.Registers.D = cpu.Alu.Decrement(cpu.Registers.D)),
            new OpCode(0x16, 8,  1, (cpu, i) => cpu.Registers.D = i.Operand8),
            // TODO implement 0x17 RLA
            // TODO implement 0x18 JR r8
            new OpCode(0x19, 8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.Registers.DE)),
            new OpCode(0x1A, 8,  0, (cpu, i) => cpu.Registers.A = cpu.MemController.ReadByte(cpu.Registers.DE)),
            new OpCode(0x1B, 8,  0, (cpu, i) => cpu.Registers.DE = cpu.Alu.Decrement(cpu.Registers.DE)),
            new OpCode(0x1C, 4,  0, (cpu, i) => cpu.Registers.E = cpu.Alu.Increment(cpu.Registers.E)),
            new OpCode(0x1D, 4,  0, (cpu, i) => cpu.Registers.E = cpu.Alu.Decrement(cpu.Registers.E)),
            new OpCode(0x1E, 8,  1, (cpu, i) => cpu.Registers.E = i.Operand8),
            // TODO implement 0x1F RRA

            // 0x20 - 0x2F
            // TODO 0x20 JR NZ,r8
            new OpCode(0x21, 12, 2, (cpu, i) => cpu.Registers.HL = i.Operand16),
            new OpCode(0x22, 8,  0, (cpu, i) => {
                cpu.MemController.WriteByte(cpu.Registers.HL, cpu.Registers.A);
                cpu.Registers.HL = cpu.Alu.Increment(cpu.Registers.HL);
            }),
            new OpCode(0x23, 8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Increment(cpu.Registers.HL)),
            new OpCode(0x24, 4,  0, (cpu, i) => cpu.Registers.H = cpu.Alu.Increment(cpu.Registers.H)),


            // 0x80 - 0x8F
            new OpCode(0x80, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.B)),
            new OpCode(0x81, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.C)),
            new OpCode(0x82, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.D)),
            new OpCode(0x83, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.E)),
            new OpCode(0x84, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.H)),
            new OpCode(0x85, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.L)),
            new OpCode(0x86, 8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.MemController.ReadByte(cpu.Registers.HL))),
            new OpCode(0x87, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A)),

            // 0x90 .. 0x9F
            new OpCode(0x90, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.B)),
            new OpCode(0x91, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.C)),
            new OpCode(0x92, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.D)),
            new OpCode(0x93, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.E)),
            new OpCode(0x94, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.H)),
            new OpCode(0x95, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.L)),
            new OpCode(0x96, 8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.MemController.ReadByte(cpu.Registers.HL))),
            new OpCode(0x97, 4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A)),

        };

        public static readonly OpCode[] PrefixedOpCodes =
        {

        };
    }
}
