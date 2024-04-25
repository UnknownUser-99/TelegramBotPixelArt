using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.OpenFile
{
    public abstract class OpenFileFactory
    {
        public abstract IOpenFile CreateOpenFile();
    }
}
