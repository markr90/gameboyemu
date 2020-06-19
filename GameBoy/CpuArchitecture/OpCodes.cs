
namespace GameBoy.CpuArchitecture
{
    public static class OpCodes
    {
        public const byte ExtendedTableOpCode = 0xCB;

        public static readonly OpCode[] SingleByteOpCodes =
        {
            // 0x00 - 0x0F
            new OpCode(0x00, "NOP",         4,  0, (cpu, i) => { }),
            new OpCode(0x01, "LD BC d16" ,  12, 2, (cpu, i) => cpu.Registers.BC = i.Operand16 ),
            new OpCode(0x02, "LD (BC) A",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.BC, cpu.Registers.A)),
            new OpCode(0x03, "INC BC",      8,  0, (cpu, i) => cpu.Registers.BC = cpu.Alu.Inc(cpu.Registers.BC)),
            new OpCode(0x04, "INC B",       4,  0, (cpu, i) => cpu.Registers.B = cpu.Alu.Inc(cpu.Registers.B)),
            new OpCode(0x05, "DEC B",       4,  0, (cpu, i) => cpu.Registers.B = cpu.Alu.Dec(cpu.Registers.B)),
            new OpCode(0x06, "LD B d8",     8,  1, (cpu, i) => cpu.Registers.B = i.Operand8),
            new OpCode(0x07, "RLCA",        4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Rlca()),
            new OpCode(0x08, "LD (d16) SP", 20, 2, (cpu, i) => cpu.MemController.Write(i.Operand16, cpu.SP)),
            new OpCode(0x09, "ADD HL BC",   8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.Registers.BC)),
            new OpCode(0x0A, "LD A (BC)",   8,  0, (cpu, i) => cpu.Registers.A = cpu.MemController.Read(cpu.Registers.BC)),
            new OpCode(0x0B, "DEC BC",      8,  0, (cpu, i) => cpu.Registers.BC = cpu.Alu.Dec(cpu.Registers.BC)),
            new OpCode(0x0C, "INC C",       4,  0, (cpu, i) => cpu.Registers.C = cpu.Alu.Inc(cpu.Registers.C)),
            new OpCode(0x0D, "DEC C",       4,  0, (cpu, i) => cpu.Registers.C = cpu.Alu.Dec(cpu.Registers.C)),
            new OpCode(0x0E, "LD C d8",     8,  1, (cpu, i) => cpu.Registers.C = i.Operand8),
            new OpCode(0x0F, "RRCA",        4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Rrca()),

            // 0x10 - 0x1F
            new OpCode(0x10, "STOP 0",      4,  0, (cpu, i) => cpu.Stop()), // TODO Improve?
            new OpCode(0x11, "LD DE d16",   12, 2, (cpu, i) => cpu.Registers.DE = i.Operand16),
            new OpCode(0x12, "LD (DE) A",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.DE, cpu.Registers.A)),
            new OpCode(0x13, "INC DE",      8,  0, (cpu, i) => cpu.Registers.DE = cpu.Alu.Inc(cpu.Registers.DE)),
            new OpCode(0x14, "INC D",       4,  0, (cpu, i) => cpu.Registers.D = cpu.Alu.Inc(cpu.Registers.D)),
            new OpCode(0x15, "DEC D",       4,  0, (cpu, i) => cpu.Registers.D = cpu.Alu.Dec(cpu.Registers.D)),
            new OpCode(0x16, "LD D d8",     8,  1, (cpu, i) => cpu.Registers.D = i.Operand8),
            new OpCode(0x17, "RLA",         4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Rla()),
            new OpCode(0x18, "JR d8",       12, 1, (cpu, i) => cpu.JumpRelative((sbyte) i.Operand8)),
            new OpCode(0x19, "ADD HL DE",   8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.Registers.DE)),
            new OpCode(0x1A, "LD A (DE)",   8,  0, (cpu, i) => cpu.Registers.A = cpu.MemController.Read(cpu.Registers.DE)),
            new OpCode(0x1B, "DEC DE",      8,  0, (cpu, i) => cpu.Registers.DE = cpu.Alu.Dec(cpu.Registers.DE)),
            new OpCode(0x1C, "INC E",       4,  0, (cpu, i) => cpu.Registers.E = cpu.Alu.Inc(cpu.Registers.E)),
            new OpCode(0x1D, "DEC E",       4,  0, (cpu, i) => cpu.Registers.E = cpu.Alu.Dec(cpu.Registers.E)),
            new OpCode(0x1E, "LD E d8",     8,  1, (cpu, i) => cpu.Registers.E = i.Operand8),
            new OpCode(0x1F, "RRA",         4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Rra()),

            // 0x20 - 0x2F
            new OpCode(0x20, "JR NZ r8",    12, 8, 1, (cpu, i) => cpu.JumpRelativeConditional(i.OpCode, (sbyte) i.Operand8, !cpu.Registers.AreFlagsSet(RegisterFlags.Z))),
            new OpCode(0x21, "LD HL d16",   12, 2, (cpu, i) => cpu.Registers.HL = i.Operand16),
            new OpCode(0x22, "LD (HL+) A",  8,  0, (cpu, i) => { cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.A); cpu.Registers.HL = cpu.Alu.Inc(cpu.Registers.HL); }),
            new OpCode(0x23, "INC HL",      8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Inc(cpu.Registers.HL)),
            new OpCode(0x24, "INC H",       4,  0, (cpu, i) => cpu.Registers.H = cpu.Alu.Inc(cpu.Registers.H)),
            new OpCode(0x25, "DEC H",       4,  0, (cpu, i) => cpu.Registers.H = cpu.Alu.Dec(cpu.Registers.H)),
            new OpCode(0x26, "LD H d8",     8,  1, (cpu, i) => cpu.Registers.H = i.Operand8),
            // TODO 0x27 DAA
            new OpCode(0x28, "JR Z r8",     12, 8, 1, (cpu, i) => cpu.JumpRelativeConditional(i.OpCode, (sbyte) i.Operand8, cpu.Registers.AreFlagsSet(RegisterFlags.Z))),
            new OpCode(0x29, "ADD HL HL",   8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.Registers.HL)),
            new OpCode(0x2A, "LD A (HL+)",  8,  0, (cpu, i) => { cpu.Registers.A = cpu.MemController.Read(cpu.Registers.HL); cpu.Registers.HL = cpu.Alu.Inc(cpu.Registers.HL); }),
            new OpCode(0x2B, "DEC HL",      8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Dec(cpu.Registers.HL)),
            new OpCode(0x2C, "INC L",       4,  0, (cpu, i) => cpu.Registers.L = cpu.Alu.Inc(cpu.Registers.L)),
            new OpCode(0x2D, "DEC L",       4,  0, (cpu, i) => cpu.Registers.L = cpu.Alu.Dec(cpu.Registers.L)),
            new OpCode(0x2E, "LD L d8",     8,  1, (cpu, i) => cpu.Registers.L = i.Operand8),
            // TODO 0x2F CPL

            // 0x30 - 0x3F
            new OpCode(0x30, "JR NC r8",    12, 8, 1, (cpu, i) => cpu.JumpRelativeConditional(i.OpCode, (sbyte) i.Operand8, !cpu.Registers.AreFlagsSet(RegisterFlags.C))),
            new OpCode(0x31, "LD SP d16",   12, 2, (cpu, i) => cpu.SP = i.Operand16),
            new OpCode(0x32, "LD (HL-) A",  8,  0, (cpu, i) => { cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.A); cpu.Registers.HL = cpu.Alu.Dec(cpu.Registers.HL); }),
            new OpCode(0x33, "INC SP",      8,  0, (cpu, i) => cpu.SP = cpu.Alu.Inc(cpu.SP)),
            new OpCode(0x34, "INC (HL)",    12, 0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Alu.Inc(cpu.MemController.Read(cpu.Registers.HL)))),
            new OpCode(0x35, "DEC (HL)",    12, 0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Alu.Dec(cpu.MemController.Read(cpu.Registers.HL)))),
            new OpCode(0x36, "LD (HL) d8",  12, 1, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, i.Operand8)),
            // TODO 0x37 SCF
            new OpCode(0x38, "JR C r8",     12, 8, 1, (cpu, i) => cpu.JumpRelativeConditional(i.OpCode, (sbyte) i.Operand8, cpu.Registers.AreFlagsSet(RegisterFlags.C))),
            new OpCode(0x39, "ADD HL SP",   8,  0, (cpu, i) => cpu.Registers.HL = cpu.Alu.Add(cpu.Registers.HL, cpu.SP)),
            new OpCode(0x3A, "LD A (HL-)",  8,  0, (cpu, i) => { cpu.Registers.A = cpu.MemController.Read(cpu.Registers.HL); cpu.Registers.HL = cpu.Alu.Dec(cpu.Registers.HL); }),
            new OpCode(0x3B, "DEC SP",      8,  0, (cpu, i) => cpu.SP = cpu.Alu.Dec(cpu.SP)),
            new OpCode(0x3C, "INC A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Inc(cpu.Registers.A)),
            new OpCode(0x3D, "DEC A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Dec(cpu.Registers.A)),
            new OpCode(0x3E, "LD A d8",     8,  1, (cpu, i) => cpu.Registers.A = i.Operand8),
            // TODO 0x3F CCF

            // 0x40 - 0x4F
            new OpCode(0x40, "LD B B",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.B),
            new OpCode(0x41, "LD B C",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.C),
            new OpCode(0x42, "LD B D",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.D),
            new OpCode(0x43, "LD B E",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.E),
            new OpCode(0x44, "LD B H",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.H),
            new OpCode(0x45, "LD B L",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.L),
            new OpCode(0x46, "LD B (HL)",   8,  0, (cpu, i) => cpu.Registers.B = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x47, "LD B A",      4,  0, (cpu, i) => cpu.Registers.B = cpu.Registers.A),
            new OpCode(0x48, "LD C B",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.B),
            new OpCode(0x49, "LD C C",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.C),
            new OpCode(0x4A, "LD C D",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.D),
            new OpCode(0x4B, "LD C E",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.E),
            new OpCode(0x4C, "LD C H",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.H),
            new OpCode(0x4D, "LD C L",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.L),
            new OpCode(0x4E, "LD C (HL)",   8,  0, (cpu, i) => cpu.Registers.C = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x4F, "LD C A",      4,  0, (cpu, i) => cpu.Registers.C = cpu.Registers.A),

            // 0x50 - 0x5F
            new OpCode(0x50, "LD D B",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.B),
            new OpCode(0x51, "LD D C",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.C),
            new OpCode(0x52, "LD D D",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.D),
            new OpCode(0x53, "LD D E",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.E),
            new OpCode(0x54, "LD D H",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.H),
            new OpCode(0x55, "LD D L",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.L),
            new OpCode(0x56, "LD D (HL)",   8,  0, (cpu, i) => cpu.Registers.D = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x57, "LD D A",      4,  0, (cpu, i) => cpu.Registers.D = cpu.Registers.A),
            new OpCode(0x58, "LD E B",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.B),
            new OpCode(0x59, "LD E C",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.C),
            new OpCode(0x5A, "LD E D",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.D),
            new OpCode(0x5B, "LD E E",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.E),
            new OpCode(0x5C, "LD E H",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.H),
            new OpCode(0x5D, "LD E L",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.L),
            new OpCode(0x5E, "LD E (HL)",   8,  0, (cpu, i) => cpu.Registers.E = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x5F, "LD E A",      4,  0, (cpu, i) => cpu.Registers.E = cpu.Registers.A),

            // 0x60 - 0x6F
            new OpCode(0x60, "LD H B",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.B),
            new OpCode(0x61, "LD H C",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.C),
            new OpCode(0x62, "LD H D",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.D),
            new OpCode(0x63, "LD H E",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.E),
            new OpCode(0x64, "LD H H",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.H),
            new OpCode(0x65, "LD H L",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.L),
            new OpCode(0x66, "LD H (HL)",   8,  0, (cpu, i) => cpu.Registers.H = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x67, "LD H A",      4,  0, (cpu, i) => cpu.Registers.H = cpu.Registers.A),
            new OpCode(0x68, "LD L B",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.B),
            new OpCode(0x69, "LD L C",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.C),
            new OpCode(0x6A, "LD L D",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.D),
            new OpCode(0x6B, "LD L E",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.E),
            new OpCode(0x6C, "LD L H",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.H),
            new OpCode(0x6D, "LD L L",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.L),
            new OpCode(0x6E, "LD L (HL)",   8,  0, (cpu, i) => cpu.Registers.L = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x6F, "LD L A",      4,  0, (cpu, i) => cpu.Registers.L = cpu.Registers.A),

            // 0x70 - 0x7F
            new OpCode(0x70, "LD (HL) B",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.B)),
            new OpCode(0x71, "LD (HL) C",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.C)),
            new OpCode(0x72, "LD (HL) D",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.D)),
            new OpCode(0x73, "LD (HL) E",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.E)),
            new OpCode(0x74, "LD (HL) H",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.H)),
            new OpCode(0x75, "LD (HL) L",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.L)),
            new OpCode(0x76, "HALT",        4,  0, (cpu, i) => cpu.Halt()),
            new OpCode(0x77, "LD (HL) A",   8,  0, (cpu, i) => cpu.MemController.Write(cpu.Registers.HL, cpu.Registers.A)),
            new OpCode(0x78, "LD A B",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.B),
            new OpCode(0x79, "LD A C",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.C),
            new OpCode(0x7A, "LD A D",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.D),
            new OpCode(0x7B, "LD A E",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.E),
            new OpCode(0x7C, "LD A H",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.H),
            new OpCode(0x7D, "LD A L",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.L),
            new OpCode(0x7E, "LD A (HL)",   8,  0, (cpu, i) => cpu.Registers.A = cpu.MemController.Read(cpu.Registers.HL)),
            new OpCode(0x7F, "LD A A",      4,  0, (cpu, i) => cpu.Registers.A = cpu.Registers.A),

            // 0x80 - 0x8F
            new OpCode(0x80, "ADD B",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.B)),
            new OpCode(0x81, "ADD C",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.C)),
            new OpCode(0x82, "ADD D",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.D)),
            new OpCode(0x83, "ADD E",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.E)),
            new OpCode(0x84, "ADD H",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.H)),
            new OpCode(0x85, "ADD L",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.L)),
            new OpCode(0x86, "ADD (HL)",    8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.MemController.Read(cpu.Registers.HL))),
            new OpCode(0x87, "ADD A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Add(cpu.Registers.A, cpu.Registers.A)),
            new OpCode(0x88, "ADC B",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.B)),
            new OpCode(0x89, "ADC C",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.C)),
            new OpCode(0x8A, "ADC D",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.D)),
            new OpCode(0x8B, "ADC E",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.E)),
            new OpCode(0x8C, "ADC H",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.H)),
            new OpCode(0x8D, "ADC L",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.L)),
            new OpCode(0x8E, "ADC (HL)",    8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.MemController.Read(cpu.Registers.HL))),
            new OpCode(0x8F, "ADC A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Adc(cpu.Registers.A, cpu.Registers.A)),

            // 0x90 .. 0x9F
            new OpCode(0x90, "SUB B",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.B)),
            new OpCode(0x91, "SUB C",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.C)),
            new OpCode(0x92, "SUB D",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.D)),
            new OpCode(0x93, "SUB E",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.E)),
            new OpCode(0x94, "SUB H",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.H)),
            new OpCode(0x95, "SUB L",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.L)),
            new OpCode(0x96, "SUB (HL)",    8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.MemController.Read(cpu.Registers.HL))),
            new OpCode(0x97, "SUB A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sub(cpu.Registers.A, cpu.Registers.A)),
            new OpCode(0x98, "SBC B",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.B)),
            new OpCode(0x99, "SBC C",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.C)),
            new OpCode(0x9A, "SBC D",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.D)),
            new OpCode(0x9B, "SBC E",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.E)),
            new OpCode(0x9C, "SBC H",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.H)),
            new OpCode(0x9D, "SBC L",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.L)),
            new OpCode(0x9E, "SBC (HL)",    8,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.MemController.Read(cpu.Registers.HL))),
            new OpCode(0x9F, "SBC A",       4,  0, (cpu, i) => cpu.Registers.A = cpu.Alu.Sbc(cpu.Registers.A, cpu.Registers.A)),


        };

        public static readonly OpCode[] PrefixedOpCodes =
        {

        };
    }
}
