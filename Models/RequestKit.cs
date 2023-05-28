using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rostelekom.Models
{
    public class RequestKit
    {
        public int Id { get; set; }
        public int EmployeesId { get; set; }
        public string EmployerName { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int StatusRequestId { get; set; }
        public string StatusRequestName { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, EmployeesId = {EmployeesId}, EmployerName = {EmployerName}, RequestDate = {RequestDate.ToString("g")}, CompletionDate = {CompletionDate.ToString("g")}, StatusRequestId = {StatusRequestId}, StatusRequestName = {StatusRequestName} ";
        }
    }
}
