using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KANATypingAssistant
{
    class KeyboardSimulator
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern int VkKeyScan(char ch);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static Dictionary<char, byte> keyMap = new Dictionary<char, byte>()
        {
            {'A', 0x41},
            {'B', 0x42},
            {'C', 0x43},
            {'D', 0x44},
            {'E', 0x45},
            {'F', 0x46},
            {'G', 0x47},
            {'H', 0x48},
            {'I', 0x49},
            {'J', 0x4A},
            {'K', 0x4B},
            {'L', 0x4C},
            {'M', 0x4D},
            {'N', 0x4E},
            {'O', 0x4F},
            {'P', 0x50},
            {'Q', 0x51},
            {'R', 0x52},
            {'S', 0x53},
            {'T', 0x54},
            {'U', 0x55},
            {'V', 0x56},
            {'W', 0x57},
            {'X', 0x58},
            {'Y', 0x59},
            {'Z', 0x5A},
            { '1', 0x1E },
            { '2', 0x1F },
            { '3', 0x20 },
            { '4', 0x21 },
            { '5', 0x22 },
            { '6', 0x23 },
            { '7', 0x24 },
            { '8', 0x25 },
            { '9', 0x26 },
            { '0', 0x27 },
            { '-', 0xbd },
            { ',', 0xbc },
            { '.', 0xbe },
            { '?', 0xbf }
        };
        public static List<byte> waitList = new List<byte>()
        {
            0x32,
            0xbd,
            0xde,
            0x51,
            0x57,
            0x52,
            0x54,
            0x50,
            0x41,
            0x53,
            0x44,
            0x46,
            0x47,
            0x48,
            0xba,
            0x5a,
            0x58,
            0x43,
            0x56,
            0x42,
        };
        public static Dictionary<int, string> kanaMap = new Dictionary<int, string>()
        {
            { 0x0, "" },
            { 0xd, "" }, // enter key
            { 0x31, "NU" },
            { 0x32, "FU" },
            { 0x33, "A" },
            { 0x34, "U" },
            { 0x35, "E" },
            { 0x36, "O" },
            { 0x37, "YA" },
            { 0x38, "YU" },
            { 0x39, "YO" },
            { 0x30, "WA" },
            { 0xbd, "HO" },
            { 0xde, "HE" },
            { 0xdc, "-" },
            { 0x51, "TA" },
            { 0x57, "TE" },
            { 0x45, "I" },
            { 0x52, "SU" },
            { 0x54, "KA" },
            { 0x59, "XN" },
            { 0x55, "NA" },
            { 0x49, "NI" },
            { 0x4f, "RA" },
            { 0x50, "SE" },
            { 0x41, "CHI" },
            { 0x53, "TO" },
            { 0x44, "SHI" },
            { 0x46, "HA" },
            { 0x47, "KI" },
            { 0x48, "KU" },
            { 0x4a, "MA" },
            { 0x4b, "NO" },
            { 0x4c, "RI" },
            { 0xbb, "RE" },
            { 0xba, "KE" },
            { 0xdd, "MU" },
            { 0x5a, "TSU" },
            { 0x58, "SA" },
            { 0x43, "SO" },
            { 0x56, "HI" },
            { 0x42, "KO" },
            { 0x4e, "MI" },
            { 0x4d, "MO" },
            { 0xbc, "NE" },
            { 0xbe, "RU" },
            { 0xbf, "ME" },
            { 0xe2, "RO" },
            { 0xc0, "" },
            { 0xdb, "" }
        };
        public static Dictionary<int, string> shiftedKanaMap = new Dictionary<int, string>()
        {
            { 0x0, "" },
            { 0xd, "" }, //enter key
            { 0x31, "NU" },
            { 0x32, "FU" },
            { 0x33, "LA" },
            { 0x34, "LU" },
            { 0x35, "LE" },
            { 0x36, "LO" },
            { 0x37, "LYA" },
            { 0x38, "LYU" },
            { 0x39, "LYO" },
            { 0x30, "WO" },
            { 0xbd, "HO" },
            { 0xde, "HE" },
            { 0xdc, "-" },
            { 0x51, "TA" },
            { 0x57, "TE" },
            { 0x45, "LI" },
            { 0x52, "SU" },
            { 0x54, "KA" },
            { 0x59, "XN" },
            { 0x55, "NA" },
            { 0x49, "NI" },
            { 0x4f, "RA" },
            { 0x50, "SE" },
            { 0x41, "CHI" },
            { 0x53, "TO" },
            { 0x44, "SHI" },
            { 0x46, "HA" },
            { 0x47, "KI" },
            { 0x48, "KU" },
            { 0x4a, "MA" },
            { 0x4b, "NO" },
            { 0x4c, "RI" },
            { 0xbb, "RE" },
            { 0xba, "KE" },
            { 0xdd, "MU" },
            { 0x5a, "LTSU" },
            { 0x58, "SA" },
            { 0x43, "SO" },
            { 0x56, "HI" },
            { 0x42, "KO" },
            { 0x4e, "MI" },
            { 0x4d, "MO" },
            { 0xbc, "," },
            { 0xbe, "." },
            { 0xbf, "?" },
            { 0xe2, "RO" }
        };
        public static Dictionary<byte, string> dakutenKanaMap = new Dictionary<byte, string>()
        {
            { 0x32, "BU" },
            { 0xbd, "BO" },
            { 0xde, "BE" },
            { 0x51, "DA" },
            { 0x57, "DE" },
            { 0x52, "ZU" },
            { 0x54, "GA" },
            { 0x50, "ZE" },
            { 0x41, "DI" },
            { 0x53, "DO" },
            { 0x44, "JI" },
            { 0x46, "BA" },
            { 0x47, "GI" },
            { 0x48, "GU" },
            { 0xba, "GE" },
            { 0x5a, "DU" },
            { 0x58, "ZA" },
            { 0x43, "ZO" },
            { 0x56, "BI" },
            { 0x42, "GO" },
        };
        public static Dictionary<byte, string> handakutenKanaMap = new Dictionary<byte, string>()
        {
            { 0x32, "PU" },
            { 0xbd, "PO" },
            { 0xde, "PE" },
            { 0x46, "PA" },
            { 0x56, "PI" },
        };

        static string GetWindowTitle(IntPtr hwnd)
        {
            StringBuilder tsb = new StringBuilder(256);
            int retCode = GetWindowText(hwnd, tsb, tsb.Capacity);
            return tsb.ToString();
        }

        private static bool compareKey(Tuple<byte, bool> a, Tuple<byte, bool> b)
        {
            return a.Item1 == b.Item1 && a.Item2 == b.Item2;
        }

        private static int KEYEVENTF_KEYDOWN = 0x0;
        private static int KEYEVENTF_KEYUP = 0x2;

        private static Queue<Tuple<byte, bool>> keyQueue = new Queue<Tuple<byte, bool>>();
        private static Tuple<byte, bool> tsu = new Tuple<byte, bool>(0x5a, true);
        public static void simulateKey(byte key, bool shifted)
        {
            IntPtr hWnd = GetForegroundWindow();
            Debug.WriteLine(GetWindowTitle(hWnd));

            Tuple<byte, bool> keySet = new Tuple<byte, bool>(key, shifted);
            string stroke = "";
            if (!waitList.Contains(key))
            {
                if (keyQueue.Count == 0)
                {
                    stroke = shifted ? shiftedKanaMap[key] : kanaMap[key];
                }
                else if (keyQueue.Count == 1)
                {
                    Tuple<byte, bool> prevKeySet = keyQueue.Dequeue();
                    if (compareKey(prevKeySet, tsu))
                    {
                        stroke = shifted ? shiftedKanaMap[key] : kanaMap[key];
                        if (stroke.Length > 0) stroke = stroke[0] + stroke;
                    }
                    else if (dakutenKanaMap.ContainsKey(prevKeySet.Item1) || handakutenKanaMap.ContainsKey(prevKeySet.Item1))
                    {
                        if (key == 0xc0)
                        {
                            if (dakutenKanaMap.ContainsKey(prevKeySet.Item1))
                            {
                                stroke = dakutenKanaMap[prevKeySet.Item1];
                            }
                        }
                        else if (key == 0xdb)
                        {
                            if (handakutenKanaMap.ContainsKey(prevKeySet.Item1))
                            {
                                stroke = handakutenKanaMap[prevKeySet.Item1];
                            }
                        }
                        else
                        {
                            stroke = prevKeySet.Item2 ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                            stroke += shifted ? shiftedKanaMap[key] : kanaMap[key];
                        }
                    }
                    else
                    {
                        stroke = shifted ? shiftedKanaMap[key] : kanaMap[key];
                    }
                }
                else if (keyQueue.Count == 2)
                {
                    Tuple<byte, bool> prevprevKeySet = new Tuple<byte, bool>(0, false);
                    Tuple<byte, bool> prevKeySet = new Tuple<byte, bool>(0, false);
                    prevprevKeySet = keyQueue.Dequeue();
                    prevKeySet = keyQueue.Dequeue();
                    if (compareKey(prevprevKeySet, tsu))
                    {
                        if (dakutenKanaMap.ContainsKey(prevKeySet.Item1) || handakutenKanaMap.ContainsKey(prevKeySet.Item1))
                        {
                            if (key == 0xc0)
                            {
                                if (dakutenKanaMap.ContainsKey(prevKeySet.Item1))
                                {
                                    stroke = dakutenKanaMap[prevKeySet.Item1];
                                    stroke = stroke[0] + stroke;
                                }
                            }
                            else if (key == 0xdb)
                            {
                                if (handakutenKanaMap.ContainsKey(prevKeySet.Item1))
                                {
                                    stroke = handakutenKanaMap[prevKeySet.Item1];
                                    stroke = stroke[0] + stroke;
                                }
                            }
                            else
                            {
                                stroke = prevKeySet.Item2 ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                                stroke = stroke[0] + stroke;
                                stroke += shifted ? shiftedKanaMap[key] : kanaMap[key];
                            }
                        }
                    }
                }
            }
            else
            {
                if (compareKey(keySet, tsu))
                {
                    if (keyQueue.Count == 1)
                    {
                        Tuple<byte, bool> prevKeySet = keyQueue.Dequeue();
                        stroke = shifted ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                    }
                }
                else
                {
                    if (keyQueue.Count == 1)
                    {
                        Tuple<byte, bool> prevKeySet = keyQueue.Peek();
                        if (!compareKey(prevKeySet, tsu))
                        {
                            stroke = shifted ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                            keyQueue.Dequeue();
                        }
                    }
                    else if (keyQueue.Count == 2)
                    {
                        Tuple<byte, bool> prevprevKeySet = keyQueue.Dequeue();
                        Tuple<byte, bool> prevKeySet = keyQueue.Dequeue();
                        if (!compareKey(prevprevKeySet, tsu))
                        {
                            stroke = prevprevKeySet.Item2 ? shiftedKanaMap[prevprevKeySet.Item1] : kanaMap[prevprevKeySet.Item1];
                            stroke += prevKeySet.Item2 ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                        }
                        else
                        {
                            stroke += prevKeySet.Item2 ? shiftedKanaMap[prevKeySet.Item1] : kanaMap[prevKeySet.Item1];
                            stroke = stroke[0] + stroke;
                        }
                    }
                }
                keyQueue.Enqueue(keySet);
            }
            MainForm.unhook();
            foreach(char c in stroke)
            {
                byte vk = keyMap[c];
                keybd_event((byte)vk, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((byte)vk, 0, KEYEVENTF_KEYUP, 0);
            }
            if (key == 0xd)
            {
                keybd_event((byte)Keys.Enter, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((byte)Keys.Enter, 0, KEYEVENTF_KEYUP, 0);
            }
            MainForm.hook();
        }

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        
    }


}
