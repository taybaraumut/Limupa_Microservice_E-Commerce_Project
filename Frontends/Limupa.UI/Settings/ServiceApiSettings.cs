﻿namespace Limupa.UI.Settings
{
    public class ServiceApiSettings
    {
        public string OcelotUrl { get; set; }
        public string IdentityUrl { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi Discount { get; set; }
        public ServiceApi Order { get; set; }
        public ServiceApi Cargo { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Payment { get; set; }
        public ServiceApi Comment { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
