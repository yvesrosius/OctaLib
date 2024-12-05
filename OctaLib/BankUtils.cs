using System.IO;

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

        public static int ReadTrackNum(byte[] bankData, int trackNum)
        {
            return bankData[Constants.ADDR_PAT01 + Constants.LENGTH_PATTERN_HEADER + (Constants.LENGTH_TRAC * trackNum) + Constants.OFFSET_TRACK_NUM];
        }

        public static int ReadTrackState(byte[] bankData, int patNum, int trackNum)
        {
            return bankData[
                Constants.ADDR_PAT01 + // Address of pattern 1
                (Constants.LENGTH_PATTERN_LENGTH * patNum) + // Offset by length of a pattern block * pattern number
                Constants.LENGTH_PATTERN_HEADER + (Constants.LENGTH_TRAC * trackNum) + Constants.OFFSET_TRACK_USAGE];
        }

        public static int ReadMTrackState(byte[] bankData, int patNum, int trackNum)
        {
            return bankData[
                Constants.ADDR_PAT01 + // Address of pattern 1
                (Constants.LENGTH_PATTERN_LENGTH * patNum) + // Offset by length of a pattern block * pattern number
                (Constants.LENGTH_TRAC * 8) + // Skip the tracks to get to the MIDI tracks
                Constants.LENGTH_PATTERN_HEADER + (Constants.LENGTH_MTRA * trackNum) + Constants.OFFSET_TRACK_USAGE];
        }


        /// <summary>
        ///  Iterate through tracks until we find something that seems active (not empty)
        /// </summary>
        /// <param name="bankData">Bank byte data</param>
        /// <param name="patNum">Pattern number (base 0)</param>
        /// <returns>True if not empty</returns>
        public static bool ReadPatternActiveState(byte[] bankData, int patNum)
        {
            // Iterate through tracks
            for (int t = 0; t < 8; t++)
            {
                if (BankUtils.ReadTrackState(bankData, patNum, t) > 0)
                {
                    return true;
                }
            }

            // Iterate through MIDI tracks
            for (int t = 0; t < 8; t++)
            {
                if (BankUtils.ReadMTrackState(bankData, patNum, t) > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
