using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace KANATypingAssistant
{
    public partial class MainForm : Form
    {

        private static MainForm instance;
    
        private MainForm()
        {
            InitializeComponent();
        }
        public static MainForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainForm();
                }
                return instance;
            }
        }

        private static bool enabled = true;

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.Checked)
            {
                if (radioButton.Text == "ON")
                {
                    enabled = true;
                }
                else if (radioButton.Text == "OFF")
                {
                    enabled = false;
                }
            }
        }

        public void enable()
        {
            enabled = true;
            onButton.Checked = true;
            offButton.Checked = false;
        }

        public void disable()
        {
            enabled = false;
            onButton.Checked = false;
            offButton.Checked = true;
        }

        public void toggle()
        {
            if (enabled)
            {
                disable();
            }
            else
            {
                enable();
            }
        }

        private const int WH_KEYBOARD_LL = 13;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public byte vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public static IntPtr _hookID = IntPtr.Zero;

        private void MainForm_Load(object sender, EventArgs e)
        {
            hook();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            unhook();
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void hook()
        {
            Debug.WriteLine("Hook");
            _hookID = SetHook(HookCallback);
        }

        public static void unhook()
        {
            Debug.WriteLine("UnHook");
            UnhookWindowsHookEx(_hookID);
        }

        private static int counter = 0;
        private static bool shifted = false;

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            KBDLLHOOKSTRUCT keyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if (nCode >= 0 && (wParam == 256 || wParam == 257))
            {
                if (keyInfo.vkCode == 0xF3)
                {
                    MainForm.Instance.toggle();
                    Debug.WriteLine("Enabled: " + enabled);
                    return (IntPtr)1;
                }
                else if (keyInfo.vkCode == 0xF4)
                {
                    return (IntPtr)1;
                }
            }
            if (enabled && nCode >= 0 && (wParam == 256 || wParam == 257))
            {
                Debug.WriteLine(keyInfo.vkCode.ToString("X"));
                if (KeyboardSimulator.kanaMap.ContainsKey(keyInfo.vkCode))
                {
                    if (wParam == 256)
                    {
                        KeyboardSimulator.simulateKey(keyInfo.vkCode, shifted);
                    }
                    return (IntPtr)1;
                }
                else if (keyInfo.vkCode == 0xA0 || keyInfo.vkCode == 0xA1)
                {
                    if (wParam == 256)
                    {
                        shifted = true;
                    }
                    else
                    {
                        shifted = false;
                    }
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private void clearTextButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }
    }
}