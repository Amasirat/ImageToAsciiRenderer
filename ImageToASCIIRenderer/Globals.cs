namespace ImageToASCIIRenderer;

public static class Globals
{
    public static readonly string DefaultOutput = 
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"/AsciiOut/";

    public static readonly char[] DefaultASCIIChars = 
        { ' ', '.', ':', '-', '=', '+', '*', '#', '@', '$' };
    
    public static readonly int ResizeFactor = 2;

    public static readonly bool DefaultDoResize = true;
}