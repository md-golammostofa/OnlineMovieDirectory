using MovieDirectory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieDirectory.ViewModel
{
    public class MovieIndexVM
    {
        public Genre SelectedGenre { get; set; }
        public List<Genre> Genres { get; set; }
        
    }
    
}