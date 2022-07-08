using System;
using Persistance;

namespace PersistanceClient
{
    [TargetPersistaneType(PersistanceType.XML)]
    public class PatientDataModel
    {
        [TargetXmlType(XmlAttributeType.Attribute)]
        public string MRN { get; set; }

        [TargetXmlType(XmlAttributeType.Element)]
        public string Name { get; set; }

        [TargetXmlType(XmlAttributeType.Ignore)]
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PatientDataModel model = new PatientDataModel() { MRN = "M100", Name = "Tom", Age = 33 };
            Persister _persister = new Persister();
            _persister.Persist(model);
        }
    }
}
