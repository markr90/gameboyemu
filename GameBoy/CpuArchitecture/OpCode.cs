using GameBoy.CpuArchitecture;
using System.Runtime.CompilerServices;
using System;

namespace GameBoy.CpuArchitecture
{
    public delegate void Operation(CPU cpu, Instruction instruction);
    public delegate int OperationAlt(CPU cpu, Instruction instruction);
    public class OpCode
    {
        private readonly OperationAlt Operation;
        public readonly int ClockCycles;
        public readonly int ClockCyclesAlt;
        public readonly ushort OperandLength;
        public readonly byte Code;
        public readonly string Mnemonic;

        public OpCode(byte code, string mnemonic, int clockCycles, ushort operandLength, Operation operation)
            : this(code, mnemonic, clockCycles, clockCycles, operandLength, (cpu, i) => { operation(cpu, i); return clockCycles; })
        {
        }

        public OpCode(byte code, string mnemonic, int clockCycles, int clockCyclesAlt, ushort operandLength, OperationAlt operation)
        {
            Code = code;
            Mnemonic = mnemonic;
            ClockCycles = clockCycles;
            ClockCyclesAlt = clockCyclesAlt;
            OperandLength = operandLength;
            Operation = operation;
        }

        public int Perform(CPU cpu, Instruction instruction)
        {
            return Operation(cpu, instruction);
        }

        public override string ToString()
        {
            return string.Format("0x{0:x2}: {1}", Code, Mnemonic);
        }
    }

    public class PrefixedOpCode: OpCode
    {
        public const byte prefixCode = 0xCB;
        public PrefixedOpCode(byte prefixedCode, string mnemonic, int clockCycles, ushort operandLength, Operation operation)
            : base(prefixedCode, mnemonic, clockCycles, operandLength, operation)
        { }
    }


}
