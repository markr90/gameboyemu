using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Device
{
    public interface IMemoryBus
    {
        byte ReadByte(ushort address);
        void WriteByte(ushort address, byte value);
    }
}
