namespace OctaLib
{
    internal class Constants
    {
        public const int ADDR_PART_1_NAME = 0x0009B4B3;
        public const int ADDR_PART_2_NAME = 0x0009B4BA;
        public const int ADDR_PART_3_NAME = 0x0009B4C1;
        public const int ADDR_PART_4_NAME = 0x0009B4C8;

        /*
         * Pattern 1 track addresses: use these to find the start of tracks relative to other pattern headers 
        TRAC01 0x0000001E
        TRAC02 0x00000940
        TRAC03 0x00001262
        TRAC04 0x00001B84
        TRAC05 0x000024A6
        TRAC06 0x00002DC8
        TRAC07 0x000036EA
        TRAC08 0x0000400C

        Pattern 1 MIDI track addresses: use these to find the start of tracks relative to other pattern headers 
        MTRA01 0x0000492E
        MTRA02 0x000051E7
        MTRA03 0x00005AA0
        MTRA04 0x00006359
        MTRA05 0x00006C12
        MTRA06 0x000074CB
        MTRA07 0x00007D84
        MTRA08 0x0000863D

        Pattern header addresses
        PTRN01 0x00000016
        PTRN02 0x00008F02
        PTRN03 0x00011DEE
        PTRN04 0x0001ACDA
        PTRN05 0x00023BC6
        PTRN06 0x0002CAB2
        PTRN07 0x0003599E
        PTRN08 0x0003E88A
        PTRN09 0x00047776
        PTRN10 0x00050662
        PTRN11 0x0005954E
        PTRN12 0x0006243A
        PTRN13 0x0006B326
        PTRN14 0x00074212
        PTRN15 0x0007D0FE
        PTRN16 0x00085FEA

        There seem to be two sets of parts. Why?
        PART01 0x0008EED6
        PART02 0x00090791
        PART03 0x0009204C
        PART04 0x00093907
        PART05 0x000951C2
        PART06 0x00096A7D
        PART07 0x00098338
        PART08 0x00099BF3
        **/
    }
}
