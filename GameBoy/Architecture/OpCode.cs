using GameBoy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Architecture
{
    public delegate void Operation(CPU cpu);
    public class OpCode
    {
        public readonly Operation Operation;
        public readonly int ClockCycles;
        public OpCode(int clockCycles, Operation operation)
        {
            Operation = operation;
            ClockCycles = clockCycles;
        }
    }
}
