using Core.Entities;
using Microsoft.AspNetCore.Razor.Language;
using System.Collections.Generic;

namespace Web.View_Models
{
    public class HomeViewModel
    {
        public Owner Owner { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }
    }
}
