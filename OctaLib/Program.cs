using OctaLib;
using System.Runtime.CompilerServices;



if (args.Length == 0)
{
    Console.WriteLine("Usage: OctaLib <project folder>");
    return;
}

string[] partnames = new string[4];



for (int i = 1; i< 17; i++)
{
    var bankNumStr = i.ToString("00");
    var b = File.ReadAllBytes(args[0] + $"\\bank{bankNumStr}.strd");
    //Console.WriteLine($"bank{bankNumStr} has {b.Length} bytes");

    string[] partNames = new string[4];
    partNames[0] = BankUtils.ReadPartName(b, Constants.ADDR_PART_1_NAME).PadRight(6);
    partNames[1] = BankUtils.ReadPartName(b, Constants.ADDR_PART_2_NAME).PadRight(6);
    partNames[2] = BankUtils.ReadPartName(b, Constants.ADDR_PART_3_NAME).PadRight(6);
    partNames[3] = BankUtils.ReadPartName(b, Constants.ADDR_PART_4_NAME).PadRight(6);

    Console.WriteLine($"bank{bankNumStr} part names: {partNames[0]} {partNames[1]} {partNames[2]} {partNames[3]}");
}


