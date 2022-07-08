using System;
using System.Linq;

namespace Persistance
{
    public enum PersistanceType
    {
        XML,
        JSON,
        BINARY,
        CSV,
        RSS
    }

    public enum XmlAttributeType
    {
        Attribute,
        Element,
        Ignore
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TargetPersistaneTypeAttribute : Attribute
    {
        public PersistanceType _format;
        public TargetPersistaneTypeAttribute(PersistanceType format)
        {
            _format = format;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TargetXmlTypeAttribute : Attribute
    {
        public XmlAttributeType _attributeType;
        public TargetXmlTypeAttribute(XmlAttributeType attributeType)
        {
            _attributeType = attributeType;
        }
    }

    internal class XMLPersister
    {
        public void WriteObject(object source)
        {
            //Property List (public)
            // How to transform each Property (xml attribute ,xml element)
            var properties = source.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                TargetXmlTypeAttribute attribute = properties[i].GetCustomAttributes(typeof(TargetXmlTypeAttribute), true).FirstOrDefault() as TargetXmlTypeAttribute;
                if (attribute != null)
                {
                    Console.WriteLine($"Attribute type is {attribute._attributeType}");
                }
            }
        }
    }

    public class Persister
    {
        public bool Persist(object source)
        {
            var targetTypeAttribute = source.GetType().GetCustomAttributes(typeof(TargetPersistaneTypeAttribute), true).FirstOrDefault() as TargetPersistaneTypeAttribute;
            PersistanceType _targetFormat = targetTypeAttribute._format;
            Console.WriteLine($"Target format is : {_targetFormat} ");
            switch (_targetFormat)
            {
                case PersistanceType.XML:
                    XMLPersister _persister = new XMLPersister();
                    _persister.WriteObject(source);
                    break;
            }
            return false;
        }
    }
}
