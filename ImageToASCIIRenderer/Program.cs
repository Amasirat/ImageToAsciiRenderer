namespace ImageToASCIIRenderer
{
    internal static class Parser
    {
        public enum State : byte
        {
            ParsingValid,
            ParsingInvalid
        }
        public static void Main(string[] args)
        {
            // AsciiMachine machine = new AsciiMachine();
            //
            // machine.ConvertToAscii(Globals.ProjectDirectory + @"/assets/image2.png");
            
            try
            {
                // Parse Arguments
                state = State.ParsingInvalid;
                ParseArguments(args);
                // If input is not given, the state will not allow the program to continue
                if (state == State.ParsingInvalid) throw new Exception("Invalid arguments: No input given");
                
                // Do the job
                AsciiMachine machine;
                if(outputpath != null)
                    machine = new AsciiMachine(outputpath);
                else
                {
                    machine = new AsciiMachine();
                }
                
                machine.ConvertToAscii(inputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ParseArguments(string[] arguments)
        {
            int argPosition = 0;
            while (argPosition < arguments.Length)
            {
                if (arguments[argPosition].StartsWith("--"))
                {
                    switch (arguments[argPosition])
                    {
                        case "--in":
                            if(arguments[argPosition + 1].StartsWith("--")) 
                                throw new Exception("Invalid input");
                            inputPath = arguments[argPosition + 1];
                            state = State.ParsingValid;
                            break;
                        case "--out":
                            if(arguments[argPosition + 1].StartsWith("--")) 
                                throw new Exception("Invalid output");
                            outputpath = arguments[argPosition + 1];
                            break;
                        case "--help":
                            break;
                    }
                }
                else
                {
                    throw new Exception($"Invalid argument: {arguments[0]}");
                }
                argPosition += 2;
            }
        }
        public static State state { get; set; }

        private static string? outputpath = null;
        private static string? inputPath = null;
    }
}