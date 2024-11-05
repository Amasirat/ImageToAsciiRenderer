namespace ImageToASCIIRenderer
{
    internal static class Parser
    {
        public static void Main(string[] args)
        {
            AsciiMachine machine = new AsciiMachine();
            
            machine.ConvertToAscii(Globals.ProjectDirectory + @"/assets/image.jpg");
        }
    }
}