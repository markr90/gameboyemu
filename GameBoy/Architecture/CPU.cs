using GameBoy.Architecture;

namespace GameBoyCPU.Architecture
{
    public class CPU
    {
        private Registers registers = new Registers();

        public void Execute(Instruction instruction)
        {
            instruction.Execute(registers);
        }
    }
}
