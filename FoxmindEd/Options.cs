using CommandLine;

namespace FoxmindEd
{
    public class Options
    {
        [Option('f', "filePath", Required = true, HelpText = "File path.")]
        public string FilePath { get; set; }
    }
}
