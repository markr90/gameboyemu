using GameBoy.CpuArchitecture;
using GameBoy.Device;
using System;

namespace GameBoy.CpuArchitecture
{
    public class CPU
    {
        private const double OfficalClockFrequency = 4194304;

        public Registers Registers { get; }
        public MemoryController MemController { get; }
        public Alu Alu { get; }
        private Disassembler disassembler;
        private Instruction _nextInstruction = new Instruction();

        public readonly double CyclesPerSecond = OfficalClockFrequency;
        private ulong _ticks;
        private byte[] operandBlock = new byte[4];

        public ushort SP;
        public ushort PC;

        public CPU(GameBoyDevice device)
        {
            Registers = new Registers();
            MemController = new MemoryController(device);
            Alu = new Alu(Registers);
            disassembler = new Disassembler(MemController);
        }

        public int Step()
        {
            int cycles = 0;
            disassembler.FetchInstruction(ref PC, ref _nextInstruction);
            cycles = _nextInstruction.Execute(this);
            return cycles;
        }

        public void PrintRegister()
        {
            Console.WriteLine(Registers.ToString());
        }

        public void PrintRegisterAsBits()
        {
            Console.WriteLine(Registers.ToBitString());
        }

        public void JumpRelative(sbyte r8)
        {
            ushort jumpTo = unchecked((ushort)(PC + r8));
            Jump(jumpTo);
        }

        public int JumpRelativeConditional(OpCode opCode, sbyte r8, bool conditionalCheck)
        {
            ushort jumpTo = unchecked((ushort)(PC + r8));
            return JumpConditional(opCode, jumpTo, conditionalCheck);
        }

        public void Jump(ushort address)
        {
            PC = address;
        }

        /// <summary>
        /// Jumps the PC to address (can provide optional flag checks)
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="address"></param>
        /// <param name="conditionalFlags">Optional parameter</param>
        /// <returns>Clock cycles of the operation</returns>
        public int JumpConditional(OpCode opCode, ushort address, bool conditionalCheck)
        {
            if (conditionalCheck)
            {
                Jump(address);
                return opCode.ClockCycles;
            }
            else
            {
                return opCode.ClockCyclesAlt;
            }
        }

        public void Push(ushort value)
        {
            // TODO is this correct? Should this not be SP += 2??
            SP -= 2;
            MemController.Write(SP, value);
        }

        public void Stop()
        { 
            // TODO IMPLEMENT 
        }

        public void Halt() 
        {
            // TODO IMPLEMENT 
        }
    }
}
