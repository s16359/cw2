using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2
{
    public class Student
    {
        [XmlElement(elementName: "fname")]
        public string Imie { get; set; }
        [XmlElement(elementName: "lname")]
        public string Nazwisko { get; set; }
        public string email { get; set; }
        [XmlElement(elementName: "indexNumber")]
        public string index { get; set; }
        public string birthDate { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies { get; set; }
    }
}
