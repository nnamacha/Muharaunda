﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Models
{
    public class Audit
    {
        public int UpdatedBy { get; set; }
        public DateTime Updated { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime Approved { get; set; }
    }
}
