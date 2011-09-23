using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Julian.Me.Web.Models
{
  public class MessageModel
  {
    [Required(ErrorMessage="Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; }
  }
}