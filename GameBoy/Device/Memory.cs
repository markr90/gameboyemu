using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Device
{
    public class MemoryAccessFailure: Exception
    {
        public MemoryAccessFailure(ushort address)
            :base(String.Format("MEM_ACCESS_FAILURE: {0}", address))
        { }
    }

    public class Memory: IMemoryBus
    {
        private byte[] _memory;
        private int _size;

        public Memory(int size)
        {
            _memory = new byte[size];
            _size = size;
        }

        public void WriteByte(ushort address, byte value)
        {
            if (address < _size)
                _memory[address] = value;
            else
                throw new MemoryAccessFailure(address);
        }

        public byte ReadByte(ushort address)
        {
            return _memory[address];
        }
    }
}
