﻿using Domain.Enums;
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
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }

        public float Price { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }
    }
}
