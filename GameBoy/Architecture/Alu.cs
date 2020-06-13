using GameBoy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RegisterFlags flags = RegisterFlags.None;

            if ((carryBits & 0x100) == 0x100)
                flags |= RegisterFlags.C;

            if ((carryBits & 0x10) == 0x10)
                flags |= RegisterFlags.H;

            if (result == 0)
                flags |= RegisterFlags.Z;

            _registers.SetFlags(flags);

            return result;

        }

        public byte Add(byte targetValue)
        {
            return Perform(_registers.A, targetValue, (x, y) => x + y);
        }
    }
}
