﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class BaseResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public bool Success { get; internal set; }

        public string Details { get; internal set; }
    }
}
