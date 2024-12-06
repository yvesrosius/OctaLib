using System;
using System.Collections.ObjectModel;

namespace OctaLib
{
    public class Pattern
    {
        public int Number {  get; set; }
        public int PartNumber {  get; set; }
        public string PartName { get; set; }

        public string CompositePart
        {
            get
            {
                return $"{PartNumber}: {PartName}";
            }
        }

        // FIXME: Not very clean
        public string ImageSrc
        {
            get
            {
                if (Number % 4 == 0)
                {
                    return "Assets/ot-button-outline.svg";
                }
                else
                {
                    return "Assets/ot-button.svg";
                }
            }
        }

        public Pattern(int number, int partNumber, string partName)
        {
            this.Number = number;
            this.PartNumber = partNumber;
            this.PartName = partName;
        }
    }

    public class Patterns : ObservableCollection<Pattern>
    {
        public Patterns()
        {
            for (int i = 1; i < 17; i++)
            {
                Add(new Pattern(i, 1, "ONE"));
            }
        }

    }
}
