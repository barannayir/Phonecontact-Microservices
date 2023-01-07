
using System;
using System.Collections.Generic;

namespace ContactMicroService.Entities
{
    public class ContactDto
    {
   
        public string uuid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }

    }

}
