
using GameBoy.Architecture;

namespace GameBoyCPU.Architecture
{
    public interface Instruction
    {
        void Execute(Registers registers);
    }

    public class ADD: Instruction
    {
        private Register Target { get; }

        public ADD(Register target)
        {
            Target = target;
        }

        public void Execute(Registers registers)
        {
            byte value = registers.ReadByte(Target);
            int intermediate = registers.A + registers.ReadByte(Target);
            byte new_value = (byte)(intermediate & 0xFF);
            registers.A = (byte) new_value;
        }
    }
}
