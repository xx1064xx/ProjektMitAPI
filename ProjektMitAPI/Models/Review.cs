using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace ProjektMitAPI.Models
{
    public class Review
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public long MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
