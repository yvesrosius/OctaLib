using OctaLib;

internal class Program
{
    private const int ARG_PATH = 0;
    private const int ARG_COMMAND = 1;
    private const int ARG_1 = 2;
    private const int ARG_2 = 3;

    private static void IntegrityCheck(string path)
    {
        if (!File.Exists(path + "\\project.strd") && !File.Exists(path + "\\project.work"))
        {
            Console.WriteLine("This does not appear to be a valid Octatrack project directory");
            return;
        }

        if (ProjectUtils.GetVersion(path) != 19)
        {
            Console.WriteLine("Unrecognized project version");
            return;
        }
    }

    private static void Info(string path)
    {
        string[] partnames = new string[4];

        for (int bankNumber = 1; bankNumber < 17; bankNumber++)
        {
            var bankFileName = bankNumber.ToString("00");
            if (File.Exists(path + $"\\bank{bankFileName}.strd"))
            {
                var bankData = File.ReadAllBytes(path + $"\\bank{bankFileName}.strd");
                AssembleInfo(bankData, bankFileName, bankNumber);
            }
            if (File.Exists(path + $"\\bank{bankFileName}.work"))
            {
                var bankData = File.ReadAllBytes(path + $"\\bank{bankFileName}.work");
                AssembleInfo(bankData, bankFileName, bankNumber);
            }
        }
    }

    private static void AssembleInfo(byte[] bankData, string bankFileName, int bankNumber)
    {
        if (!BankUtils.ValidateHeader(bankData))
        {
            Console.WriteLine($"Bank {bankNumber + 1} appears to be invalid");
            return;
        }

        string[] partNames = new string[4];
        for (int partIndex = 0; partIndex < 4; partIndex++)
        {
            partNames[partIndex] = BankUtils.ReadPartName(bankData, partIndex).PadRight(6);
        }

        Console.WriteLine($"bank{bankFileName} part names: {partNames[0]} {partNames[1]} {partNames[2]} {partNames[3]}");

        Console.Write("Pattern states: ");
        for (int patternIndex = 0; patternIndex < 16; patternIndex++)
        {
            Console.Write(BankUtils.ReadPatternActiveState(bankData, patternIndex) ? "1" : "0");
        }
        Console.WriteLine();
    }

    private static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: OctaLib <project folder> <command> <params>");
            Console.WriteLine("");
            Console.WriteLine("Commands:");
            Console.WriteLine("info         Print info about the project");
            Console.WriteLine("swap-banks   Swap banks");
            return;
        }

        IntegrityCheck(args[ARG_PATH]);

        switch (args[ARG_COMMAND].ToLower())
        {
            case "info":
                Info(args[ARG_PATH]);
                break;

            case "swap-banks":
                if (args.Length < 4)
                {
                    Console.WriteLine("swap-banks requires 2 additional arguments (bank # 1 and 2)");
                    return;
                }
                var b1 = Int32.Parse(args[ARG_1]);
                var b2 = Int32.Parse(args[ARG_2]);
                if (b1 < 1 || b1 > 16)
                {
                    Console.WriteLine("Invalid bank number (expected: 1-16)");
                    return;
                }
                if (b2 < 1 || b2 > 16)
                {
                    Console.WriteLine("Invalid bank number (expected: 1-16)");
                    return;
                }
                BankUtils.SwapBanks(args[ARG_PATH], b1, b2);
                break;
        }
    }
}