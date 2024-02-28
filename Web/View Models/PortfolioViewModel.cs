using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace Web.View_Models
{
    public class PortfolioViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descrption { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
    }
}
