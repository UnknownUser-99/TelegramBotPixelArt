using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBotPixelArt.Telegram
{
    public class SizeHandler
    {
        public int SizeInput(string size)
        {
            StringBuilder sb = new StringBuilder();

            if (size != null)
            {
                foreach (char c in size)
                {
                    if (char.IsDigit(c))
                    {
                        sb.Append(c);
                    }
                }

                if (int.TryParse(sb.ToString(), out int result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
