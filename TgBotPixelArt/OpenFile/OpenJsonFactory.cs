using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.OpenFile
{
    public class OpenJsonFactory : OpenFileFactory
    {
        public override IOpenFile CreateOpenFile()
        {
            return new OpenJson();
        }
    }
}
