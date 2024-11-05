using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageToASCIIRenderer;

public class AsciiMachine
{
    // Master Ctor
    public AsciiMachine(string outputPath, char[] chars, 
        int resize, bool doResize) : this(outputPath, chars)
    {
        DoResize = doResize;
        ResizeFactor = resize;
    }
    // outputPath and asciiChars Ctor
    public AsciiMachine(string outputPath, char[] chars)
    {
        this.outputPath = outputPath;
        asciichars = chars;
    }
    // No Arg Ctor
    public AsciiMachine()
    {
        outputPath = Globals.DefaultOutput;
        asciichars = Globals.DefaultASCIIChars.ToArray();
        ResizeFactor = Globals.ResizeFactor;
        DoResize = Globals.DefaultDoResize;
    }
    // Output Arg Ctor
    public AsciiMachine(string outPath)
    {
        outputPath = outPath;
    }
    
    public void ChangeAsciiChars(char[] chars)
    {
        if (chars.Length != asciichars.Length)
        {
            throw new 
                ArgumentException($"There must only be {asciichars.Length} ASCII chars");
        }
        
        for (int i = 0; i < chars.Length; i++)
        {
            asciichars[i] = chars[i];
        }
    }

    public void ConvertToAscii(string imagePath)
    {
        using Image<L8> image = Image.Load<L8>(imagePath);
        // Resize image if DoResize is true
        if(DoResize) image.Mutate(x => 
            x.Resize(image.Width / ResizeFactor, image.Height / ResizeFactor));
        
        // Find minimum and maximum luminance for image,
        // for normalizing luminance with respect to the image
        int minLuminance = 255;
        int maxLuminance = 0;
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<L8> row = accessor.GetRowSpan(y);
                for (int x = 0; x < accessor.Width; x++)
                {
                    int luminance = row[x].PackedValue;
                    if (luminance > maxLuminance) maxLuminance = luminance;
                    if (luminance < minLuminance) minLuminance = luminance;
                }
            }
        });
        StreamWriter sw = File.CreateText(outputPath + 
                                          Path.GetFileNameWithoutExtension(imagePath) + 
                                          ".ascii.txt");
        int range = (maxLuminance - minLuminance) + 1;
        // Build the string using the information of each pixel's luminance
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                Span<L8> row = accessor.GetRowSpan(y);
                for (int x = 0; x < accessor.Width; x++)
                {
                    int luminance = row[x].PackedValue;
                    int normalizedLuminance = 
                        (luminance - minLuminance) * (asciichars.Length - 1) / range;
                    sw.Write(asciichars[normalizedLuminance]);
                }
                // append new line in order to format the ascii output
                sw.Write('\n');
            }
        });
        sw.Close();
    }
    
    // Properties
    public int ResizeFactor
    {
        get
        {
            return resizeFactor;
        }
        set
        {
            if (value <= 0)
            {
                resizeFactor = 1;
            }
            else
            {
                resizeFactor = value;
            }
        }
    }
    
    public bool DoResize { get; set; }
    
    // fields
    private char[] asciichars;
    private string outputPath;
    private int resizeFactor;
}