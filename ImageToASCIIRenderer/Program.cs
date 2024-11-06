namespace ImageToASCIIRenderer
{
    internal static class Parser
    {
        private enum State : byte
        {
            ParsingValid,
            ParsingInvalid
        }
        public static void Main(string[] args)
        {
            try
            {
                // Parse Arguments
                // Once input has been received, state changes to State.ParsingValid
                _state = State.ParsingInvalid;
                ParseArguments(args);
                // If input is not given, the state will not allow the program to continue
                if (_state == State.ParsingInvalid) throw new Exception("Invalid arguments: No input given");

                // Do the job
                // if null, give default value from Globals
                _outputPath ??= Globals.DefaultOutput;
                AsciiMachine machine = new AsciiMachine(_outputPath);

                if (_inputPath != null)
                {
                    string output = machine.ConvertToAscii(_inputPath);
                    Console.WriteLine($"Successfully converted to ASCII as {output}");
                }
                else
                {
                    throw new Exception("Input was not given! (Main thread)");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("An operation is missing its operand!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ParseArguments(string[] arguments)
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
                            _inputPath = arguments[argPosition + 1];
                            // input has to be given for the program to function
                            _state = State.ParsingValid;
                            break;
                        case "--out":
                            if(arguments[argPosition + 1].StartsWith("--")) 
                                throw new Exception("Invalid output");
                            _outputPath = arguments[argPosition + 1];
                            break;
                        case "--help":
                            break;
                        default:
                            throw new Exception("Invalid argument: " + arguments[argPosition]);
                    }
                }
                else
                {
                    throw new Exception($"Invalid argument: {arguments[argPosition]}");
                }
                // go to the next operation
                // each operation takes in only one argument, therefore this method
                // is the simplest
                argPosition += 2;
            }
        }
        // fields
        private static State _state;
        private static string? _outputPath;
        private static string? _inputPath;
    }
}