using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InYourInterest.InputModels.Tags
{
    public class TagsInfoViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
