using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListToDo.Web.Models
{
    public class ListToDo
    {
        public int Id { get; set; }
        public string NomeItem { get; set; }
        public string DescricaoItem { get; set; }
        public string Status { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
    }
}