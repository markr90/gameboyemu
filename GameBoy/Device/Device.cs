using System;
using GameBoy.CpuArchitecture;

namespace GameBoy.Device
{
    public class GameBoyDevice
    {
        // Components
        public CPU Cpu { get; }
        //public GPU Gpu { get; }
        //public Cartridge Cartridge { get; }
        public IMemoryBus InternalMemory { get; }

        public GameBoyDevice()
        {
            Cpu = new CPU(this);
            InternalMemory = new Memory(0x10000); // 64 KB
        }
    }
}
