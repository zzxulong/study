
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using MimeKit;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;
using MailKit.Search;

namespace ConsoleTest.Core
{
    public class PopHelper
    {
        #region 属性 账户配置信息等
        /// <summary>
        ///  发件人邮箱地址
        /// </summary>
        private string fromEmail = null;
        /// <summary>
        ///  发件人别名
        /// </summary>
        private string fromAlias = null;
        /// <summary>
        /// 发件人邮箱密码(或授权码)
        /// </summary>
        private string fromPwd = null;
        /// <summary>
        /// SMTP服务器地址
        /// </summary>
        private string serverSMTP = null;
        private int portSMTP = 0;
        /// <summary>
        /// IMAP服务器地址
        /// </summary>
        private string serverIMAP = null;
        private int portIMAP = 0;
        /// <summary>
        /// POP服务器地址
        /// </summary>
        private string serverPOP = null;
        private int portPOP = 0;
        /// <summary>
        /// 邮件账户(收邮件时登录账户)
        /// </summary>
        private string account = null;
        /// <summary>
        /// 邮件账户密码(收邮件时登录密码)
        /// </summary>
        private string pwd = null;
        #endregion

        #region 属性 邮件主体内容 内容块容器

        /// <summary>
        /// 邮件对象
        /// </summary>
        private MimeMessage message = null;
        /// <summary>
        /// 邮件内容块的容器 放置邮件正文,附件等内容块
        /// </summary>
        private Multipart mimeparts = null;
        /// <summary>
        /// 收件人列表
        /// </summary>
        private List<MailboxAddress> toList = null;
        /// <summary>
        /// 附件列表
        /// </summary>
        private List<MimePart> attaList = null;
        #endregion

        /// <summary>
        /// 操作异常信息
        /// </summary>
        public string ErrMsg { get; private set; }

        #region 制作与发送邮件

        /// <summary>
        /// 添加一个收件人
        /// 在制作邮件方法之前调用
        /// </summary>
        /// <param name="address">收件人地址</param>
        /// <param name="name"></param>
        public void AddTo (string address, string name = null)
        {
            if (this.toList == null)
                this.toList = new List<MailboxAddress>();
            if (string.IsNullOrWhiteSpace(name))
                name = address.Substring(0, address.IndexOf('@'));
            this.toList.Add(new MailboxAddress(name, address));
        }

        /// <summary>
        /// 添加一个附件
        /// 在制作邮件方法之前调用
        /// </summary>
        /// <param name="atta">附件流</param>
        /// <param name="name">附件名字</param>
        /// <param name="size">附件大小(K)</param>
        public void AddAttachment (Stream atta, string name, long size = 0)
        {
            try
            {
                if (this.attaList == null)
                    this.attaList = new List<MimePart>();
                // 附件内容块
                MimePart attapart = new MimePart();
                attapart.Content = new MimeContent(atta);

                // 内容描述为附件
                attapart.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);

                // 附件名字设置,如果名字有中文也没关系
                attapart.ContentDisposition.FileName = name;
                // 大小设置
                if (size > 0)
                    attapart.ContentDisposition.Size = size;

                // 采用base64编码传输
                attapart.ContentTransferEncoding = ContentEncoding.Base64;

                //
                this.attaList.Add(attapart);
            }
            catch (Exception e)
            {
                ErrMsg = $"添加附件异常:{e.ToString()} [{e.Message}]";
            }
        }

        /// <summary>
        /// 制作一封邮件
        /// 调用此方法之前,先调用邮件配置初始化方法和添加收件人,添加附件方法
        /// </summary>
        /// <param name="subject">邮件主题(标题)</param>
        /// <param name="body">邮件正文(内容)</param>
        /// <param name="ishtml">正文是否为HTML格式,纯文本格式=false</param>
        public void MakeEmail (string subject, string body, bool ishtml = true)
        {
            try
            {
                // 邮件类新实例
                message = new MimeMessage();

                // 设置邮件主题
                message.Subject = subject;

                // 设置发件人信息
                message.From.Add(new MailboxAddress(fromAlias, fromEmail));

                // 设置收件人信息
                message.To.AddRange(this.toList);

                // 设置邮件正文
                var content = new TextPart (ishtml? "html" : "plain");
                content.SetText(Encoding.UTF8, body);

                // 建立内容块容器,将内容或附件等添加到其中 MimeEntity是各种类型内容的基类
                mimeparts = new Multipart("mixed");
                mimeparts.Add(content);
                // 附件
                if (this.attaList != null)
                {
                    foreach (var atta in this.attaList)
                    {
                        mimeparts.Add(atta);
                    }
                }

                // 将内容块容器设置到邮件的内容.到此已经填好邮件实体的主要属性
                message.Body = mimeparts;
            }
            catch (Exception e)
            {
                ErrMsg = $"制作邮件异常:{e.ToString()} [{e.Message}]";
            }
        }

        /// <summary>
        /// 设置此邮件是对指定邮件的回复(这是一封回复邮件)
        /// 在调用制作邮件方法之后调用,在发送前调用.需要调用收件配置方法CfgIMAP()
        /// </summary>
        /// <param name="uniqueid">被回复邮件唯一标识</param>
        /// <param name="folderName">被回复邮件文件夹</param>
        public void SetReplyTo (uint uniqueid, string folderName = null)
        {
            try
            {
                // 被回复的邮件
                MimeMessage remail;
                // 查找这个被回复的邮件,设置回复状态
                using (var client = ConnectIMAP())
                {
                    if (folderName == null)
                        folderName = client.Inbox.Name;
                    var emailUniqueId = new UniqueId(uniqueid);
                    var folder = client.GetFolder(folderName);
                    folder.Open(FolderAccess.ReadWrite);

                    remail = folder.GetMessage(emailUniqueId);
                    folder.AddFlags(emailUniqueId, MessageFlags.Answered, true);
                    folder.Close();
                    client.Disconnect(true);
                }
                // construct the In-Reply-To and References headers
                if (!string.IsNullOrEmpty(remail.MessageId))
                {
                    // 设置此邮件是对这个MESSAGEID的邮件的回复
                    message.InReplyTo = remail.MessageId;
                    // 此邮件的"对其它消息"的引用属性设为这个邮件的引用属性
                    foreach (var id in remail.References)
                        message.References.Add(id);
                    message.References.Add(remail.MessageId);
                }
                // 回复邮件主题前面加RE:
                if (!message.Subject.StartsWith("RE:", StringComparison.OrdinalIgnoreCase))
                    message.Subject = "RE:" + message.Subject;
            }
            catch (Exception e)
            {
                ErrMsg = $"设置为回复邮件异常:{e.ToString()} [{e.Message}]";
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="uniqueid"></param>
        /// <param name="folderName"></param>
        //public void SetForWard(uint uniqueid, string folderName = null)
        //{

        //}

        /// <summary>
        /// 发送一个邮件
        /// 调用此方法之前,请先调用建立邮件的方法MakeMessage()
        /// </summary>
        public bool SendEmail ()
        {
            try
            {
                // 建立发件服务客户端
                using (var client = new SmtpClient())
                {
                    // SMTP服务器
                    client.Connect(serverSMTP, portSMTP);

                    // 登录
                    client.Authenticate(fromEmail, fromPwd);

                    // 发邮件
                    client.Send(message);

                    // 关闭连接
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"发送邮件异常:{e.ToString()} [{e.Message}]";
                return false;
            }
        }

        #endregion

        #region 接收与处理邮件

        /// <summary>
        /// 连接到IMAP服务器        
        /// </summary>
        private ImapClient ConnectIMAP ()
        {
            try
            {
                ImapClient client = new ImapClient();
                client.Connect(serverIMAP, portIMAP);
                client.Authenticate(account, pwd);

                /**********************************************************************/
                // 网易126 163相关邮箱时,要用这两句话,表明客户端身份.在连接后调用.否则无法登录邮箱.
                var clientImplementation = new ImapImplementation
                {
                    Name = "MeSince",
                    Version = "2.0"
                };
                var serverImplementation = client.Identify(clientImplementation);
                /*********************************************************************/

                return client;
            }
            catch (Exception e)
            {
                ErrMsg = $"连接到IMAP服务器异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        /// <summary>
        /// 获取邮箱的所有文件夹列表
        /// 调用前先调用配置方法CfgIMAP()
        /// </summary>
        public EmailViewM GetFolders ()
        {
            try
            {
                using (var client = ConnectIMAP())
                {
                    List<IMailFolder> mailFolderList = client.GetFolders(client.PersonalNamespaces[0]).ToList();
                    var entity = FillEntity(null, null, mailFolderList.ToArray());
                    client.Disconnect(true);
                    //
                    return entity;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"获取邮箱的所有文件夹异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        /// <summary>
        /// 根据唯一标识和文件夹名,获取单个邮件
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public EmailViewM GetEmailByUid (uint uniqueid, string folderName = null)
        {
            try
            {
                using (ImapClient client = ConnectIMAP())
                {
                    if (folderName == null)
                        folderName = client.Inbox.Name;
                    IMailFolder folder = client.GetFolder(folderName);
                    folder.Open(FolderAccess.ReadOnly);
                    var email = folder.GetMessage(new UniqueId(uniqueid));
                    var entity = FillEntity(null, email);
                    //
                    folder.Close();
                    client.Disconnect(true);
                    // 
                    return entity;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"获取单个邮件异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        /// <summary>
        /// 获取一个文件夹的邮件 返回一个列表,包含摘要信息.收件/发件人,有几个附件,时间和标题,是否已读
        /// 默认只获取3个月内的邮件
        /// 调用前先调用配置方法CfgIMAP()
        /// </summary>
        public List<EmailViewM> GetEmailByFolder (string folderName = null)
        {
            try
            {
                using (ImapClient client = ConnectIMAP())
                {
                    IMailFolder folder;
                    // 默认是收件箱
                    if (folderName == null || folderName.ToLower() == "inbox")
                    {
                        folder = client.GetFolder(client.Inbox.Name);
                    }
                    else
                    {
                        // 其它特定的文件夹
                        string dirK = folderName.ToLower();
                        Dictionary<string, SpecialFolder> fdict = new Dictionary<string, SpecialFolder>();
                        fdict.Add("archive", SpecialFolder.Archive);
                        fdict.Add("drafts", SpecialFolder.Drafts);
                        fdict.Add("flagged", SpecialFolder.Flagged);
                        fdict.Add("sent", SpecialFolder.Sent);
                        fdict.Add("junk", SpecialFolder.Junk);
                        fdict.Add("trash", SpecialFolder.Trash);
                        if (fdict.ContainsKey(dirK))
                            folder = client.GetFolder(fdict[dirK]);
                        else
                        {
                            // 否则是自定义的文件夹,或者是邮件服务商的特别文件夹
                            folder = client.GetFolder(folderName);
                        }
                    }

                    folder.Open(FolderAccess.ReadOnly);

                    // 获取所有邮件的唯一标识列表
                    SearchQuery sq = SearchQuery.DeliveredAfter(DateTime.Today.AddMonths(-3));
                    var emailUids = folder.Search(sq);

                    // 获取这些邮件的摘要信息(MessageSummaryItems.BodyStructure这个项可以知道是否带附件)
                    var mails = folder.Fetch(emailUids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure | MessageSummaryItems.Full);
                    List<EmailViewM> entityls = new List<EmailViewM>();
                    foreach (var emhead in mails)
                    {
                        var embody = folder.GetMessage(emhead.UniqueId);
                        var entity = FillEntity(emhead, embody, folder);
                        entityls.Add(entity);
                    }
                    //
                    folder.Close();
                    client.Disconnect(true);
                    //
                    return entityls;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"获取一个文件夹的邮件异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        /// <summary>
        /// 使用唯一ID获取一封完整邮件
        /// 调用前先调用配置方法CfgIMAP()
        /// </summary>
        /// <param name="folder">文件夹名,默认是收件箱</param>
        /// <param name="uniqueid">邮件唯一编号</param>
        public EmailViewM GetEmailByUniqueId (uint uniqueid, string folderName = null)
        {
            try
            {
                using (ImapClient client = ConnectIMAP())
                {
                    if (folderName == null)
                        folderName = client.Inbox.Name;
                    IMailFolder folder = client.GetFolder(folderName);
                    folder.Open(FolderAccess.ReadWrite);
                    UniqueId emailUniqueId = new UniqueId(uniqueid);

                    // 获取这些邮件的摘要信息
                    List<UniqueId> uids = new List<UniqueId>();
                    uids.Add(emailUniqueId);
                    var emaills = folder.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope);
                    var emhead = emaills[0];

                    // 获取邮件含正文部分,.
                    MimeMessage embody = folder.GetMessage(emailUniqueId);
                   
                    /*赋值到实体类*/
                    var entity = FillEntity(emhead, embody, folder);
                    //然后设置为已读
                    folder.AddFlags(emailUniqueId, MessageFlags.Seen, true);
                    //
                    folder.Close();
                    client.Disconnect(true);
                    //
                    return entity;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"获取单个完整邮件异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        /// <summary>
        /// 邮件添加标识(已读,已回复,已删除等等).参数值参考EmailViewM实体同名属性
        /// 调用前先调用配置方法CfgIMAP()
        /// </summary>
        /// <param name="uniqueIdls">同一文件夹下的邮件唯一标识列表</param>
        /// <param name="flag">标识代码 1=已读 2=已回复 8=删除</param>
        /// <param name="folderType">文件夹名</param>
        public void SetFlag (List<uint> uniqueIdls, int flag, string folderType = null)
        {
            try
            {
                using (ImapClient client = ConnectIMAP())
                {
                    List<UniqueId> uniqueids = uniqueIdls.Select(o => new UniqueId(o)).ToList();
                    MessageFlags messageFlags = (MessageFlags)flag;
                    if (folderType == null)
                        folderType = client.Inbox.Name;
                    IMailFolder folder = client.GetFolder(folderType);
                    folder.Open(FolderAccess.ReadWrite);
                    folder.AddFlags(uniqueids, messageFlags, true);
                    //
                    folder.Close();
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"邮件添加标识时异常:{e.ToString()} [{e.Message}]";
            }
        }

        /// <summary>
        /// 将邮件保存到草稿箱 返回邮件的唯一标识
        /// 调用前先调用配置方法CfgIMAP(),调用制做邮件方法
        /// </summary>
        public int SaveDrafts (int uniqueId = -1)
        {
            try
            {
                using (ImapClient client = ConnectIMAP())
                {
                    // 打开草稿箱,添加邮件
                    IMailFolder folder = client.GetFolder(SpecialFolder.Drafts);
                    folder.Open(FolderAccess.ReadWrite);

                    // 如果保存的是已经有的草稿邮件,则删除它再保存新的草稿.(没找到保存已有草稿的办法)
                    if (uniqueId > -1)
                    {
                        List<UniqueId> uidls = new List<UniqueId>();
                        uidls.Add(new UniqueId((uint)uniqueId));
                        folder.SetFlags(uidls, MessageFlags.Seen | MessageFlags.Deleted, true);
                        folder.Expunge(uidls);
                    }

                    UniqueId? uid = folder.Append(this.message, MessageFlags.Seen | MessageFlags.Draft);
                    //
                    folder.Close();
                    client.Disconnect(true);
                    return uid.HasValue ? (int)uid.Value.Id : -1;
                }
            }
            catch (Exception e)
            {
                ErrMsg = $"邮件保存草稿时异常:{e.ToString()} [{e.Message}]";
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 将邮件相关信息填充到实体对象
        /// </summary>
        /// <param name="emhead">邮件基本信息</param>
        /// <param name="embody">邮件详细信息</param>
        /// <param name="folders">邮箱文件夹</param>
        /// <returns></returns>
        private EmailViewM FillEntity (IMessageSummary emhead = null, MimeMessage embody = null, params IMailFolder[] folders)
        {
            try
            {
                // 邮件基本信息 主题(标题),发件人名,地址,日期,状态等
                EmailViewM entity = new EmailViewM();
                if (emhead != null)
                {
                    entity.UniqueId = emhead.UniqueId.Id;
                    if (emhead.Envelope.From.Count > 0)
                    {
                        entity.Name = emhead.Envelope.From.Mailboxes.ElementAt(0).Name;
                        entity.Address = emhead.Envelope.From.Mailboxes.ElementAt(0).Address;
                    }

                    entity.Date = emhead.Date.DateTime;
                    entity.Subject = emhead.Envelope.Subject;
                  
                    if (folders.Length > 0)
                    {
                        entity.FolderType = folders[0].Name;
                    }
                    // 收件人可能有多个
                    entity.ToList = new List<EmailViewM>();
                    foreach (var to in emhead.Envelope.To.Mailboxes)
                    {
                        entity.ToList.Add(new EmailViewM { Name = to.Name, Address = to.Address });
                    }
                    // 邮件状态,已读未读等等
                    if (emhead.Flags.HasValue)
                    {
                        entity.IsRead = emhead.Flags.Value.HasFlag(MessageFlags.Seen);
                        entity.IsAnswered = emhead.Flags.Value.HasFlag(MessageFlags.Answered);
                    }
                    // 附件个数(只传emhead时)
                    entity.Count = emhead.Attachments.Count();
                }

                // 正文 附件
                if (embody != null)
                {
                    // 正文
                    entity.BodyText = embody.TextBody;
                    entity.BodyHTML = embody.HtmlBody;

                    // 附件
                    // 附件个数(传embody时,包含有附件完整信息)
                    entity.Count = embody.Attachments.Count();
                    // 附件信息
                    if (entity.Count > 0)
                    {
                        entity.AttaList = new List<EmailViewM>();
                        // 这里要转成mimepart类型
                        foreach (MimePart attachment in embody.Attachments)
                        {
                            var atta = new EmailViewM();
                            atta.Name = attachment.ContentDisposition.FileName;
                            atta.AttaStream = new MemoryStream();
                            attachment.Content.DecodeTo(atta.AttaStream);
                            atta.Size = Math.Round((double)atta.AttaStream.Length / 1024, 1).ToString();
                            entity.AttaList.Add(atta);
                        }
                    }
                }
                // 邮箱文件夹
                if (folders.Length > 0)
                {
                    entity.FolderList = new List<EmailViewM>();
                    foreach (var item in folders)
                    {
                        entity.FolderList.Add(new EmailViewM()
                        {
                            Name = item.Name,
                            FolderType = item.Attributes.ToString(),
                            Count = item.Count
                        });
                    }
                }
                return entity;
            }
            catch (Exception e)
            {
                ErrMsg = $"邮件填充到实体时异常:{e.ToString()} [{e.Message}]";
                return null;
            }
        }

        #region 配置账号密码方法

        /// <summary>
        /// 初始化一个发件人的配置,发件箱,发件箱密码,SMTP服务器
        /// </summary>
        /// <param name="emailCode"></param>
        public void CfgSendEmail (int emailCode)
        {
            switch (emailCode)
            {
                default:
                    fromAlias = "发件人名称";
                    fromEmail = "发件人地址";
                    fromPwd = "授权码或密码";
                    serverSMTP = "smtp服务器地址";
                    portSMTP = 25;
                    break;
            }
        }

        /// <summary>
        /// 初始化一个接收邮件的配置 登录名和密码,IMAP服务器,
        /// </summary>
        /// <param name="accountCode"></param>
        public void CfgIMAP (int accountCode)
        {
            switch (accountCode)
            {
                default:
                    account = "song";
                    pwd = "20191104";//"";
                    serverIMAP = "mail.ldddns.com";
                    portIMAP = 143;
                    //serverPOP = "POP3服务器地址";
                    //portPOP = 110;
                    break;
            }
        }
        #endregion
    }

    public class EmailViewM
    {
        public string FromName { get; set; }
        /// <summary>
        /// 1.从服务器上获取的邮件的UniqueId
        /// </summary>
        /// 
        public uint UniqueId { get; set; }
        /// <summary>
        /// 1.发件人名字,这个名字可能为null.因为发件人可以不设名字
        /// 2.收件人名(只在ToList里的对象有值)
        /// 3.附件名(只在AttaList里的对象有值)
        /// 4.文件夹名字(只在FolderList里的对象有值)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 1.发件人地址
        /// 2.收件人地址(只在ToList里的对象有值)
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 发件人邮箱授权码
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<EmailViewM> ToList { get; set; }
        /// <summary>
        /// 邮件主题(标题)
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件时间
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 1.附件个数
        /// 2.文件夹内邮件个数(只在FolderList里的对象有值)
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 附件标识ID在保存附件在本地时设置(只在AttaList里的对象有值)
        /// 当附件从邮件服务器下载到本地后,需要向客户端提供下载时,用这个ID找到该附件.
        /// </summary>
        public string AttaGuid { get; set; }
        /// <summary>
        /// 附件大小(只在AttaList里的对象有值)
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 附件流(只在AttaList里的对象有值)
        /// </summary>
        public Stream AttaStream { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<EmailViewM> AttaList { get; set; }
        /// <summary>
        /// 是否已经读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 是否已经回复
        /// </summary>
        public bool IsAnswered { get; set; }
        /// <summary>
        /// 邮件正文的纯文本形式
        /// </summary>
        public string BodyText { get; set; }
        /// <summary>
        /// 邮件正文的HTML形式.
        /// </summary>
        public string BodyHTML { get; set; }

        /// <summary>
        /// 邮箱的文件夹列表
        /// </summary>
        public List<EmailViewM> FolderList { get; set; }
        /// <summary>
        /// 文件夹类型名
        /// 1.表示当前邮件所处文件夹名字
        /// 2.在FolderList里的对象,表示文件夹名字
        ///inbox(收件箱),
        ///archive(档案箱),
        ///drafts(草稿箱),
        ///flagged(标记的),
        ///junk(垃圾箱),
        ///sent(发件箱),
        ///trash(回收箱)
        /// </summary>
        public string FolderType { get; set; }
        /// <summary>
        /// 邮件标识,需要修改邮件标识时,传入此值
        /// 1=Seen(设为已读),
        /// 2=Answered(设为已经回复),
        /// 8=Deleted(设为删除),
        /// </summary>
        public int Flag { get; set; }
    }
}

