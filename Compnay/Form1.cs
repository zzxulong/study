using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Compnay
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern Int32 ShowWindow (Int32 hwnd, Int32 nCmdShow);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern Int32 FindWindow (string lpClassName, string lpWindowName);
        static int iOperCount=0;
        static int iOperCount2=0;
        public Form1 ()
        {
            InitializeComponent();
            HideTask();
            this.WindowState = FormWindowState.Maximized;

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            this.chromiumWebBrowser1.Load("http://localhost:9998/index.html");
            chromiumWebBrowser1.RegisterJsObject("xuanhuJS", new JsEvent(), new BindingOptions() { CamelCaseJavascriptNames = false });
            

            this.chromiumWebBrowser2.Load("http://www.powerbibbs.com:8282/design?fmenuid=menuRptfid2e76fad0c44f11e89ddf3179bb7b39db&fid=rptaea0fd60c2dc11e8b06943b98c029e53");
            chromiumWebBrowser2.RegisterJsObject("xuanhuJS", new JsEvent(), new BindingOptions() { CamelCaseJavascriptNames = false });
            this.chromiumWebBrowser2.FrameLoadEnd += ChromiumWebBrowser2_FrameLoadEnd;

            this.chromiumWebBrowser3.Load("http://qq.com");
            MyMessage msg=new MyMessage();
            Application.AddMessageFilter(msg);

            this.tabControl1.SelectedIndex = 2;
           
            this.axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            this.axWindowsMediaPlayer1.ClickEvent += AxWindowsMediaPlayer1_ClickEvent;
            this.axWindowsMediaPlayer1.PlayStateChange += AxWindowsMediaPlayer1_PlayStateChange;
        }
       
        private void ChromiumWebBrowser2_FrameLoadEnd (object sender, FrameLoadEndEventArgs e)
        {
            var js = "$(function(){$('body,#app').click(function(e){if(typeof(xuanhuJS)!=\"undefined\"){xuanhuJS.ShowTest();return false}})});";
            this.chromiumWebBrowser2.GetBrowser().MainFrame.ExecuteJavaScriptAsync(js);
        }

        private void AxWindowsMediaPlayer1_PlayStateChange (object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (!this.timer1.Enabled)
            {
                if (this.axWindowsMediaPlayer1.playState == WMPPlayState.wmppsStopped)
                {
                    this.tabControl1.SelectedIndex = 0;
                    iOperCount2 = 0;
                    this.timer2.Start();
                }
            }
        }

        private void AxWindowsMediaPlayer1_ClickEvent (object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            this.SwitchTab();
        }


        public void SwitchTab ()
        {
            this.Invoke((EventHandler)delegate
            {
                iOperCount = 0;
                if (this.timer1.Enabled)
                {
                    if (this.tabControl1.SelectedIndex != 1)
                    {
                        this.timer1.Stop();
                    }
                    if (this.tabControl1.SelectedIndex == 2)
                    {
                        this.axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }
                else
                {
                    if (this.tabControl1.SelectedIndex == 0 || this.tabControl1.SelectedIndex == 2)
                    {
                        this.tabControl1.SelectedIndex = 1;
                        this.axWindowsMediaPlayer1.Ctlcontrols.stop();
                    }
                    this.timer1.Start();
                }
            });
        }
        private void Form1_Load (object sender, EventArgs e)
        {
           
        }

        protected override bool ProcessCmdKey (ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;
            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        var hwnd = FindWindow("Shell_TrayWnd", null);//获取任务栏的句柄
                        ShowWindow(hwnd, 5);//隐藏任务栏
                        this.Close();//Esc退出                  

                        break;
                }

            }
            return false;
        }
        internal class MyMessage : IMessageFilter
        {
            public bool PreFilterMessage (ref Message m)
            {
                //int WM_KEYDOWN = 256;
                //int WM_SYSKEYDOWN = 260;
                //if (m.Msg == WM_KEYDOWN | m.Msg == WM_SYSKEYDOWN)
                //{
                //    var keyData=Program.MainForm.key
                //    switch (keyData)
                //    {
                //        case Keys.Escape:
                //            var hwnd = FindWindow("Shell_TrayWnd", null);//获取任务栏的句柄
                //            ShowWindow(hwnd, 5);//隐藏任务栏
                //            this.Close();//Esc退出                  

                //            break;
                //    }

                //}

                if (m.Msg == 0x0201 || m.Msg == 0x0204)
                {
                    Program.MainForm.SwitchTab();
                    iOperCount = 0;
                }
               
                return false;
            }
        }

        private void timer1_Tick (object sender, EventArgs e)
        {
            iOperCount++;
            if (iOperCount > ConfigHelper.GetAppSettingInt("waittime"))
            {
                this.tabControl1.SelectedIndex = 2;
                this.axWindowsMediaPlayer1.Ctlcontrols.play();
                this.timer1.Stop();
                iOperCount = 0;
            }
        }

        private void tabControl1_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                this.timer1.Start();
            }
            if (this.tabControl1.SelectedIndex != 0)
            {
                this.timer2.Stop();
                iOperCount2 = 0;
            }
            iOperCount = 0;
        }

        private void axWindowsMediaPlayer1_Enter (object sender, EventArgs e)
        {
            //this.SwitchTab();
        }

        private void timer2_Tick (object sender, EventArgs e)
        {
            iOperCount2++;
            if (iOperCount2 > ConfigHelper.GetAppSettingInt("dapingtime"))
            {
                this.tabControl1.SelectedIndex = 2;
                this.axWindowsMediaPlayer1.Ctlcontrols.play();
                this.timer2.Stop();
                iOperCount2 = 0;
            }
        }

        private void HideTask ()
        {
            var hwnd = FindWindow("Shell_TrayWnd", null);//获取任务栏的句柄
            ShowWindow(hwnd, 0);//隐藏任务栏
        }
    }
}
