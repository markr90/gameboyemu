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
        private Instruction _nextInstruction;

        public readonly double CyclesPerSecond = OfficalClockFrequency;
        private ulong _ticks;
        private byte[] operands = new byte[3];

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

            try
            {
                _nextInstruction = disassembler.ReadInstruction(ref Registers.PC);
                cycles = _nextInstruction.Execute(this);
            }
            catch
            {
                Console.WriteLine("Instruction not found!");
            }

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
            ushort jumpTo = unchecked((ushort)(Registers.PC + r8));
            Jump(jumpTo);
        }

        public int JumpRelativeConditional(OpCode opCode, sbyte r8, bool conditionalCheck)
        {
            ushort jumpTo = unchecked((ushort)(Registers.PC + r8));
            return JumpConditional(opCode, jumpTo, conditionalCheck);
        }

        public void Jump(ushort address)
        {
            Registers.PC = address;
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
            Registers.SP -= 2;
            MemController.Write(Registers.SP, value);
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
