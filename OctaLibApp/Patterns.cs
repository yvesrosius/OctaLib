using System;
using System.Collections.ObjectModel;

namespace OctaLib
{
    public class Pattern
    {
        public int Number {  get; set; }
        public int PartNumber {  get; set; }
        public string PartName { get; set; }

        public bool Status { get; set; }

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
                if (Number % 4 == 1)
                {
                    if (Status)
                    {
                        return "Assets/ot-button-illum-outline.svg";
                    }
                    else
                    {
                        return "Assets/ot-button-outline.svg";
                    }
                    
                }
                else
                {
                    if (Status)
                    {
                        return "Assets/ot-button-illum.svg";
                    }
                    else
                    {
                        return "Assets/ot-button.svg";
                    }
                    
                }
            }
        }
    }

    public class Patterns : ObservableCollection<Pattern>
    {
        public Patterns()
        {

            // This just populates some dummy patterns for the initial view
            for (int i = 1; i < 17; i++)
            {
                var p = new Pattern();
                p.Number = i;
                p.PartNumber = 1;
                p.PartName = "ONE";
                p.Status = false;
                Add(p);
            }
        }

    }
}
