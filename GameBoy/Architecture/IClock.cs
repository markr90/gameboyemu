using System;

namespace GameBoy.Architecture
{
    public interface IClock
    {
        event EventHandler Tick;
        void Start();
        void Stop();
    }
}
