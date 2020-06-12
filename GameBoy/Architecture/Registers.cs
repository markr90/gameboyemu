using GameBoyCPU.Architecture;
using System;
using System.Runtime.InteropServices;

namespace GameBoy.Architecture
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public class Registers
    {
        [FieldOffset(0)] public byte A;
        [FieldOffset(1)] public byte F;
        [FieldOffset(0)] public ushort AF;

        [FieldOffset(2)] public byte B;
        [FieldOffset(3)] public byte C;
        [FieldOffset(2)] public ushort BC;

        [FieldOffset(4)] public byte D;
        [FieldOffset(5)] public byte E;
        [FieldOffset(4)] public ushort DE;

        [FieldOffset(6)] public byte H;
        [FieldOffset(7)] public byte L;
        [FieldOffset(6)] public ushort HL;

        [FieldOffset(8)] public ushort SP;
        [FieldOffset(10)] public ushort PC;

        public FlagsRegister GetFlagRegister()
        {
            return new FlagsRegister(F);
        }

        public void SetFlagRegister(FlagsRegister fr)
        {
            F = fr.GetByteValue();
        }

        public byte ReadByte(Register target)
        {
            switch (target)
            {
                case Register.A:
                    return A;
                case Register.F:
                    return F;
                case Register.B:
                    return B;
                case Register.C:
                    return C;
                case Register.D:
                    return D;
                case Register.E:
                    return E;
                case Register.H:
                    return H;
                case Register.L:
                    return L;
                default:
                    throw new ArgumentException("Unknown 8 bit register.");
            }
        }
    }

    public enum Register
    {
        A, F, B, C, D, E, H, L,
        AF, BC, DE, HL, SP, PC
    }
}
