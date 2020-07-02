using System;
using GameBoy.DeviceComponents;
using GameBoy.Main;

namespace GameBoy.Main
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

        public void Start()
        {
            Cpu.Initialize();
            // LoadRom();

        }
    }
}
