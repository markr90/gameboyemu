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
        public Operation Operation { get; }
        public OpCode(Operation operation)
        {
            Operation = operation;
        }
    }
}
