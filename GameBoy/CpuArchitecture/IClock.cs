using System;

namespace GameBoy.CpuArchitecture
{
    public interface IClock
    {
        event EventHandler Tick;
        void Start();
        void Stop();
    }
}
