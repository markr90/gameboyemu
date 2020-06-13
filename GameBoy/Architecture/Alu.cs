using GameBoy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static GameBoy.Architecture.RegisterFlags;

namespace GameBoy.Architecture
{


    public class Alu
    {
        private Registers _registers;

        public Alu(Registers registers)
        {
            _registers = registers;
        }

        private byte Perform(byte a, byte b, Func<byte, byte, int> function)
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

            _registers.SetFlags(flags);

            return result;

        }

        public byte Add(byte targetValue)
        {
            _registers.ClearFlags(Z | H | C);
            _registers.SetFlags(None);
            return Perform(_registers.A, targetValue, (x, y) => x + y);
        }

        public byte Sub(byte targetValue)
        {
            _registers.ClearFlags(Z | H | C);
            _registers.SetFlags(N);
            return Perform(_registers.A, targetValue, (x, y) => x - y);
        }
    }
}
