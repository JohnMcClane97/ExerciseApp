using System;
using System.Collections.Generic;
using System.Text;

namespace StartApp.Models
{
    public class TabataExercise
    {
        public List<Tabata> TabataList { get; set; }

        public int Sets { get; set; }

        public string Name { get; set; }

        public int RestBetweenSets { get; set; }

        public int Cycles { get; set; }
    }
}
