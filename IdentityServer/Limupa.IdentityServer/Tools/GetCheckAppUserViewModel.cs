﻿using System;
using System.Collections.Generic;

namespace Limupa.IdentityServer.Tools
{
    public class GetCheckAppUserViewModel
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsExist { get; set; }
    }
}
