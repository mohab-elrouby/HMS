﻿
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DoctorPrescriptionResponce
    {
        public string DoctorFullName { get; set;}
        public string Department { get; set; }
        public List<Prescription>? Presciption { get; set;}
        public Prescription? Prescription { get; set;}
    }
}
