using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AggregatorController.DataAccess.DataModels
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class rss
    {
        private rssChannel channelField;
        private decimal versionField;

        public rssChannel channel
        {
            get => channelField;
            set => channelField = value;
        }

        [XmlAttribute]
        public decimal version
        {
            get => versionField;
            set => versionField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class rssChannel
    {
        private string titleField;
        private string linkField;
        private link link1Field;
        private object descriptionField;
        private string languageField;
        private string copyrightField;
        private string lastBuildDateField;
        private string pubDateField;
        private rssChannelImage imageField;
        private rssChannelItem[] itemField;

        public string title
        {
            get => titleField;
            set => titleField = value;
        }

        public string link
        {
            get => linkField;
            set => linkField = value;
        }

        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public link link1
        {
            get => link1Field;
            set => link1Field = value;
        }

        public object description
        {
            get => descriptionField;
            set => descriptionField = value;
        }

        public string language
        {
            get => languageField;
            set => languageField = value;
        }

        public string copyright
        {
            get => copyrightField;
            set => copyrightField = value;
        }

        public string lastBuildDate
        {
            get => lastBuildDateField;
            set => lastBuildDateField = value;
        }

        public string pubDate
        {
            get => pubDateField;
            set => pubDateField = value;
        }

        public rssChannelImage image
        {
            get => imageField;
            set => imageField = value;
        }

        [XmlElement("item")]
        public rssChannelItem[] item
        {
            get => itemField;
            set => itemField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
    [XmlRoot(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
    public partial class link
    {
        private string hrefField;
        private string relField;
        private string typeField;

        [XmlAttribute]
        public string href
        {
            get => hrefField;
            set => hrefField = value;
        }

        [XmlAttribute]
        public string rel
        {
            get => relField;
            set => relField = value;
        }

        [XmlAttribute]
        public string type
        {
            get => typeField;
            set => typeField = value;
        }
    }

    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class rssChannelImage
    {
        private string titleField;
        private string urlField;
        private string linkField;

        public string title
        {
            get => titleField;
            set => titleField = value;
        }

        public string url
        {
            get => urlField;
            set => urlField = value;
        }

        public string link
        {
            get => linkField;
            set => linkField = value;
        }
    }

    /// <remarks/>
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItem
    {
        private string titleField;
        private string linkField;
        private rssChannelItemGuid guidField;
        private link link1Field;
        private string descriptionField;
        private string creatorField;
        private string pubDateField;
        private rssChannelItemCategory[] categoryField;
        private content contentField;
        private string creditField;
        private string description1Field;

        public string title
        {
            get => titleField;
            set => titleField = value;
        }

        public string link
        {
            get => linkField;
            set => linkField = value;
        }

        public rssChannelItemGuid guid
        {
            get => guidField;
            set => guidField = value;
        }

        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public link link1
        {
            get => link1Field;
            set => link1Field = value;
        }

        public string description
        {
            get => descriptionField;
            set => descriptionField = value;
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/")]
        public string creator
        {
            get => creatorField;
            set => creatorField = value;
        }

        public string pubDate
        {
            get => pubDateField;
            set => pubDateField = value;
        }

        [XmlElement("category")]
        public rssChannelItemCategory[] category
        {
            get => categoryField;
            set => categoryField = value;
        }

        [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public content content
        {
            get => contentField;
            set => contentField = value;
        }

        [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public string credit
        {
            get => creditField;
            set => creditField = value;
        }

        [XmlElement("description", Namespace = "http://search.yahoo.com/mrss/")]
        public string description1
        {
            get => description1Field;
            set => description1Field = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItemGuid
    {
        private bool isPermaLinkField;
        private string valueField;

        [XmlAttribute]
        public bool isPermaLink
        {
            get => isPermaLinkField;
            set => isPermaLinkField = value;
        }

        [XmlText]
        public string Value
        {
            get => valueField;
            set => valueField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItemCategory
    {
        private string domainField;
        private string valueField;

        [XmlAttribute]
        public string domain
        {
            get => domainField;
            set => domainField = value;
        }

        [XmlText]
        public string Value
        {
            get => valueField;
            set => valueField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
    [XmlRoot(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
    public partial class content
    {
        private byte heightField;
        private string mediumField;
        private string urlField;
        private byte widthField;

        [XmlAttribute]
        public byte height
        {
            get => heightField;
            set => heightField = value;
        }

        [XmlAttribute]
        public string medium
        {
            get => mediumField;
            set => mediumField = value;
        }

        [XmlAttribute]
        public string url
        {
            get => urlField;
            set => urlField = value;
        }

        [XmlAttribute]
        public byte width
        {
            get => widthField;
            set => widthField = value;
        }
    }
}