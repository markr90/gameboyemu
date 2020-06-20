using GameBoy.CpuArchitecture;
using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using static GameBoy.CpuArchitecture.RegisterFlags;

namespace GameBoy.CpuArchitecture
{


    public class Alu
    {
        private Registers _registers;

        public Alu(Registers registers)
        {
            _registers = registers;
        }

        private ushort PerformMath(ushort a, ushort b, Func<ushort, ushort, int> function, RegisterFlags affectedFlags)
        {
            int intermediate = function(a, b);
            ushort result = (ushort)(intermediate & 0xFFFF);
            int carryBits = a ^ b ^ intermediate;

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((carryBits & 0x10000) == 0x10000)
                flags |= C;

            if ((carryBits & 0x1000) == 0x1000)
                flags |= H;

            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);
            return result;

        }

        private ushort PerformMath(ushort a, sbyte b, Func<ushort, sbyte, int> function, RegisterFlags affectedFlags)
        {
            int intermediate = function(a, b);
            ushort result = (ushort)(intermediate & 0xFFFF);
            int carryBits = a ^ b ^ intermediate;

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((carryBits & 0x100) == 0x100)
                flags |= C;

            if ((carryBits & 0x10) == 0x10)
                flags |= H;

            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);
            return result;

        }

        private byte PerformMath(byte a, byte b, Func<byte, byte, int> function, RegisterFlags affectedFlags)
        {
            int intermediate = function(a, b);
            int carryBits = a ^ b ^ intermediate;
            byte result = (byte)(intermediate & 0xFF);

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((carryBits & 0x100) == 0x100)
                flags |= C;

            if ((carryBits & 0x10) == 0x10)
                flags |= H;

            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);
            return result;

        }

        /// <summary>
        /// Adds target value to value of register A
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Add(byte a, byte targetValue)
        {
            // affected flags z0hc
            _registers.ClearFlags(N);
            return PerformMath(a, targetValue, (x, y) => x + y, Z | H | C);
        }

        public ushort Add(ushort a, ushort b)
        {
            // affected flags -0hc
            _registers.ClearFlags(N);
            return PerformMath(a, b, (x, y) => x + y, H | C);
        }

        public ushort Add(ushort a, sbyte b)
        {
            _registers.ClearFlags(Z | N);
            return PerformMath(a, b, (x, y) => x + y, H | C);
        }

        public byte Adc(byte a, byte targetValue)
        {
            // affected flags z0hc
            int carry = _registers.AreFlagsSet(C) ? 1 : 0;
            _registers.ClearFlags(N);
            return PerformMath(a, targetValue, (x, y) => x + y + carry, Z | H | C);
        }

        /// <summary>
        /// Subtracts target value to value of register A
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Sub(byte a, byte targetValue)
        {
            // affected flags z1hc
            _registers.SetFlags(N);
            return PerformMath(a, targetValue, (x, y) => x - y, Z | H | C);
        }

        public byte Sbc(byte a, byte targetValue)
        {
            // affected flags z1hc
            _registers.SetFlags(N);
            int carry = _registers.AreFlagsSet(C) ? 1 : 0;
            return PerformMath(a, targetValue, (x, y) => x - y - carry, Z | H | C);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public ushort Inc(ushort targetValue)
        {
            // affected flags ----
            return PerformMath(targetValue, (ushort) 1, (x, y) => x + y, None);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Inc(byte targetValue)
        {
            // affected flags z0h-
            _registers.ClearFlags(N);
            return PerformMath(targetValue, (byte)1, (x, y) => x + y, Z | H);
        }

        public ushort Dec(ushort targetValue)
        {
            // affected flags ----
            return PerformMath(targetValue, (ushort)1, (x, y) => x - y, None);
        }

        public byte Dec(byte targetValue)
        {
            // affected flags z1h-
            _registers.SetFlags(N);
            return PerformMath(targetValue, (byte)1, (x, y) => x - y, Z | H);
        }

        // Left rotations

        public byte Rlca()
        {
            // affected flags 000c
            _registers.ClearFlags(Z | N | H);
            return RlcWork(_registers.A, C);
        }

        public byte Rla()
        {
            // affected flags 000c
            _registers.ClearFlags(Z | N | H);
            return RlWork(_registers.A, C);
        }

        public byte Rlc(byte targetValue)
        {
            // affected flags z00c
            _registers.ClearFlags(N | H);
            return RlcWork(targetValue, Z | C);
        }

        public byte Rl(byte targetValue)
        {
            // affected flags z00c
            _registers.ClearFlags(N | H);
            return RlWork(targetValue, Z | C);
        }

        private byte RlWork(byte targetValue, RegisterFlags affectedFlags)
        {
            byte carry = (byte)(_registers.AreFlagsSet(C) ? 1 : 0);
            byte result = (byte)((targetValue << 1) | carry);

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((targetValue & (1 << 7)) == (1 << 7))
                flags |= C;
            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);

            return result;
        }

        private byte RlcWork(byte targetValue, RegisterFlags affectedFlags)
        {
            byte result = (byte)((targetValue << 1) | (targetValue >> 7));

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((targetValue & (1 << 7)) == (1 << 7))
                flags |= C;
            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);

            return result;
        }

        // Right rotations

        public byte Rrca()
        {
            // affected flags 000c
            _registers.ClearFlags(Z | N | H);
            return RrcWork(_registers.A, C);
        }

        public byte Rra()
        {
            // affected flags 000c
            _registers.ClearFlags(Z | N | H);
            return RrWork(_registers.A, C);
        }

        public byte Rrc(byte targetValue)
        {
            // affected flags z00c
            _registers.ClearFlags(N | H);
            return RrcWork(targetValue, Z | C);
        }

        public byte Rr(byte targetValue)
        {
            // affected flags z00c
            _registers.ClearFlags(N | H);
            return RrWork(targetValue, Z | C);
        }

        private byte RrcWork(byte targetValue, RegisterFlags affectedFlags)
        {
            byte result = (byte)((targetValue >> 1) | ((targetValue & 1) << 7));

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((targetValue & (1 << 7)) == (1 << 7))
                flags |= C;
            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);

            return result;
        }

        private byte RrWork(byte targetValue, RegisterFlags affectedFlags)
        {
            byte carry = (byte)(_registers.AreFlagsSet(C) ? 1 << 7 : 0);
            byte result = (byte)((targetValue >> 1) | carry);

            _registers.ClearFlags(affectedFlags);
            RegisterFlags flags = None;

            if ((targetValue & (1 << 7)) == (1 << 7))
                flags |= C;
            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);

            return result;
        }

        public byte Cpl(byte value)
        {
            // affected flags -11-
            _registers.SetFlags(N | H);
            return (byte)~(value);
        }

        public void Scf()
        {
            // affected flags -001
            _registers.ClearFlags(N | H);
            _registers.SetFlags(C);
        }

        public void Ccf()
        {
            // affected flags -00c
            _registers.ClearFlags(N | H);
            _registers.InvertFlags(C);
        }

        public byte And(byte a, byte b)
        {
            // affected flags z010
            _registers.ClearFlags(Z | N | C);
            _registers.SetFlags(H);
            byte result = (byte) (a & b);
            if (result == 0)
                _registers.SetFlags(Z);

            return result;
        }

        public byte Xor(byte a, byte b)
        {
            // affected flags z000
            _registers.ClearFlags(All);
            byte result = (byte)(a ^ b);
            if (result == 0)
                _registers.SetFlags(Z);
            return result;
        }

        public byte Or(byte a, byte b)
        {
            // affected flags z000
            _registers.ClearFlags(All);
            byte result = (byte)(a | b);
            if (result == 0)
                _registers.SetFlags(Z);
            return result;
        }

        public void Cp(byte a, byte b)
        {
            // affected flags z1hc
            // Same as subtraction a - b but discard result
            PerformMath(a, b, (x, y) => x - y, Z | H | C);
            _registers.SetFlags(N);
        }

        public void Daa()
        {
            // TODO implement
        }
    }
}
