namespace OctaLib
{
    internal class BankUtils
    {

        public static string ReadPartName(byte[] bankData, int addr)
        {
            char[] partName = new char[6];

            // Part names can be 6 chars
            Array.Copy(bankData, addr, partName, 0, 6);

            // But we need to ignore anything after 00 because it might be junk
            for (int i = 0; i < 6; i++)
            {
                if (partName[i] == 0)
                {
                    char[] temp = new char[i];
                    Array.Copy(partName, temp, i);
                    return new String(temp);
                }
            }

            return new String(partName);
        }

    }
}
