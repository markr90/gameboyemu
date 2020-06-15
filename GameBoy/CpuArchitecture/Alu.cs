using GameBoy.CpuArchitecture;
using System;

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
        public byte Add(byte targetValue)
        {
            _registers.ClearFlags(N);
            return Perform(_registers.A, targetValue, (x, y) => x + y, Z | H | C);
        }

        /// <summary>
        /// Subtracts target value to value of register A
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Sub(byte targetValue)
        {
            _registers.SetFlags(N);
            return Perform(_registers.A, targetValue, (x, y) => x - y, Z | H | C);
        }

        public ushort Add(ushort a, ushort b)
        {
            _registers.ClearFlags(N);
            return Perform(a, b, (x, y) => x + y, H | C);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public ushort Increment(ushort targetValue)
        {
            return Perform(targetValue, 1, (x, y) => x + y, None);
        }

        /// <summary>
        /// Increments target value by one
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public byte Increment(byte targetValue)
        {
            _registers.ClearFlags(N);
            return Perform(targetValue, 1, (x, y) => x + y, Z | H);
        }

        public ushort Decrement(ushort targetValue)
        {
            return Perform(targetValue, 1, (x, y) => x - y, None);
        }

        public byte Decrement(byte targetValue)
        {
            _registers.SetFlags(N);
            return Perform(targetValue, 1, (x, y) => x - y, Z | H);
        }

        public byte RlcA()
        {
            return Rlc(_registers.A, C);
        }

        public byte Rlc(byte targetValue)
        {
            return Rlc(targetValue, Z | C);
        }

        private byte Rlc(byte targetValue, RegisterFlags affectedFlags)
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
    }
}
