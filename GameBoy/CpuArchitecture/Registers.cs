using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace GameBoy.CpuArchitecture
{
    [StructLayout(LayoutKind.Explicit)]
    public class Registers
    {
        // FA makes 16 bit field AF (swapped around due to most significant bit = left)
        [FieldOffset(0)] public byte F;
        [FieldOffset(1)] public byte A;
        [FieldOffset(0)] public ushort AF;

        [FieldOffset(2)] public byte C;
        [FieldOffset(3)] public byte B;
        [FieldOffset(2)] public ushort BC;

        [FieldOffset(4)] public byte E;
        [FieldOffset(5)] public byte D;
        [FieldOffset(4)] public ushort DE;

        [FieldOffset(6)] public byte L;
        [FieldOffset(7)] public byte H;
        [FieldOffset(6)] public ushort HL;

        public void Reset()
        {
            A = B = C = D = E = F = H = L = 0;
        }

        public void ResetFlags()
        {
            F = 0;
        }

        public void ClearFlags(RegisterFlags flags)
        {
            F &= (byte) ~(byte)flags;
        }

        public void SetFlags(RegisterFlags flags)
        {
            F |= (byte)flags;
        }

        public void InvertFlags(RegisterFlags flags)
        {
            F ^= (byte)flags;
        }

        public bool AreFlagsSet(RegisterFlags flags)
        {
            if (flags == RegisterFlags.None)
            {
                return F == 0;
            }
            return (F & (byte)flags) == (byte)flags;
        }

        public override string ToString()
        {
            return string.Format("AF: {0:X4}\r\n" +
                                 "BC: {1:X4}\r\n" +
                                 "DE: {2:X4}\r\n" +
                                 "HL: {3:X4}\r\n" +
                                 AF, BC, DE, HL);
        }

        private string AsBitString(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }
        private string AsBitString(ushort b)
        {
            string s = Convert.ToString(b, 2).PadLeft(16, '0');
            string result = Regex.Replace(s, ".{8}", "$0 ");
            return result;
        }

        public string ToBitString()
        {
            return string.Format(
                "AF: {0} {1}\r\n" +
                "BC: {2} {3}\r\n" +
                "DE: {4} {5}\r\n" +
                "HL: {6} {7}\r\n" +
                AsBitString(A), AsBitString(F), AsBitString(B), AsBitString(C), AsBitString(D), AsBitString(E), AsBitString(H), AsBitString(L)
            );
        }
    }
}
