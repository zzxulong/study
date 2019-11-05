using S22.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.Core
{
    public class ImapHelper
    {
        public static void GetEmails ()
        {
            string ImapServer = "mail.ldddns.com";
            string ImapUserame = "song";
            string ImapPwd = "20191104";
            using (S22.Imap.ImapClient client = new S22.Imap.ImapClient(ImapServer, 110, ImapUserame, ImapPwd))
            {
                var unseen = client.Search(SearchCondition.Unseen());

                if (unseen == null || unseen.Count() == 0)
                {
                    Console.WriteLine(string.Format("==============>没有新邮件！"));
                    return;
                }
                Console.WriteLine(string.Format("==============>开始检测"));
                foreach (uint uid in unseen)
                {
                    var msg= client.GetMessage(uid,true);
                    var dataStream = msg.AlternateViews[0].ContentStream;
                    byte[] byteBuffer = new byte[dataStream.Length];
                    string altbody = msg.BodyEncoding.GetString(byteBuffer, 0, dataStream.Read(byteBuffer, 0, byteBuffer.Length));
                    try
                    {
                       
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
    }
}
