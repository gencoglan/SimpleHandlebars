using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasicHandleBars.Segment
{
    public static class SegmentFactory
    {
        public static object _LockObject = new object();
        private static SortedDictionary<string, Type> _AllSegments = null;
        private static SortedDictionary<string, Type> AllSegments {
            get {
                if (_AllSegments != null) {
                    return _AllSegments;
                }
                lock (_LockObject) {
                    if (_AllSegments != null)
                    {
                        return _AllSegments;
                    }
                    var tempInstance = new SortedDictionary<string, Type>();
                    string containerPath = "";
                    if (System.Web.HttpContext.Current != null)
                    {
                        containerPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"bin");
                    }
                    else {
                        containerPath = AppDomain.CurrentDomain.BaseDirectory;
                    }
                    var files = System.IO.Directory.GetFiles(containerPath);
                    for (int i = 0; i < files.Length; i++) {
                        try {
                            if (files[i].EndsWith(".dll") || files[i].EndsWith(".exe"))
                            {
                                var loadedAssembly = Assembly.UnsafeLoadFrom(files[i]);
                                var instances = from type in loadedAssembly.GetTypes()
                                                where type.IsClass && type.GetInterfaces().Contains(typeof(ISegment)) && type.GetConstructor(Type.EmptyTypes) != null
                                                select Activator.CreateInstance(type);
                                foreach (ISegment instance in instances)
                                    tempInstance[instance.GetType().Name] = instance.GetType();
                            }
                        }
                        catch
                        {

                        }
                    }
                    _AllSegments = tempInstance;
                }
                return _AllSegments;
            }
        }
        public static ISegment getSegment(string name) {
            if (name == null)
                return null;
            Type segmentType = AllSegments[name];
            if (segmentType == null) {
                throw new ArgumentNullException("Segment Not Found" + name);
            }
            ISegment segment = Activator.CreateInstance(segmentType) as ISegment;
            if (segment == null) {
                throw new ArgumentNullException("Segment Not Found On Type" + name);
            }
            return segment;
        }
    }
}
