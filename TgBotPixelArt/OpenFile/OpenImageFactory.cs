using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.OpenFile
{
    public class OpenImageFactory : OpenFileFactory
    {
        public override IOpenFile CreateOpenFile()
        {
            return new OpenImage();
        }
    }
}
