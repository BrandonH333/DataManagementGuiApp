﻿using System;

namespace DataManagementGuiApp.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
}