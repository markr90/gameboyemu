using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Architecture
{
    public class Memory
    {
        private byte[] _memory;

        public Memory()
        {
            _memory = new byte[256];
        }

        public byte ReadByte(ushort address)
        {
            return _memory[address];
        }

        public byte[] ReadBytes(ushort address, int length)
        {
            byte[] buffer = new byte[length];

            for (int i = 0; i < length; i++)
            {
                buffer[i] = ReadByte((ushort)(address + i));
            }

            return buffer;
        }
    }
}
