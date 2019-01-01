using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class LoggerText
    {
        public LoggerText()
        {
            Text = string.Empty;
        }

        public string Text { get; set; }

        public void AppendLine(string textToAppend)
        {
            this.Text += textToAppend + Environment.NewLine;
        }
    }
}
