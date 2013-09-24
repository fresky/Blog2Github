using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MetaWeblogSharp;
using Path = System.IO.Path;

namespace Blog2Github
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            comboBlogList.Items.Add(new BlogProvider("cnblogs", "http://{0}.cnblogs.com/services/metaweblog.aspx"));
            comboBlogList.Items.Add("Other");
            comboBlogList.SelectedIndex = 0;
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            btnGenerate.IsEnabled = false;
            var blogProvider = comboBlogList.SelectedItem as BlogProvider;

            string blogMetweblogUrl;
            string password = pwboxPassword.Password;
            string username = textUserName.Text;
            int count = 500;
            int.TryParse(textCount.Text, out count);
            string outputPath = textOutput.Text;

            blogMetweblogUrl = blogProvider != null ? blogProvider.GetMetaWeblogUrl(username) : textUrl.Text;

            List<PostInfo> posts = new List<PostInfo>();
            await Task.Factory.StartNew(() =>
            {
                var blogcon = new BlogConnectionInfo(
                    string.Empty,
                    blogMetweblogUrl,
                    string.Empty,
                    username,
                    password);

                var client = new Client(blogcon);
                posts = client.GetRecentPosts(count);
                
            });

            int finished = 0;
            progressBar.Maximum = posts.Count;
            progressBar.Minimum = 0;
            foreach (var postInfo in posts)
            {
                await Task.Factory.StartNew(() => { write2Markdown(postInfo, outputPath); },
                                        TaskCreationOptions.AttachedToParent);
                finished++;
                progressBar.Value = finished;
                textFinished.Text = finished.ToString();
            }
            btnGenerate.IsEnabled = true;
        }

        private static void write2Markdown(PostInfo postInfo, string outputPath)
        {
            string createDate = ((DateTime) postInfo.DateCreated).ToString("yyyy-MM-dd");
            string path = Path.Combine(outputPath, string.Format("{0}-blogpost.markdown", createDate));
            int index = 1;          
            while (File.Exists(path))
            {
                path = Path.Combine(outputPath, string.Format("{0}-blogpost{1}.markdown", createDate, ++index));
            }
            string head =
                string.Format(
                    "---{0}layout: post{0}title: \"{1}\"{0}date: {2}{0}comments: true{0}categories: {0}---{0}",
                    Environment.NewLine, postInfo.Title, createDate);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(head);
                sw.Write(postInfo.Description);
            }
        }
    }
}
