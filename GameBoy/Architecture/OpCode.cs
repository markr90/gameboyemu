using GameBoy.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBoy.Architecture
{
    public delegate void OperationDef(CPU cpu);
    public delegate int Operation(CPU cpu);
    public class OpCode
    {
        public readonly Operation Operation;
        private readonly int ClockCycles;

        public OpCode(int clockCycles, OperationDef operation)
            : this(clockCycles, cpu => { operation(cpu); return clockCycles; })
        {
        }
        private OpCode(int clockCycles, Operation operation)
        {
            Operation = operation;
            ClockCycles = clockCycles;
        }
    }
}
