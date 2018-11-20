using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Emgu.CV;
using Emgu.CV.Ocl;

namespace ShapeDetector
{
    public struct Video_Device
    {
        public string Device_Name;
        public int Device_ID;
        public Guid Identifier;

        public Video_Device(int Id, string Name, Guid Identity = new Guid())
        {
            Device_ID = Id;
            Device_Name = Name;
            Identifier = Identity;
        }
        
        public override string ToString()
        {
            return "["+Device_ID+"] "+Device_Name+": "+Identifier;
        }
    }
    

}
