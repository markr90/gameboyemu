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
        /// <summary>
        /// Constructor for opcodes that only have on clockcycle count
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mnemonic"></param>
        /// <param name="clockCycles"></param>
        /// <param name="operandLength"></param>
        /// <param name="operation"></param>
        public OpCode(byte code, string mnemonic, int clockCycles, ushort operandLength, Operation operation)
            : this(code, mnemonic, clockCycles, clockCycles, operandLength, (cpu, i) => { operation(cpu, i); return clockCycles; })
        {
        }
        /// <summary>
        /// Constructor for OpCodes that have an alternative clockcycle count. Operation must return clock cycle number
        /// </summary>
        /// <param name="code"></param>
        /// <param name="mnemonic"></param>
        /// <param name="clockCycles"></param>
        /// <param name="clockCyclesAlt"></param>
        /// <param name="operandLength"></param>
        /// <param name="operation"></param>
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
            return string.Format("0x00{0:x2}: {1}", Code, Mnemonic);
        }
    }

    public class PrefixOpCode: OpCode
    {
        public const byte prefixCode = 0xCB;
        public PrefixOpCode(byte code, string mnemonic, int clockCycles, ushort operandLength, Operation operation)
            : base(code, mnemonic, clockCycles, operandLength, operation)
        {
        }
        public override string ToString()
        {
            return string.Format("0x{0:x2}{1:x2}: {2}", prefixCode, Code, Mnemonic);
        }
    }

    public class InvalidOpCode: OpCode
    {
        public InvalidOpCode(byte code)
            : base(code, "INVALID", 0, 0, (cpu, i) => throw new InvalidOperationException(string.Format("INVALID_OP: {0}", code)))
        {
        }
    }


}
