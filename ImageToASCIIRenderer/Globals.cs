namespace ImageToASCIIRenderer;

public static class Globals
{
    public static string ProjectDirectory = 
        Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    
    public static string DefaultOutput = 
        ProjectDirectory + @"\Out\";

    public static char[] DefaultASCIIChars = 
        { ' ', '.', ':', '-', '=', '+', '*', '#', '@', '$' };
    
    public static int ResizeFactor = 2;

    public static bool DefaultDoResize = true;
}