using System;
using System.Runtime.InteropServices;
//using System.EnterpriseServices;//引用COM+的类

namespace MoecraftFramework
{
    [Guid("EE4D4D3C-666C-48B7-AED5-0EE29246937A")]
    public interface IMoecraft
    {
        string main(string cqpath, string fromQQ, string info);
    }
    [Guid("E5C89BCD-2F8D-4BF3-AF0A-234EBC47F3DE")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Entry : IMoecraft
    {
        public string text;
        public string main(string cqpath, string fromQQ, string info)
        {
            return text;
        }
    }
}
