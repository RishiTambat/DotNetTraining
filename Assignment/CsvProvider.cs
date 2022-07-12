using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;

namespace Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<dynamic> list = new List<dynamic>();
            //select all the patients where location="blr"
            System.IO.StreamReader _r = new System.IO.StreamReader("..//..//Patients.csv");
            try
            {
                string[] header = _r.ReadLine().Split(',');
                while (!_r.EndOfStream)
                {
                    string line = _r.ReadLine();
                    string[] lineContent = line.Split(',');

                    dynamic patient = new ElasticType();
                    for (int i = 0; i < header.Length; i++)
                    {
                        patient.TrySetMember(new MemberBinder(header[i]), lineContent[i]);
                    }
                    list.Add(patient);
                }
            }
            finally
            {
                _r.Close();
            }
            var result = list.Where((p) => p.Location == "blr");
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }
    }

    class ElasticType : DynamicObject
    {
        Dictionary<string, object> _stateBag = new Dictionary<string, object>();

        //set accessor
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _stateBag[binder.Name] = value;
            return true;
        }

        //get accessor
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;
            if (this._stateBag.ContainsKey(binder.Name))
            {
                result = this._stateBag[binder.Name];
                return true;
            }
            return false;
        }

    }

    public class MemberBinder : SetMemberBinder
    {
        public MemberBinder(string name) : base(name, true)
        {

        }

        public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
