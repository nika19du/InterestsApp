using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InYourInterest.Services.Reactions
{
    public class ReactionsCountServiceModel
    { 
        public int Likes { get; set; }

        public int Loves { get; set; }

        public int WowCount { get; set; }

        public int HahaCount { get; set; }

        public int SadCount { get; set; }

        public int AngryCount { get; set; }
    }
}
