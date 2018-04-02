using System.Collections.Generic;

namespace ListToDo.Web.Models.ViewModels
{
    public class ListaDeItemViewModel
    {
        public List<ListToDo> ListToDo_Pendetens { get; set; }
        public List<ListToDo> ListToDo_Concluidas { get; set; }
    }
}