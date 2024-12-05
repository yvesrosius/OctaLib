using OctaLib;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: OctaLib <project folder>");
            return;
        }

        string[] partnames = new string[4];

        for (int i = 1; i < 17; i++)
        {
            var bankNumStr = i.ToString("00");
            var b = File.ReadAllBytes(args[0] + $"\\bank{bankNumStr}.strd");
            //Console.WriteLine($"bank{bankNumStr} has {b.Length} bytes");

            string[] partNames = new string[4];
            for (int p = 0; p < 4; p++)
            {
                partNames[p] = BankUtils.ReadPartName(b, Constants.ADDR_PART_NAME[p]).PadRight(6);
            }

            Console.WriteLine($"bank{bankNumStr} part names: {partNames[0]} {partNames[1]} {partNames[2]} {partNames[3]}");

            Console.Write("Pattern states: ");
            for (int pat = 0; pat < 16; pat++)
            {
                Console.Write(BankUtils.ReadPatternActiveState(b, pat) ? "1" : "0");
            }
            Console.WriteLine();

        }
    }
}