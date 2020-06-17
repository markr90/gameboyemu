using GameBoy.CpuArchitecture;
using System;
using System.Net.Sockets;
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

        private ushort Perform(ushort a, ushort b, Func<ushort, ushort, int> function, RegisterFlags affectedFlags)
        {
            int intermediate = function(a, b);
            ushort result = (ushort)(intermediate & 0xFFFF);
            int carryBits = a ^ b ^ intermediate;

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

        private byte Perform(byte a, byte b, Func<byte, byte, int> function, RegisterFlags affectedFlags)
        {
            int intermediate = function(a, b);
            int carryBits = a ^ b ^ intermediate;
            byte result = (byte)(intermediate & 0xFF);

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
            _registers.ClearFlags(N);
            return Perform(a, targetValue, (x, y) => x + y, Z | H | C);
        }

        public ushort Add(ushort a, ushort b)
        {
            _registers.ClearFlags(N);
            return Perform(a, b, (x, y) => x + y, H | C);
        }

        public byte Adc(byte a, byte targetValue)
        {
            _registers.ClearFlags(N);
            int carry = _registers.AreFlagsSet(RegisterFlags.C) ? 1 : 0;
            return Perform(a, targetValue, (x, y) => x + y + carry, Z | H | C);
        }

        /// <summary>
        /// Subtracts target value to value of register A
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Sub(byte a, byte targetValue)
        {
            _registers.SetFlags(N);
            return Perform(a, targetValue, (x, y) => x - y, Z | H | C);
        }

        public byte Sbc(byte a, byte targetValue)
        {
            _registers.SetFlags(N);
            int carry = _registers.AreFlagsSet(RegisterFlags.C) ? 1 : 0;
            return Perform(a, targetValue, (x, y) => x - y - carry, Z | H | C);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public ushort Inc(ushort targetValue)
        {
            return Perform(targetValue, 1, (x, y) => x + y, None);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Inc(byte targetValue)
        {
            _registers.ClearFlags(N);
            return Perform(targetValue, 1, (x, y) => x + y, Z | H);
        }

        public ushort Dec(ushort targetValue)
        {
            return Perform(targetValue, 1, (x, y) => x - y, None);
        }

        public byte Dec(byte targetValue)
        {
            _registers.SetFlags(N);
            return Perform(targetValue, 1, (x, y) => x - y, Z | H);
        }

        // Left rotations

        public byte Rlca()
        {
            return RlcWork(_registers.A, C);
        }

        public byte Rla()
        {
            return RlWork(_registers.A, C);
        }

        public byte Rlc(byte targetValue)
        {
            return RlcWork(targetValue, Z | C);
        }

        public byte Rl(byte targetValue)
        {
            return RlWork(targetValue, Z | C);
        }

        private byte RlWork(byte targetValue, RegisterFlags affectedFlags)
        {
            _registers.ClearFlags(N | H);

            byte carry = (byte)(_registers.AreFlagsSet(C) ? 1 : 0);
            byte result = (byte)((targetValue << 1) | carry);
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
            _registers.ClearFlags(N | H);

            byte result = (byte)((targetValue << 1) | (targetValue >> 7));
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
            return RrcWork(_registers.A, C);
        }

        public byte Rra()
        {
            return RrWork(_registers.A, C);
        }

        public byte Rrc(byte targetValue)
        {
            return RrcWork(targetValue, Z | C);
        }

        public byte Rr(byte targetValue)
        {
            return RrWork(targetValue, Z | C);
        }

        private byte RrcWork(byte targetValue, RegisterFlags affectedFlags)
        {
            _registers.ClearFlags(N | H);

            byte result = (byte)((targetValue >> 1) | ((targetValue & 1) << 7));
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
            _registers.ClearFlags(N | H);

            byte carry = (byte)(_registers.AreFlagsSet(C) ? 1 << 7 : 0);
            byte result = (byte)((targetValue >> 1) | carry);
            RegisterFlags flags = None;

            if ((targetValue & (1 << 7)) == (1 << 7))
                flags |= C;
            if (result == 0)
                flags |= Z;

            _registers.SetFlags(flags & affectedFlags);

            return result;
        }

        public void Daa()
        {

        }
    }
}
