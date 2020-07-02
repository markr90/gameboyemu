
using GameBoy.CpuArchitecture;
using GameBoy.DeviceComponents;
using System;

namespace GameBoy.CpuArchitecture
{
    public delegate void Operation(CPU cpu);
    public delegate bool GetConditionalFlag(CPU cpu);

    public class OpCode
    {
        public readonly byte Code;
        public readonly string Mnemonic;
        private readonly int ClockCycles1;
        private readonly int ClockCycles2;
        private readonly GetConditionalFlag ConditionalFlag;
        private readonly Operation Operation;

        public OpCode(byte code, string mnemonic, int clockCycles1, int clockCycles2, GetConditionalFlag conditionalFlag, Operation operation)
        {
            Code = code;
            Mnemonic = mnemonic;
            ClockCycles1 = clockCycles1;
            ClockCycles2 = clockCycles2;
            Operation = operation;
            ConditionalFlag = conditionalFlag;
        }

        public OpCode(byte code, string mnemonic, int clockCycles, Operation operation)
            :this(code, mnemonic, clockCycles, clockCycles, cpu => true, operation)
        {

        }

        public int Execute(CPU cpu)
        {
            if (ConditionalFlag(cpu))
            {
                Operation(cpu);
                return ClockCycles1;
            }
            else
            {
                return ClockCycles2;
            }
        }
    }

    public class PrefixOpCode: OpCode
    {
        public const byte prefixCode = 0xCB;
        public PrefixOpCode(byte code, string mnemonic, int clockCycles, Operation operation)
            : base(code, mnemonic, clockCycles, operation)
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
            : base(code, "INVALID", 0, cpu => throw new InvalidOperationException(string.Format("INVALID_OP: {0}", code)))
        {
        }
    }


}
