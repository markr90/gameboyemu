using GameBoy.CpuArchitecture;
using System.Runtime.CompilerServices;
using System;

namespace GameBoy.CpuArchitecture
{
    public delegate void Operation(CPU cpu, Instruction instruction);
    public class OpCode
    {
        private readonly Operation Operation;
        private readonly int ClockCycles;
        public readonly int OperandLength;
        public readonly byte Code;

        public OpCode(byte code, int clockCycles, int operandLength, Operation operation)
        {
            Code = code;
            Operation = operation;
            OperandLength = operandLength;
            ClockCycles = clockCycles;
        }

        public int Perform(CPU cpu, Instruction instruction)
        {
            Operation(cpu, instruction);
            return ClockCycles;
        }

        public override string ToString()
        {
            return String.Format("{0:x2}", Code);
        }
    }

    public class PrefixedOpCode: OpCode
    {
        public const byte prefixCode = 0xCB;
        public PrefixedOpCode(byte prefixedcode, int clockCycles, int operandLength, Operation operation)
            : base(prefixedcode, clockCycles, operandLength, operation)
        { }
    }
}
