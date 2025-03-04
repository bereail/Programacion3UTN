using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.BookDTOs
{
    public class BookGetDTO
    {
        public string Title { get; set; }
        public BookGenre Genre { get; set; }
        public string Author { get; set; }

        public float Price { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }
    }
}
