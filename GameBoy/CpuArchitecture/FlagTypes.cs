using System;

namespace GameBoy.CpuArchitecture
{
    [Flags]
    public enum RegisterFlags : byte
    {
        None = 0,
        Z = 1 << 7,
        N = 1 << 6,
        H = 1 << 5,
        C = 1 << 4,
        All = Z | N | H | C
    }

    [Flags]
    public enum InterruptFlags: byte
    {
        None = 0,
        VBlank = (1 << 0),
        LCDStat = (1 << 1),
        Timer = (1 << 2),
        Serial = (1 << 3),
        Joypad = (1 << 4)
    }
}
