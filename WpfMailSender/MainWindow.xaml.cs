using MailSender;
using MailSender.Models;
using MailSender.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        byte[] file;
        string FileName;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMailServices service = new SMTPService();
                var model = new MailModer
                {
                    From = nFrom.Text,
                    ToStr = tbTo.Text,
                    Title = tb_Subject.Text,
                    Body = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd).Text,

                };
                model.Attachments.Add(new MailModer.Attachment
                {
                    Name = SelectFile.Content.ToString(),
                    Content = file
                });
                MessageBox.Show(await service.Send(model));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var formDialog = new System.Windows.Forms.OpenFileDialog();
            var result = formDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:

                    var fullFileName = formDialog.FileName;
                    FileName = fullFileName.Split('\\').LastOrDefault();
                    SelectFile.Content = FileName;
                    file = File.ReadAllBytes(fullFileName);
                    break;

                default:
                    SelectFile.Content = "...";
                    break;
            }
        }
    }
}

