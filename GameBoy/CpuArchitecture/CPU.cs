﻿using GameBoy.CpuArchitecture;
using GameBoy.Device;
using System;
using System.Runtime.InteropServices;

namespace GameBoy.CpuArchitecture
{
    public class CPU
    {
        // TODO implement halt mechanism
        // TODO implement shutdown
        // TODO implement stop


        private const double OfficalClockFrequency = 4194304;

        public Registers Registers { get; }
        public MemoryController MemController { get; }
        public Alu Alu { get; }
        private Instruction _nextInstruction = new Instruction();

        public readonly double CyclesPerSecond = OfficalClockFrequency;
        public readonly int CyclesPerFrame = 70224; // Official gameboy had 70224 cycles per frame at 59.7275 fps
        private ulong _ticks;

        public ushort SP;
        public ushort PC;
        private byte[] operandBuffer = new byte[2];

        private InterruptFlags IE;
        private InterruptFlags IF;
        private bool IME;

        public CPU(GameBoyDevice device)
        {
            Registers = new Registers();
            MemController = new MemoryController(device);
            Alu = new Alu(Registers);
        }

        public int Step()
        {
            int cycles = 0;
            FetchInstruction();
            cycles = _nextInstruction.Execute(this);
            return cycles;
        }

        private void FetchInstruction()
        {
            byte code = MemController.Read(PC++);
            Console.WriteLine("Trying to read code: {0:x2}", code);

            OpCode opcode = code == OpCodes.ExtendedTableOpCode
                ? OpCodes.PrefixedOpCodes[PC++]
                : OpCodes.SingleByteOpCodes[code];

            for (ushort i = 0; i < opcode.OperandLength; i++)
                operandBuffer[i] = MemController.Read(PC++);

            _nextInstruction.Set(opcode, operandBuffer);
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

        public void Jump(ushort address)
        {
            PC = address;
        }

        public void Push(ushort value)
        {
            SP -= 2;
            MemController.Write(SP, value);
        }

        public ushort Pop()
        {
            ushort result = MemController.ReadUshort(SP);
            SP += 2;
            return result;
        }

        public void Ret()
        {
            PC = Pop();
        }

        public int RetConditional(OpCode opcode, bool conditionalCheck)
        {
            if (conditionalCheck)
            {
                Ret();
                return opcode.ClockCycles;
            }
            return opcode.ClockCyclesAlt;
        }

        public void Call(ushort address)
        {
            Push(PC);
            PC = address;
        }

        public int CallConditional(OpCode opcode, ushort address, bool conditionalFlag)
        {
            if (conditionalFlag)
            {
                Call(address);
                return opcode.ClockCycles;
            }
            return opcode.ClockCyclesAlt;
        }

        public void EnableInterrupts()
        {
            IME = true;
        }
        public void DisableInterrupts()
        {
            IME = false;
        }

        public void Stop()
        { 
        }

        public void Halt() 
        {
        }
    }
}
