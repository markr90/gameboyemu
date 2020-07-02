

using GameBoy.Main;
using System;

namespace GameBoy.DeviceComponents
{
    /// <summary>
    ///  Delegates memory address requests to the correct component
    /// </summary>
    public class MemoryController
    {
        private byte[] _singleBuffer = new byte[1];
        private byte[] _doubleBuffer = new byte[2];
        private GameBoyDevice _device;        

        public MemoryController(GameBoyDevice device)
        {
            _device = device;
        }

        public byte Read(ushort address)
        {
            return _device.InternalMemory.ReadByte(address);
            // TODO should be a switch statement for performance

            //if (address < 0x8000)
            //    // Should read from cartridge
            //    throw new NotImplementedException("Cartridge not implemented yet");
            //else if (address < 0xA000)
            //    // Read from gpu
            //    throw new NotImplementedException("GPU not implemented yet");
            //else if (address < 0xC000)
            //    // read from cartridge (switchable bank)
            //    throw new NotImplementedException("Cartridge not implemented yet");
            //else if (address < 0xD000)
            //    // read from internal ram 4kb
            //    throw new NotImplementedException("Internal ram not implemented yet");
            //else if (address < 0xE000)
            //    // read from switchable ram (1 bank for GB mode, 7 for gbc)
            //    throw new NotImplementedException("Switchable ram not implemented yet");
            //else if (address < 0xFE00)
            //    // read from echo ram
            //    throw new NotImplementedException("Echo ram no t implemented yet");
            //else if (address < 0xFEA0)
            //    // sprite attribute table
            //    throw new NotImplementedException("Sprite attribute table not implemented yet");
            //else if (address < 0xFF00)
            //    // Not usuable
            //    throw new NotImplementedException("Unusable ram");
            //else if (address < 0xFF80)
            //    // I/O registers
            //    throw new NotImplementedException("IO Registers not implemented yet");
            //else if (address < 0xFFFF)
            //    // High ram
            //    throw new NotImplementedException("High ram not implemented yet");
            //else if (address == 0xFFFF)
            //    // interrupts enable register
            //    throw new NotImplementedException("Interrupts enable register not implemented yet");
            //else
            //    throw new MemoryAccessFailure(address);
        }
        public byte[] Read(ushort address, int count)
        {
            byte[] result;

            switch (count)
            {
                case 1:
                    result = _singleBuffer;
                    break;
                case 2:
                    result = _doubleBuffer;
                    break;
                default:
                    result = new byte[count];
                    break;
            }

            for (int i = 0; i < count; i++)
                result[i] = Read((ushort)(address + i));

            return result;
        }

        public ushort ReadUshort(ushort address)
        {
            return BitConverter.ToUInt16(Read(address, 2), 0);
        }

        public void Write(ushort address, ushort value)
        {
            Write(address, BitConverter.GetBytes(value));
        }

        public void Write(ushort address, byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                Write((ushort) (address + i), bytes[i]);
            }
        }

        public void Write(ushort address, byte b)
        {
            _device.InternalMemory.WriteByte(address, b);
        }
    }
}
