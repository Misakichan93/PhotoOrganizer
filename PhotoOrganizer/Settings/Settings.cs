using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PhotoOrganizer.Settings
{
    [XmlRoot(nameof(Settings))]
    public class Settings
    {
        [XmlAttribute(nameof(SourceFolder))]
        public string SourceFolder { get; set; }
        [XmlAttribute(nameof(DestinationFolder))]
        public string DestinationFolder { get; set; }
    }
}
