namespace Blog2Github
{
    class BlogProvider
    {
        private readonly string m_Name;
        private readonly string m_MetaWeblogUrlFormat;

        public BlogProvider(string name, string metaWeblogUrlFormat)
        {
            m_Name = name;
            m_MetaWeblogUrlFormat = metaWeblogUrlFormat;
        }

        public string GetMetaWeblogUrl(string user)
        { 
            return string.Format(m_MetaWeblogUrlFormat, user) ;
        }

        public override string ToString()
        {
            return m_Name;
        }
    }
}